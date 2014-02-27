using RolePlayingGame.Core.Human.Enemies;
using RolePlayingGame.Core.Map;
using System.Collections.Generic;

namespace RolePlayingGame.Core.Human
{
	internal class Player : Human, IMovable, IPlayer
	{
		#region Constants

		private const int DefaultHealth = 500;
		private const int DefaultMana = 5;
		private const int DefaultKnowledge = 30;
		private const int DefaultDefense = 50;
		private const int DefaultExperience = 0;
		private const int DefaultLevel = 1;
		private const int DefaultNextUpgrade = 50;
		private const int UpgradeMultiplicator = 2;
		private const int LevelUPMultiplicator = 10;

		#endregion Constants

		#region Fields

		private int _experience;
		private int _nextUpgrade;

		#endregion Fields

		#region Constructors

		//Just base constructor
		//Should be singleton design pattern
		public Player(float x, float y)
			: base(x, y, new Entity(EntityType.Player), true)
		{
			this.Health = DefaultHealth;
			this.Mana = DefaultMana;
			this.Knowledge = DefaultKnowledge;
			this.Defense = DefaultDefense;
			this.Experience = DefaultExperience;
			this._nextUpgrade = DefaultNextUpgrade;
			this.Level = DefaultLevel;
			this.IsHeroFighting = false;
			this.IsHeroAnimating = false;
		}

		#endregion Constructors

		#region Properties

		public int Mana { get; set; }

		public int Knowledge { get; set; }

		public int Defense { get; set; }

		//Experience property automatically upgrades your skill as the 'set' passes
		public int Experience
		{
			get
			{
				return _experience;
			}
			set
			{
				_experience = value;
				//If we hit the upgrade threshold then increase our abilities
				if (_experience > _nextUpgrade)
				{
					this.Mana += this.Level * LevelUPMultiplicator;
					this.Knowledge += this.Level * LevelUPMultiplicator;
					this.Health += this.Level * LevelUPMultiplicator;
					this.Defense += this.Level * LevelUPMultiplicator;
					this.Level++;
                    Sounds.LevelUp();
					//Each upgrade is a little harder to get
					_nextUpgrade *= UpgradeMultiplicator;
				}
			}
		}

		public bool IsHeroFighting { get; set; }

		public bool IsHeroAnimating { get; set; }

		public bool HasKey { get; set; }

		#endregion Properties

		#region Methods

		public void DoMagic(Area currentArea, IList<TextPopup> popups)
		{
			if (this.Mana > 0)
			{
				Sounds.Magic();
				this.Mana--;
				this.IsHeroFighting = true;
				popups.Clear();

				//All monsters on the screen take maximum damage
				for (int col = 0; col < Area.MapSizeX; col++)
				{
					for (int row = 0; row < Area.MapSizeY; row++)
					{
						Enemy enemy = currentArea.TilesMap[col, row].Sprite as Enemy;
						if (enemy != null)
						{
							//Player damage is up to twice the attack rating
							int damage = this.Knowledge * 2;
							int experiance = enemy.GetDamage(damage);
							this.Experience += experiance;
							popups.Add(new TextPopup(enemy.Location.X + 40, enemy.Location.Y + 20, damage.ToString()));
						}
					}
				}
			}
		}

		public void LoadSaveGame(SaveGameData savegame)
		{
			this.Health = savegame.Health;
			this.Mana = savegame.Mana;
			this.Knowledge = savegame.Knowledge;
			this.Defense = savegame.Defense;
			this.Experience = savegame.Experience;
			this.IsHeroFighting = savegame.IsHeroFighting;
			this.Level = savegame.Level;
			this.Location = new PointF(savegame.Location);
			this.Position = new Point(savegame.Position);
		}

		#endregion Methods
	}
}