using RolePlayingGame.Core.Human;
using RolePlayingGame.Core.Human.Enemies;
using RolePlayingGame.Core.Map.Tiles;
using RolePlayingGame.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Map
{
    internal class World : IRenderable
    {
        #region Const
        private const string MapFilePath = @"Content\map.txt";
        private const string StartArea = "start";
        #endregion

        #region Fields
        private readonly Dictionary<string, Area> _world = new Dictionary<string, Area>();
        private Area _currentArea;
        private Player _heroEntity;
        private double _startFightTime = -1.0;
        private PointF _heroNextLocation;
        private HeroDirection _direction;
        private readonly GameEngine _gameState;

        private static readonly Font _Font = new Font("Arial", 18);
        private static readonly Brush _WhiteBrush = new SolidBrush(Color.White);
        private static readonly Brush _BlackBrush = new SolidBrush(Color.Red);
        private static readonly Random _Random = new Random();
        #endregion 

        public World(GameEngine gameState)
        {
            //Main pool for all popups in the game.
            this.Popups = new List<TextPopup>();

            this._gameState = gameState;

            //Read in the map file
            this.ReadMapfile(MapFilePath);

            //Find the start point
            this._currentArea = this._world[StartArea];

            //Create and position the hero character
            var tilesMap = this._currentArea.TilesMap;
            for (int row = 0; row < tilesMap.GetLength(0); row++)
            {
                for (int col = 0; col < tilesMap.GetLength(1); col++)
                {
                    var mapTile = tilesMap[row, col];
                    if (mapTile.Type.HasValue && mapTile.Type == EntityType.Player)
                    {
                        this._heroEntity = mapTile.Sprite as Player;
                        this._heroEntity.UpdateTheHud(this._gameState);
                        mapTile.SetForegroundSprite(null);
                    }
                }
            }
        }

        #region Properties
        public IList<TextPopup> Popups { get; set; }

        #endregion

        #region Methods
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

                //Check if there is anything on this square
                this.CheckObjects();
            }

            //The hero gets animated when moving or fighting
            if (this._heroEntity.IsHeroAnimating || this._heroEntity.IsHeroFighting)
            {
                this._heroEntity.CurrentFrame = Sprite.CalculateNextFrame(gameTime, this._heroEntity.FramesCount);
            }
            else
            {
                //Otherwise use frame 0
                this._heroEntity.CurrentFrame = 0;
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
            this._heroEntity.UpdateTheHud(this._gameState);
        }

        public void Draw(IRenderer renderer)
        {
            this._currentArea.Draw(renderer);
            this._heroEntity.Draw(renderer);

            //If we are fighting then draw the damage
            if (this._heroEntity.IsHeroFighting)
            {
                foreach (TextPopup popup in this.Popups)
                {
                    //Draw 4 text offsets to get an outline
                    renderer.DrawString(popup.Text, _Font, _WhiteBrush, popup.X + 2, popup.Y);
                    renderer.DrawString(popup.Text, _Font, _WhiteBrush, popup.X - 1, popup.Y);
                    renderer.DrawString(popup.Text, _Font, _WhiteBrush, popup.X, popup.Y + 2);
                    renderer.DrawString(popup.Text, _Font, _WhiteBrush, popup.X, popup.Y - 2);

                    //Draw the actual text
                    renderer.DrawString(popup.Text, _Font, _BlackBrush, popup.X, popup.Y);
                }
            }
        }

        public void KeyDown(Keys key)
        {
            //Ignore keypresses while we are animating or fighting
            if (!this._heroEntity.IsHeroAnimating && !this._heroEntity.IsHeroFighting)
            {
                switch (key)
                {
                    case Keys.Right:
                        //Are we at the edge of the map?
                        if (this._heroEntity.Position.X < Area.MapSizeX - 1)
                        {
                            //Can we move to the next tile or not (blocking tile or monster)
                            if (this.CheckNextTile(this._currentArea.TilesMap[this._heroEntity.Position.X + 1, this._heroEntity.Position.Y], this._heroEntity.Position.X + 1, this._heroEntity.Position.Y))
                            {
                                this._heroEntity.Velocity = new PointF(GameEngine.EntitiesMoveSpeed, 0);
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
                                this._heroEntity.Velocity = new PointF(-GameEngine.EntitiesMoveSpeed, 0);
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
                                this._heroEntity.Velocity = new PointF(0, -GameEngine.EntitiesMoveSpeed);
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
                                this._heroEntity.Velocity = new PointF(0, GameEngine.EntitiesMoveSpeed);
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
                        this._heroEntity.DoMagic(this._currentArea, this.Popups);
                        break;
                }
            }
        }

        private void ReadMapfile(string mapFile)
        {
            using (StreamReader stream = new StreamReader(mapFile))
            {
                while (!stream.EndOfStream)
                {
                    //Each area constructor will consume just one area
                    Area area = new Area(stream);
                    this._world.Add(area.Name, area);
                }
            }
        }

        private void CheckObjects()
        {
            // MapTile.OnPlayerMove
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
            this.CheckDoors(nextMapTile, x, y);

            Enemy enemy = nextMapTile.Sprite as Enemy;
            //See if there is character to fight
            if (enemy != null)
            {
                Human.Human.Fight(_Random, _heroEntity, enemy, this.Popups);
                _startFightTime = -1;
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

        private bool DamageMonster(int damage, MapTile mapTile, int x, int y)
        {
            //Do some damage, and remove the monster if its dead
            bool returnValue = false; //monster not dead

            //Set the monster health if its not already set
            //if (mapTile.ObjectHealth == 0)
            //{
            //    //mapTile.ObjectHealth = mapTile.ForegroundTile.Health;
            //}

            //mapTile.ObjectHealth -= damage;

            //if (mapTile.ObjectHealth <= 0)
            //{
            //    mapTile.ObjectHealth = 0;
            //    //Experience is the monsters max health
            //    //_gameState.Experience += mapTile.ForegroundTile.Health;

            //    //Remove the monster and replace with bones
            //    //mapTile.UpdateForegroundTile(_tiles["bon"]);
            //    //mapTile.SetForegroundSprite(x, y);
            //    returnValue = true; //monster is dead
            //}

            //_popups.Add(new TextPopup((int)mapTile.Location.X + 40, (int)mapTile.Location.Y + 20, (damage != 0) ? damage.ToString() : "miss"));

            return returnValue;
        }

        private void CheckDoors(MapTile mapTile, int x, int y)
        {
            //If the next tile is a closed door then check if we have the key
            if (mapTile.IsStateChangable && mapTile.IsPassable)
            {
                //For each key if it matches then open the door by switching the sprite & sprite to its matching open version
                if (_gameState.HasKey)
                {
                    //Open the door
                    //mapTile.SetBackgroundSprite(x, y, _tiles["E"]);
                }
            }
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
        #endregion
    }
}