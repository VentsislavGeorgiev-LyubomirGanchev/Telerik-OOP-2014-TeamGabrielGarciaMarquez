using RolePlayingGame.Core.Human;
using RolePlayingGame.Core.Human.Enemies;
using RolePlayingGame.Core.Map;
using RolePlayingGame.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RolePlayingGame.Core
{
	internal class GameState
	{
		#region Constants

		public const string MissMessage = "miss";

		public const int FrameRate = 8;
		public const int EntitiesMoveSpeed = 200;

		private const int LuckyScope = 10;
		private const int LuckyNumber = 3;

		#endregion Constants

		#region Fields

		private World _world;
		private SaveGameData _savegame;

		#endregion Fields

		public GameState(SaveGameData savegame = null)
		{
			this._savegame = savegame;
			this.HUD = Core.HUD.Instance;
			if (savegame != null)
			{
				this.Initialize();
			}
			Sounds.PlayBackgroundSound();
		}

		#region Properties

		public IHUD HUD { get; private set; }

		#endregion Properties

		#region Methods

		public void Initialize()
		{
			//Create all the main gameobjects
			this._world = new World(this, this._savegame);
		}

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
					player.Die();
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

		public SaveGameData SaveGame()
		{
			return this._world.SaveGame();
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

		public void KeyDown(KeyEventArgs e)
		{
			//If the game is not over then allow the user to play
			if (this.HUD.Health > 0 && !this.HUD.GameIsWon)
			{
				this._world.KeyDown(e);
			}
			else
			{
				//If game is over then allow S to restart
				if (e.KeyCode == Keys.S)
				{
					this.Initialize();
				}
			}
		}

		#endregion Methods
	}
}