using RolePlayingGame.Core.Human;
using RolePlayingGame.Core.Human.Enemies;
using RolePlayingGame.Core.Map;
using RolePlayingGame.Core.Map.Tiles;
using RolePlayingGame.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RolePlayingGame.Core
{
	[Serializable]
	internal class GameEngine
	{
		#region Constants

		public const int FrameRate = 8;
		public const int EntitiesMoveSpeed = 200;

		private const int LuckyScope = 10;
		private const int LuckyNumber = 3;

		private const string MissMessage = "miss";

		#endregion Constants

		#region Fields

		private World _world;

		#endregion Fields

		public GameEngine()
		{
			this.HUD = Core.HUD.Instance;
		}

		#region Properties

		public IHUD HUD { get; private set; }

		#endregion Properties

		#region Methods

		public void Fight(Random random, IPlayer player, IEnemy enemy, IList<TextPopup> popups)
		{
			player.IsHeroFighting = true;
			popups.Clear();

			if (enemy as IBoss != null)
			{
				Sounds.BossFight();
			}
			else
			{
				Sounds.StudentFight();
			}

			//An enemy strength ability is 1/2 for boss and 1/3 for student of their max health. Compare that to your defense
			//If you outclass them then there is still a chance of a lucky hit
			int playerDamage = 0;

			if (random.Next(enemy.Strength + 1) >= player.Defense
				|| (enemy.Strength < player.Defense && random.Next(LuckyScope) == LuckyNumber))
			{
				//Enemies do damage up to their max health - if they hit you.
				playerDamage = random.Next(enemy.Health) + 1;
				player.Health -= playerDamage;

				if (player.Health <= 0)
				{
					player.Health = 0;
					//TODO EVENT for dead
					player.Entity.Tile = new Tile(Entity.TileDescriptions[EntityType.Bones.ToString()]);
				}
			}

			string playerDamageMessage = playerDamage != 0 ? playerDamage.ToString() : MissMessage;
			popups.Add(new TextPopup(player.Location.X + 40, player.Location.Y + 20, playerDamageMessage));

			//A enemy armour is 1/5 of their max health
			if (random.Next(player.Knowledge + 1) >= (enemy.Health / 5))
			{
				//Player damage is up to twice the attack rating
				int enemyDamage = random.Next(player.Knowledge * 2) + 1;
				if (enemyDamage > 0)
				{
					int experiance = enemy.GetDamage(enemyDamage);
					player.Experience += experiance;
				}
				string message = enemyDamage != 0 ? enemyDamage.ToString() : MissMessage;
				popups.Add(new TextPopup(enemy.Location.X + 40, enemy.Location.Y + 20, message));
			}
			else
			{
				popups.Add(new TextPopup(enemy.Location.X + 40, enemy.Location.Y + 20, MissMessage));
			}
		}

		public void Draw(IRenderer renderer)
		{
			this._world.Draw(renderer);
			this.HUD.Draw(renderer);
		}

		public void Update(double gameTime, double elapsedTime)
		{
			this._world.Update(gameTime, elapsedTime);
		}

		public void Initialize()
		{
			Sounds.Start();
			//Create all the main gameobjects
			this._world = new World(this);
		}

		public void KeyDown(Keys keys)
		{
			//If the game is not over then allow the user to play
			if (this.HUD.Health > 0 && !this.HUD.GameIsWon)
			{
				this._world.KeyDown(keys);
			}
			else
			{
				//If game is over then allow S to restart
				if (keys == Keys.S)
				{
					this.Initialize();
				}
			}
		}

		#endregion Methods
	}
}