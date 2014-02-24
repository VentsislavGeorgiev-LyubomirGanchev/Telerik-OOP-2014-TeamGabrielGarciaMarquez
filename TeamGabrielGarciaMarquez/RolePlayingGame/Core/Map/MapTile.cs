using RolePlayingGame.Core.Map.Tiles;
using System.Drawing;

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
                return this._backgroundSprite.IsPassable;
            }
        }

        public bool IsStateChangable
        {
            get
            {
                //TODO FIX
                return this._backgroundSprite.Category == EntityCategoryType.WorldItems;
            }
        }

        public string Color
        {
            get
            {
                return "green";//this._backgroundSprite.Color;
            }
        }

        public EntityCategoryType Category
        {
            get
            {
                return this._backgroundSprite.Category;
            }
        }

        public int ObjectHealth; //A copy of the health of the tile so we remember how damage monsters are

        public void SetBackgroundSprite(int x, int y, Entity entity)
        {
            //Update the sprite
            _backgroundSprite = new Sprite(Area.AreaOffsetX + x * Tile.TileSizeX,
                                      Area.AreaOffsetY + y * Tile.TileSizeY,
                                      entity);
        }

        public void UpdateBackgroundTile(Tile newBackgroundTile)
        {
            this._backgroundSprite.Entity.Tile = newBackgroundTile;
        }

        public void SetForegroundSprite(int x, int y, Entity entity)
        {
            //Update the sprite
            this._foregroundSprite = new Sprite(Area.AreaOffsetX + x * Tile.TileSizeX,
                                      Area.AreaOffsetY + y * Tile.TileSizeY,
                                      entity);
        }

        public void UpdateForegroundTile(Tile newForegroundTile)
        {
            this._foregroundSprite.Entity.Tile = newForegroundTile;
        }

        public void OnPlayerMove(/*IPlayer player*/)
        {
            //if (this._foregroundTile == null) return;
            //switch (this._foregroundTile.Category)
            //{
            //    //Most objects change your stats in some way.
            //    case "armour":
            //        _gameState.Armour++;
            //        Sounds.Pickup();
            //        break;

            //    case "attack":
            //        _gameState.Attack++;
            //        Sounds.Pickup();
            //        break;

            //    case "food":
            //        _gameState.Health += 10;
            //        Sounds.Eat();
            //        break;

            //    case "treasure":
            //        _gameState.Treasure += 5;
            //        Sounds.Pickup();
            //        break;

            //    case "potion":
            //        _gameState.Potions++;
            //        Sounds.Pickup();
            //        break;

            //    case "key":
            //        if (this._foregroundTile.Color == "brown") _gameState.HasBrownKey = true;
            //        if (this._foregroundTile.Color == "green") _gameState.HasGreenKey = true;
            //        if (this._foregroundTile.Color == "red") _gameState.HasRedKey = true;
            //        Sounds.Pickup();
            //        break;

            //    case "fire":
            //        _gameState.Health -= 2;
            //        break;
            //}

            //Remove the object unless its bones or fire
            //if (_foregroundTile.Category != "fire" && _foregroundTile.Category != "bones" && _foregroundTile.Category != "character")
            //{
            //    this._foregroundTile = null;
            //    this._foregroundSprite = null;
            //}
        }

        public void Update(double gameTime, double elapsedTime)
        {
            this._backgroundSprite.Update(gameTime, elapsedTime);
            if (this._foregroundSprite != null)
            {
                if (this._foregroundSprite.FramesCount > 1)
                {
                    this._foregroundSprite.CurrentFrame = (int)((gameTime * 8.0) % (double)this._foregroundSprite.FramesCount);
                }
                this._foregroundSprite.Update(gameTime, elapsedTime);
            }
        }

        public void Draw(Graphics graphics)
        {
            this._backgroundSprite.Draw(graphics);
            if (this._foregroundSprite != null)
            {
                this._foregroundSprite.Draw(graphics);
            }
        }
    }
}