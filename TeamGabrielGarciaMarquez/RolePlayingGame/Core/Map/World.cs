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
		private const string StartArea = "start";

		#endregion Constants

		#region Static

		private static readonly Random _Random = new Random();

		#endregion Static

		#region Fields

		private readonly Dictionary<string, Area> _world = new Dictionary<string, Area>();
		private Area _currentArea;
		private Player _heroEntity;
		private double _startFightTime = -1.0;
		private PointF _heroNextLocation;
		private HeroDirection _direction;
		private readonly GameState _gameState;

		private IList<TextPopup> _popups;

		#endregion Fields

		public World(GameState gameState, SaveGameData savegame = null)
		{
			//Main pool for all popups in the game.
			this._popups = new List<TextPopup>();

			this._gameState = gameState;

			//Read in the map file
			this.ReadMapFile(MapFilePath);

			//Find the start point
			this._currentArea = this._world[StartArea];

			//Create and position the hero character
			var tilesMap = this._currentArea.TilesMap;
			bool playerFound = false;
			for (int row = 0; row < tilesMap.GetLength(0) && !playerFound; row++)
			{
				for (int col = 0; col < tilesMap.GetLength(1) && !playerFound; col++)
				{
					var mapTile = tilesMap[row, col];
					if (mapTile.Type.HasValue && mapTile.Type == EntityType.Player)
					{
						playerFound = true;
						this._heroEntity = mapTile.Sprite as Player;
						if (savegame != null)
						{
							this._heroEntity.LoadSaveGame(savegame);
							this._currentArea = this._world[savegame.Area];
						}

						this._gameState.HUD.Update(this._heroEntity);
						mapTile.SetForegroundSprite(null);
					}
				}
			}
		}

		#region Methods

		public SaveGameData SaveGame()
		{
			var savegame = new SaveGameData(this._heroEntity);
			savegame.Area = this._currentArea.Name;
			return savegame;
		}

		public void Update(double gameTime, double elapsedTime)
		{
			//We only actually update the current area the rest all 'sleep'
			this._currentArea.Update(gameTime, elapsedTime);

			//Hero update
			this._heroEntity.Update(gameTime, elapsedTime);

			//If the hero is moving we need to check if we are there yet
			if (this._heroEntity.IsHeroAnimating && this.CheckDestination())
			{
				//We have arrived. Stop moving and animating
				this._heroEntity.Location = this._heroNextLocation;
				this._heroEntity.Velocity = PointF.Empty;
				this._heroEntity.IsHeroAnimating = false;
			}

			//The hero gets animated when moving or fighting
			if (this._heroEntity.IsHeroAnimating || this._heroEntity.IsHeroFighting)
			{
				this._heroEntity.CurrentFrameIndex = Sprite.CalculateNextFrame(gameTime, this._heroEntity.FramesCount);
			}
			else
			{
				//Otherwise use frame 0
				this._heroEntity.CurrentFrameIndex = 0;
			}

			//If we are fighting then keep animating for a period of time
			if (this._heroEntity.IsHeroFighting)
			{
				if (this._startFightTime < 0)
				{
					this._startFightTime = gameTime;
				}
				else
				{
					if (gameTime - this._startFightTime > 1.0)
					{
						this._heroEntity.IsHeroFighting = false;
					}
				}
			}
			this._gameState.HUD.Update(this._heroEntity);
		}

		public void Draw(IRenderer renderer)
		{
			this._currentArea.Draw(renderer);
			this._heroEntity.Draw(renderer);

			//If we are fighting then draw the damage
			if (this._heroEntity.IsHeroFighting)
			{
				foreach (TextPopup popup in this._popups)
				{
					popup.Draw(renderer);
				}
			}
		}

		public void KeyDown(KeyEventArgs e)
		{
			//Ignore keypresses while we are animating or fighting
			if (!this._heroEntity.IsHeroAnimating && !this._heroEntity.IsHeroFighting)
			{
				var teleportLevel = GameMaster.Teleport(e);
				if (teleportLevel != null)
				{
					this._currentArea = this._world[teleportLevel];
					this._heroEntity.Position = new Point(GameMaster.SaveSpot);
					this.SetDestination();
					this._heroEntity.Location = this._heroNextLocation;
					return;
				}

				switch (e.KeyCode)
				{
					case Keys.Right:
						//Are we at the edge of the map?
						if (this._heroEntity.Position.X < Area.MapSizeX - 1)
						{
							//Can we move to the next tile or not (blocking tile or monster)
							if (this.CheckNextTile(this._currentArea.TilesMap[this._heroEntity.Position.X + 1, this._heroEntity.Position.Y], this._heroEntity.Position.X + 1, this._heroEntity.Position.Y))
							{
								this._heroEntity.Velocity = new PointF(GameState.EntitiesMoveSpeed, 0);
								this._heroEntity.Flip = true;
								this._heroEntity.IsHeroAnimating = true;
								this._direction = HeroDirection.Right;
								this._heroEntity.Position.X++;
								this.SetDestination();
							}
						}
						else if (this._currentArea.EastArea != "-")
						{
							//Edge of map - move to next area
							this._currentArea = this._world[this._currentArea.EastArea];
							this._heroEntity.Position.X = 0;
							this.SetDestination();
							this._heroEntity.Location = this._heroNextLocation;
						}
						break;

					case Keys.Left:
						//Are we at the edge of the map?
						if (this._heroEntity.Position.X > 0)
						{
							//Can we move to the next tile or not (blocking tile or monster)
							if (this.CheckNextTile(this._currentArea.TilesMap[this._heroEntity.Position.X - 1, this._heroEntity.Position.Y], this._heroEntity.Position.X - 1, this._heroEntity.Position.Y))
							{
								this._heroEntity.Velocity = new PointF(-GameState.EntitiesMoveSpeed, 0);
								this._heroEntity.Flip = false;
								this._heroEntity.IsHeroAnimating = true;
								this._direction = HeroDirection.Left;
								this._heroEntity.Position.X--;
								this.SetDestination();
							}
						}
						else if (this._currentArea.WestArea != "-")
						{
							this._currentArea = this._world[_currentArea.WestArea];
							this._heroEntity.Position.X = Area.MapSizeX - 1;
							this.SetDestination();
							this._heroEntity.Location = _heroNextLocation;
						}
						break;

					case Keys.Up:
						//Are we at the edge of the map?
						if (this._heroEntity.Position.Y > 0)
						{
							//Can we move to the next tile or not (blocking tile or monster)
							if (this.CheckNextTile(_currentArea.TilesMap[this._heroEntity.Position.X, this._heroEntity.Position.Y - 1], this._heroEntity.Position.X, this._heroEntity.Position.Y - 1))
							{
								this._heroEntity.Velocity = new PointF(0, -GameState.EntitiesMoveSpeed);
								this._heroEntity.IsHeroAnimating = true;
								this._direction = HeroDirection.Up;
								this._heroEntity.Position.Y--;
								this.SetDestination();
							}
						}
						else if (_currentArea.NorthArea != "-")
						{
							//Edge of map - move to next area
							this._currentArea = _world[_currentArea.NorthArea];
							this._heroEntity.Position.Y = Area.MapSizeY - 1;
							this.SetDestination();
							this._heroEntity.Location = _heroNextLocation;
						}
						break;

					case Keys.Down:
						//Are we at the edge of the map?
						if (this._heroEntity.Position.Y < Area.MapSizeY - 1)
						{
							//Can we move to the next tile or not (blocking tile or monster)
							if (this.CheckNextTile(_currentArea.TilesMap[this._heroEntity.Position.X, this._heroEntity.Position.Y + 1], this._heroEntity.Position.X, this._heroEntity.Position.Y + 1))
							{
								this._heroEntity.Velocity = new PointF(0, GameState.EntitiesMoveSpeed);
								this._heroEntity.IsHeroAnimating = true;
								this._direction = HeroDirection.Down;
								this._heroEntity.Position.Y++;
								this.SetDestination();
							}
						}
						else if (_currentArea.SouthArea != "-")
						{
							//Edge of map - move to next area
							this._currentArea = _world[_currentArea.SouthArea];
							this._heroEntity.Position.Y = 0;
							this.SetDestination();
							this._heroEntity.Location = _heroNextLocation;
						}
						break;

					case Keys.P:
						//Potion - if we have any
						this._heroEntity.DoMagic(this._currentArea, this._popups);
						_startFightTime = -1;
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

		private bool CheckDestination()
		{
			//Depending on the direction we are moving we check different bounds of the destination
			switch (this._direction)
			{
				case HeroDirection.Right:
					return (this._heroEntity.Location.X >= this._heroNextLocation.X);

				case HeroDirection.Left:
					return (this._heroEntity.Location.X <= this._heroNextLocation.X);

				case HeroDirection.Up:
					return (this._heroEntity.Location.Y <= this._heroNextLocation.Y);

				case HeroDirection.Down:
					return (this._heroEntity.Location.Y >= this._heroNextLocation.Y);
			}

			throw new ArgumentException("Direction is not set correctly");
		}

		private bool CheckNextTile(MapTile nextMapTile, int x, int y)
		{
			//See if there is a door we need to open
			if (this.CheckDoors(nextMapTile, x, y))
			{
				return false;
			}

			IEnemy enemy = nextMapTile.Sprite as IEnemy;
			//See if there is character to fight
			if (enemy != null)
			{
				this._gameState.Fight(_Random, _heroEntity, enemy, this._popups);
				this._startFightTime = -1;
				return false;
			}

			//If the next tile is not a blocker then we can move
			if (nextMapTile.IsPassable)
			{
				nextMapTile.OnPlayerMove(_heroEntity);
				return true;
			}

			return false;
		}

		private bool CheckDoors(MapTile mapTile, int x, int y)
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
				if (this._heroEntity.HasKey)
				{
					this._heroEntity.HasKey = false;
					obstacle.ChangeState();
					return false;
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Calculate the next position of the player
		/// </summary>
		private void SetDestination()
		{
			//Calculate the eventual sprite destination based on the area grid coordinates
			_heroNextLocation = new PointF(this._heroEntity.Position.X * Tile.TileSizeX + Area.AreaOffsetX,
											this._heroEntity.Position.Y * Tile.TileSizeY + Area.AreaOffsetY);
		}

		private enum HeroDirection
		{
			Left,
			Right,
			Up,
			Down
		}

		#endregion Methods
	}
}