using RolePlayingGame.Core.Human;
using RolePlayingGame.Core.Human.Enemies;
using RolePlayingGame.Core.Map;
using RolePlayingGame.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace RolePlayingGame.Core
{
	internal class GameState
	{
		#region Constants

		public const string MissMessage = "miss";

		public const int FrameRate = 8;
		public const int EntitiesMoveSpeed = 200;

		private const int _LuckyScope = 10;
		private const int _MissNumber = 6;
		private const int _MaxScopeForLuckyHit = 5;

		private const string _SaveGameFileName = "save.bin";

		#endregion Constants

		#region Static

		public static GameState LoadGame()
		{
			if (!File.Exists(_SaveGameFileName))
			{
				return null;
			}

			using (var stream = File.OpenRead(_SaveGameFileName))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				var savedGameState = new GameState((SaveGameData)binaryFormatter.Deserialize(stream));
				return savedGameState;
			}
		}

		#endregion Static

		#region Fields

		private GameMovement _gameMovement;
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
		}

		#region Properties

		public IHUD HUD { get; private set; }

		#endregion Properties

		#region Methods

		public void Initialize()
		{
			this.HUD.Initialize();
			//Create all the main gameobjects
			this._gameMovement = new GameMovement(this, this._savegame);
		}

		public void Fight(Random random, IPlayer player, IEnemy enemy, IList<TextPopup> popups)
		{
			player.IsFighting = true;
			popups.Clear();

			if (enemy as IBoss != null)
			{
				Sounds.BossFight();
			}
			else
			{
				Sounds.StudentFight();
			}

			int playerDamage = 0;

			//Logic for enemy making damage on the player

			if (random.Next(_LuckyScope) != _MissNumber)
			{
				if (enemy.Strength > player.Defense)
				{
					int scopeHit = enemy.Strength - player.Defense;

					playerDamage = random.Next((scopeHit / 100) * 10, scopeHit + 1);
				}
				else
				{
					if (enemy.Strength + _MaxScopeForLuckyHit > player.Defense)
					{
						int scopeHit = (enemy.Strength + _MaxScopeForLuckyHit) - player.Defense;
						playerDamage = random.Next((scopeHit / 100) * 50, scopeHit + 1);
					}
				}

				player.Health -= playerDamage;

				if (player.Health <= 0)
				{
					player.Die();
				}
			}

			string playerDamageMessage = playerDamage != 0 ? playerDamage.ToString() : MissMessage;
			popups.Add(new TextPopup(player.Location.X + 40, player.Location.Y + 20, playerDamageMessage));

			//Logic for player making damage on the enemy
			if (random.Next(_LuckyScope) != _MissNumber)
			{
				int enemyDamage = 0;
				if (player.Knowledge >= enemy.Defense)
				{
					int scopeHit = (player.Knowledge * 2) - player.Defense;
					enemyDamage = random.Next((scopeHit / 100) * 30, scopeHit + 1);
				}
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

			player.FightStartTime = -1;
		}

		public void SaveGame()
		{
			var savegame = this._gameMovement.SaveGame();
			using (var stream = File.OpenWrite(_SaveGameFileName))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(stream, savegame);
			}
		}

		public void Draw(IRenderer renderer)
		{
			this._gameMovement.Draw(renderer);
			this.HUD.Draw(renderer);
		}

		public void Update(double gameTime, double elapsedTime)
		{
			this._gameMovement.Update(gameTime, elapsedTime);
		}

		public void KeyDown(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				default:
					//If the game is not over then allow the user to play
					if (this.HUD.Health > 0 && !this.HUD.GameIsWon)
					{
						this._gameMovement.KeyDown(e);
					}
					else
					{
						//If game is over then allow S to restart
						if (e.KeyCode == Keys.S)
						{
							this.Initialize();
						}
					}
					break;
			}
		}

		#endregion Methods
	}
}