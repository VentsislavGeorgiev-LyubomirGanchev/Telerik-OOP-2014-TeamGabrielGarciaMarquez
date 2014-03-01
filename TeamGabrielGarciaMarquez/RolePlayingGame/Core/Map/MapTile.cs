using RolePlayingGame.Core.Human;
using RolePlayingGame.Core.Item;
using RolePlayingGame.UI;

namespace RolePlayingGame.Core.Map
{
	/// <summary>
	/// A MapTile is a reference to the original tile and the sprite that represents it. Note that they have to be different
	/// because each tile can be used for multiple map tiles and each sprite has to have a unique location
	/// Each MapTile can also have an other tile object associated with it which is used for special items and monsters
	/// If its a monster then the current health is also stored per MapTile.
	/// </summary>
	internal class MapTile
	{
		private Sprite _backgroundSprite;
		private Sprite _foregroundSprite;

		public EntityType? Type
		{
			get
			{
				if (this._foregroundSprite != null)
				{
					return this._foregroundSprite.Entity.Type;
				}
				else
				{
					return null;
				}
			}
		}

		public EntityCategoryType? Category
		{
			get
			{
				if (this._foregroundSprite != null)
				{
					return this._foregroundSprite.Category;
				}
				else
				{
					return null;
				}
			}
		}

		public Sprite Sprite
		{
			get
			{
				if (this._foregroundSprite != null)
				{
					return this._foregroundSprite;
				}
				else
				{
					return null;
				}
			}
		}

		public PointF Location
		{
			get
			{
				return this._backgroundSprite.Location;
			}
		}

		public bool IsPassable
		{
			get
			{
				return (this._backgroundSprite.IsPassable || (this._foregroundSprite != null && this._foregroundSprite.IsStateChangable)) &&
					(this._foregroundSprite == null || this._foregroundSprite.IsPassable);
			}
		}

		public bool IsStateChangable
		{
			get
			{
				return this._foregroundSprite != null && this._foregroundSprite.IsStateChangable;
			}
		}

		public MapTile(Sprite backgroundSprite, Sprite foregroundSprite = null)
		{
			this._backgroundSprite = backgroundSprite;
			this._backgroundSprite.UpdateSprite += UpdateBackgroundSprite;
			this.SetForegroundSprite(foregroundSprite);
		}

		public void SetForegroundSprite(Sprite foregroundSprite)
		{
			this._foregroundSprite = foregroundSprite;
			if (foregroundSprite != null)
			{
				this._foregroundSprite.UpdateSprite += UpdateForegroundSprite;
			}
		}

		public void UpdateBackgroundSprite(EntityType type, Point location)
		{
			this._backgroundSprite = SpriteFactory.Create(location, type);
		}

		public void UpdateForegroundSprite(EntityType type, Point location)
		{
			this._foregroundSprite = SpriteFactory.Create(location, type);
		}

		public void OnPlayerMove(IPlayer player)
		{
			var dynamicItem = this.Sprite as DynamicItem;
			if (dynamicItem == null)
			{
				return;
			}

			//Dynamic objects change your stats in some way.
			switch (dynamicItem.Category)
			{
				case EntityCategoryType.Knowledge:
					player.Knowledge += dynamicItem.ItemRate;
					Sounds.KnowledgeUp();
					break;

				case EntityCategoryType.Defense:
					player.Defense += dynamicItem.ItemRate;
					Sounds.DefenseUp();
					break;

				case EntityCategoryType.Health:
					player.Health += dynamicItem.ItemRate;
					Sounds.HealthUp();
					break;

				case EntityCategoryType.Mana:
					player.Mana += dynamicItem.ItemRate;
					Sounds.ManaUp();
					break;

                case EntityCategoryType.Certificate:
                    player.HasCertificate = true;
                    break;

				case EntityCategoryType.Key:
					player.HasKey = true;
					Sounds.Pickup();
					break;
			}

			//Remove the object unless its bones or fire
			this._foregroundSprite = null;
		}

		public void Update(double gameTime, double elapsedTime)
		{
			this._backgroundSprite.Update(gameTime, elapsedTime);
			if (this._foregroundSprite != null)
			{
				if (this._foregroundSprite.FramesCount > 1)
				{
					this._foregroundSprite.CurrentFrameIndex = Sprite.CalculateNextFrame(gameTime, this._foregroundSprite.FramesCount);
				}
				this._foregroundSprite.Update(gameTime, elapsedTime);
			}
		}

		public void Draw(IRenderer renderer)
		{
			this._backgroundSprite.Draw(renderer);
			if (this._foregroundSprite != null)
			{
				this._foregroundSprite.Draw(renderer);
			}
		}
	}
}