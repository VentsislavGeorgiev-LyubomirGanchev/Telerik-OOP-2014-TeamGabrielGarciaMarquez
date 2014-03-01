using RolePlayingGame.Core.Human;
using RolePlayingGame.Core.Human.Enemies;
using RolePlayingGame.Core.Item;
using RolePlayingGame.Core.Map;
using RolePlayingGame.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace RolePlayingGame.Core
{
	internal class GameMovement : World
	{
		#region Constants

		private const string MissingAreaKey = "-";

		#endregion Constants

		#region Static

		private static readonly Random _Random = new Random();

		#endregion Static

		#region Fields

		private readonly GameState _gameState;

		private IList<TextPopup> _textPopups;

		private IPlayer _player;

		#endregion Fields

		public GameMovement(GameState gameState, SaveGameData savegame = null)
			: base(savegame)
		{
			this._gameState = gameState;

			//Main pool for all popups in the game.
			this._textPopups = new List<TextPopup>();

			//Create and position the hero character
			bool playerFound = false;
			for (int row = 0; row < Area.MapSizeY && !playerFound; row++)
			{
				for (int col = 0; col < Area.MapSizeX && !playerFound; col++)
				{
					var mapTile = this.CurrentArea.GetMapTile(row, col);
					if (mapTile.Type.HasValue && mapTile.Type == EntityType.Player)
					{
						playerFound = true;
						this._player = mapTile.Sprite as Player;
						if (savegame != null)
						{
							this._player.LoadSaveGame(savegame);
							this.CurrentArea = this.WorldMap[savegame.Area];
						}
						mapTile.SetForegroundSprite(null);
					}
				}
			}

			gameState.HUD.Update(this._player);
			Sounds.PlayBackgroundSound(LevelType.Level1);
		}

		#region Methods

		public SaveGameData SaveGame()
		{
			return this.SaveGame(this._player);
		}

		public override void Update(double gameTime, double elapsedTime)
		{
			base.Update(gameTime, elapsedTime);

			//Hero update
			this._player.Update(gameTime, elapsedTime);

			// HUD update
			this._gameState.HUD.Update(this._player);
		}

		public override void Draw(IRenderer renderer)
		{
			base.Draw(renderer);

			this._player.Draw(renderer);

			//If we are fighting then draw the damage
			if (this._player.IsFighting)
			{
				foreach (TextPopup popup in this._textPopups)
				{
					popup.Draw(renderer);
				}
			}
		}

		public void KeyDown(KeyEventArgs e)
		{
			//Ignore keypresses while we are animating or fighting
			if (!this._player.IsAnimationEnabled && !this._player.IsFighting)
			{
				if (this.CheckForTeleportation(e))
				{
					return;
				}

				switch (e.KeyCode)
				{
					case Keys.Right:
						this.TryMoveRight();
						break;

					case Keys.Left:
						this.TryMoveLeft();
						break;

					case Keys.Up:
						this.TryMoveUp();
						break;

					case Keys.Down:
						this.TryMoveDown();
						break;

					case Keys.P:
						//Magic - if we have any
						this._player.AttackWithMagic(this.CurrentArea, this._textPopups);
						break;
				}
			}
		}

		private void TryMoveRight()
		{
			//Are we at the edge of the map?
			if (this._player.CanMoveRight(Area.MapSizeXMaxIndex))
			{
				//Can we move to the next tile or not (blocking tile or monster)
				if (this.CheckNextTile(Direction.Right))
				{
					this._player.MoveRight(GameState.EntitiesMoveSpeed);
					this.CalculateSpriteNextLocation(false);
				}
			}
			else
			{
				this.ChangeArea(CurrentArea.EastArea, Direction.Right);
			}
		}

		private void TryMoveLeft()
		{
			//Are we at the edge of the map?
			if (this._player.CanMoveLeft(Area.MapSizeXMinIndex))
			{
				//Can we move to the next tile or not (blocking tile or monster)
				if (this.CheckNextTile(Direction.Left))
				{
					this._player.MoveLeft(GameState.EntitiesMoveSpeed);
					this.CalculateSpriteNextLocation(false);
				}
			}
			else
			{
				this.ChangeArea(CurrentArea.WestArea, Direction.Left);
			}
		}

		private void TryMoveUp()
		{
			//Are we at the edge of the map?
			if (this._player.CanMoveUp(Area.MapSizeYMinIndex))
			{
				//Can we move to the next tile or not (blocking tile or monster)
				if (this.CheckNextTile(Direction.Up))
				{
					this._player.MoveUp(GameState.EntitiesMoveSpeed);
					this.CalculateSpriteNextLocation(false);
				}
			}
			else
			{
				this.ChangeArea(CurrentArea.NorthArea, Direction.Up);
			}
		}

		private void TryMoveDown()
		{
			//Are we at the edge of the map?
			if (this._player.CanMoveDown(Area.MapSizeYMaxIndex))
			{
				//Can we move to the next tile or not (blocking tile or monster)
				if (this.CheckNextTile(Direction.Down))
				{
					this._player.MoveDown(GameState.EntitiesMoveSpeed);
					this.CalculateSpriteNextLocation(false);
				}
			}
			else
			{
				this.ChangeArea(CurrentArea.SouthArea, Direction.Down);
			}
		}

		private bool CheckForTeleportation(KeyEventArgs e)
		{
			var teleportLevel = GameMaster.Teleport(e);
			if (teleportLevel != null)
			{
				this.CurrentArea = this.WorldMap[teleportLevel];
				this._player.Position = new Point(GameMaster.SaveSpot);
				this.CalculateSpriteNextLocation(true);
				return true;
			}
			return false;
		}

		private bool CheckNextTile(Direction direction)
		{
			MapTile nextMapTile = this.CurrentArea.GetNextMapTile(direction, this._player.Position);
			//See if there is a door we need to open
			if (this.CheckDoors(nextMapTile))
			{
				return false;
			}

			IEnemy enemy = nextMapTile.Sprite as IEnemy;
			//See if there is character to fight
			if (enemy != null)
			{
				this._gameState.Fight(_Random, _player, enemy, this._textPopups);
				return false;
			}

			//If the next tile is not a blocker then we can move
			if (nextMapTile.IsPassable)
			{
				nextMapTile.OnPlayerMove(_player);
				return true;
			}

			return false;
		}

		private bool CheckDoors(MapTile mapTile)
		{
			//If the next tile is a closed door then check if we have the key

			if (mapTile.IsStateChangable && mapTile.IsPassable)
			{
				var obstacle = mapTile.Sprite as IObstacle;
				if (obstacle.State)
				{
					return false;
				}

				//For each key if it matches then open the door by switching the sprite & sprite to its matching open version
				if (this._player.HasKey)
				{
					this._player.HasKey = false;
					obstacle.ChangeState();
					return false;
				}
				return true;
			}
			return false;
		}

		private void ChangeArea(string areaName, Direction direction)
		{
			if (areaName != MissingAreaKey)
			{
				//Edge of map - move to next area
				this.CurrentArea = this.WorldMap[areaName];
				switch (direction)
				{
					case Direction.Left:
						this._player.Position.X = Area.MapSizeX - 1;
						break;

					case Direction.Right:
						this._player.Position.X = 0;
						break;

					case Direction.Up:
						this._player.Position.Y = Area.MapSizeY - 1;
						break;

					case Direction.Down:
						this._player.Position.Y = 0;
						break;

					default:
						throw new InvalidOperationException();
				}
				this.CalculateSpriteNextLocation(true);

				var areaNameTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(areaName);
				Sounds.PlayBackgroundSound((LevelType)Enum.Parse(typeof(LevelType), areaNameTitleCase));
			}
		}

		/// <summary>
		/// Calculate the next position of the player
		/// </summary>
		private void CalculateSpriteNextLocation(bool updateTheLocation)
		{
			//Calculate the eventual sprite destination based on the area grid coordinates
			this._player.CalculateSpriteLocation(updateTheLocation, Tile.TileSizeX, Tile.TileSizeY, Area.AreaOffsetX, Area.AreaOffsetY);
		}

		#endregion Methods
	}
}