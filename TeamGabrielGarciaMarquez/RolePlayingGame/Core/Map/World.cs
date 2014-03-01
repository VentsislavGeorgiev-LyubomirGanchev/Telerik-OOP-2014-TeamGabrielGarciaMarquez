using RolePlayingGame.Core.Human;
using RolePlayingGame.Core.Human.Enemies;
using RolePlayingGame.Core.Item;
using RolePlayingGame.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Map
{
	internal class World : IRenderable
	{
		#region Constants

		private const string MapFilePath = @"Content\map.txt";
		private const string StartingAreaKey = "start";
		private const string MissingAreaKey = "-";

		#endregion Constants

		#region Static

		private static readonly Random _Random = new Random();

		#endregion Static

		#region Fields

		private readonly Dictionary<string, IArea> _world = new Dictionary<string, IArea>();
		private IArea _currentArea;
		private IPlayer _player;
		private readonly GameState _gameState;
		private IList<TextPopup> _textPopups;

		#endregion Fields

		public World(GameState gameState, SaveGameData savegame = null)
		{
			this._gameState = gameState;

			//Main pool for all popups in the game.
			this._textPopups = new List<TextPopup>();

			//Read in the map file
			this.ReadMapFile(MapFilePath);

			//Find the start point
			this._currentArea = this._world[StartingAreaKey];

			//Create and position the hero character
			bool playerFound = false;
			for (int row = 0; row < Area.MapSizeY && !playerFound; row++)
			{
				for (int col = 0; col < Area.MapSizeX && !playerFound; col++)
				{
					var mapTile = this._currentArea.GetMapTile(row, col);
					if (mapTile.Type.HasValue && mapTile.Type == EntityType.Player)
					{
						playerFound = true;
						this._player = mapTile.Sprite as Player;
						if (savegame != null)
						{
							this._player.LoadSaveGame(savegame);
							this._currentArea = this._world[savegame.Area];
						}

						this._gameState.HUD.Update(this._player);
						mapTile.SetForegroundSprite(null);
					}
				}
			}
		}

		#region Methods

		public SaveGameData SaveGame()
		{
			var savegame = new SaveGameData(this._player);
			savegame.Area = this._currentArea.Name;
			return savegame;
		}

		public void Update(double gameTime, double elapsedTime)
		{
			//We only actually update the current area the rest all 'sleep'
			this._currentArea.Update(gameTime, elapsedTime);
			//Hero update
			this._player.Update(gameTime, elapsedTime);
			// HUD update
			this._gameState.HUD.Update(this._player);
		}

		public void Draw(IRenderer renderer)
		{
			this._currentArea.Draw(renderer);
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
						this._player.AttackWithMagic(this._currentArea, this._textPopups);
						break;
				}
			}
		}

		private void ReadMapFile(string filePath)
		{
			using (StreamReader stream = new StreamReader(filePath))
			{
				while (!stream.EndOfStream)
				{
					//Each area constructor will consume just one area
					Area area = new Area(stream);
					this._world.Add(area.Name, area);
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
				this.ChangeArea(_currentArea.EastArea, Direction.Right);
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
				this.ChangeArea(_currentArea.WestArea, Direction.Left);
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
				this.ChangeArea(_currentArea.NorthArea, Direction.Up);
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
				this.ChangeArea(_currentArea.SouthArea, Direction.Down);
			}
		}

		private bool CheckForTeleportation(KeyEventArgs e)
		{
			var teleportLevel = GameMaster.Teleport(e);
			if (teleportLevel != null)
			{
				this._currentArea = this._world[teleportLevel];
				this._player.Position = new Point(GameMaster.SaveSpot);
				this.CalculateSpriteNextLocation(true);
				return true;
			}
			return false;
		}

		private bool CheckNextTile(Direction direction)
		{
			MapTile nextMapTile = this._currentArea.GetNextMapTile(direction, this._player.Position);
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
				this._currentArea = this._world[areaName];
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