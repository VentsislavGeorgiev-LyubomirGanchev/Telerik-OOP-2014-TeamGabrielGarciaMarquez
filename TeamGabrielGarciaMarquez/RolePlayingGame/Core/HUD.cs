using RolePlayingGame.Core.Human;
using RolePlayingGame.UI;
using System.Drawing;

namespace RolePlayingGame.Core
{
	/// <summary>
	/// Head-up Display Singleton class
	/// </summary>

	internal sealed class HUD : IHUD
	{
		#region Constants

		private const float HUDSpritesSpacing = 1.46f;
		private const float HUDSpritePositionX = 10.5f;
		private const float HUDSpritePositionY = 0.3f;

		private const int HUDTextSpacing = 97;
		private const int HUDTextPositionX = 750;
		private const int HUDTextPositionY = 20;

		private const string FontFamily = "Arial";
		private const int FontSize = 24;

		#endregion Constants

		#region Static

		private static readonly PointF HUDSpritePosition = new PointF(HUDSpritePositionX, HUDSpritePositionY);
		private static readonly Point HUDTextPosition = new Point(HUDTextPositionX, HUDTextPositionY);
		private static readonly Font _Font = new Font(FontFamily, FontSize);
		private static readonly Brush _Brush = new SolidBrush(Color.Black);

		private static HUD _Instance = new HUD();

		public static HUD Instance
		{
			get
			{
				return _Instance;
			}
		}

		#endregion Static

		#region Fields

		private readonly Sprite _currentLevel;
		private readonly Sprite _experienceSprite;
		private readonly Sprite _healthSprite;
		private readonly Sprite _manaSprite;
		private readonly Sprite _knowledgeSprite;
		private readonly Sprite _defenseSprite;
		private readonly Sprite _keySprite;

		private bool _gameFinishSoundPlayed;

		#endregion Fields

		private HUD()
		{
			this.SetSprite(out this._currentLevel, HUDSpritePosition, 0, new Entity(EntityType.Level));
			this.SetSprite(out this._experienceSprite, HUDSpritePosition, HUDSpritesSpacing, new Entity(EntityType.Experience));
			this.SetSprite(out this._healthSprite, HUDSpritePosition, HUDSpritesSpacing, new Entity(EntityType.Burger));
			this.SetSprite(out this._manaSprite, HUDSpritePosition, HUDSpritesSpacing, new Entity(EntityType.Beer));
			this.SetSprite(out this._knowledgeSprite, HUDSpritePosition, HUDSpritesSpacing, new Entity(EntityType.IntroCSharp));
			this.SetSprite(out this._defenseSprite, HUDSpritePosition, HUDSpritesSpacing, new Entity(EntityType.Keyboard));
			this.SetSprite(out this._keySprite, HUDSpritePosition, HUDSpritesSpacing, new Entity(EntityType.Key));
		}

		#region Properties

		public bool GameIsWon { get; set; }

		public int Health { get; set; }

		public int Mana { get; set; }

		public int Knowledge { get; set; }

		public int Defense { get; set; }

		public int Experience { get; set; }

		public bool HasKey { get; set; }

		public int Level { get; set; }

		#endregion Properties

		#region Methods

		public void Draw(IRenderer renderer)
		{
			this._currentLevel.Draw(renderer);
			this._experienceSprite.Draw(renderer);
			this._healthSprite.Draw(renderer);
			this._manaSprite.Draw(renderer);
			this._knowledgeSprite.Draw(renderer);
			this._defenseSprite.Draw(renderer);
			if (this.HasKey)
			{
				this._keySprite.Draw(renderer);
			}

			var hudTextPosition = new Point(HUDTextPosition);
			DrawString(renderer, this.Level.ToString(), hudTextPosition, 0);
			DrawString(renderer, this.Experience.ToString(), hudTextPosition, HUDTextSpacing);
			DrawString(renderer, this.Health.ToString(), hudTextPosition, HUDTextSpacing);
			DrawString(renderer, this.Mana.ToString(), hudTextPosition, HUDTextSpacing);
			DrawString(renderer, this.Knowledge.ToString(), hudTextPosition, HUDTextSpacing);
			DrawString(renderer, this.Defense.ToString(), hudTextPosition, HUDTextSpacing);

			//If the game is over then display the end game message
			if (this.Health == 0)
			{
				this.DrawMessage(renderer, new[] { "You died!", "Press 's' to play again" });
			}

			//If the game is won then show congratulations
			if (this.GameIsWon)
			{
				this.DrawMessage(renderer, new[] { "You won!", "Press 's' to play again" });
			}
		}

		public void DrawMessage(IRenderer renderer, string[] text)
		{
			renderer.DrawString(text[0], _Font, _Brush, 200, 250);
			if (text.Length > 1)
			{
				renderer.DrawString(text[1], _Font, _Brush, 100, 300);
			}
		}

		public void Update(IPlayer player)
		{
			this.Defense = player.Defense;
			this.Health = player.Health;
			this.Knowledge = player.Knowledge;
			this.Level = player.Level;
			this.Mana = player.Mana;
			this.Experience = player.Experience;
			this.HasKey = player.HasKey;
			if (player.HasCertificate && !this._gameFinishSoundPlayed)
			{
				this.GameIsWon = true;
				Sounds.StopSound();
				Sounds.End();
				this._gameFinishSoundPlayed = true;
			}
		}

		private void SetSprite(out Sprite sprite, PointF position, float spacing, Entity entity)
		{
			sprite = SpriteFactory.Create(position.X, position.Y += spacing, entity);
		}

		private void DrawString(IRenderer renderer, string text, Point position, int spacing)
		{
			renderer.DrawString(text, _Font, _Brush, position.X, position.Y += spacing);
		}

		#endregion Methods
	}
}