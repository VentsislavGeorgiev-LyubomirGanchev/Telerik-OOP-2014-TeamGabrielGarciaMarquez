using RolePlayingGame.UI;
using System;
using System.IO;

namespace RolePlayingGame.Core.Map
{
    /// <summary>
    /// Area defines the 8x8 grid that contains a set of MapTiles
    /// </summary>
    internal class Area : IRenderable
    {
        public const int AreaOffsetX = 30;
        public const int AreaOffsetY = 50;
        public const int MapSizeX = 8;
        public const int MapSizeY = 8;

        public MapTile[,] TilesMap = new MapTile[MapSizeX, MapSizeY];

        public string Name { get; private set; }

        public string NorthArea { get; private set; }

        public string EastArea { get; private set; }

        public string SouthArea { get; private set; }

        public string WestArea { get; private set; }

        public Area(StreamReader stream)
        {
            string line;

            //1st line is the name
            this.Name = stream.ReadLine().ToLower();

            //next 4 lines are which areas you go for N,E,S,W
            this.NorthArea = stream.ReadLine().ToLower();
            this.EastArea = stream.ReadLine().ToLower();
            this.SouthArea = stream.ReadLine().ToLower();
            this.WestArea = stream.ReadLine().ToLower();

            //Read in 8 lines of 8 characters each. Look up the tile and make the
            //matching sprite
            for (int row = 0; row < MapSizeY; row++)
            {
                //Get a line of map characters
                line = stream.ReadLine();

                for (int col = 0; col < MapSizeX; col++)
                {
                    var entityKey = line[col].ToString();
                    MapTile mapTile = new MapTile(col, row, new Entity(entityKey));
                    this.TilesMap[col, row] = mapTile;
                }
            }

            //Read game objects until the blank line
            while (!stream.EndOfStream && (line = stream.ReadLine().Trim()) != "")
            {
                //Each line is an x, y coordinate and a entityKey
                //Look up the entity and set the sprite
                string[] elements = line.Split(',');
                int x = Convert.ToInt32(elements[0]);
                int y = Convert.ToInt32(elements[1]);
                var entityKey = elements[2].ToString();
                MapTile mapTile = this.TilesMap[x, y];
                mapTile.SetForegroundSprite(x, y, new Entity(entityKey));
            }
        }

        public void Update(double gameTime, double elapsedTime)
        {
            //Update all the map tiles and any objects
            foreach (MapTile mapTile in this.TilesMap)
            {
                mapTile.Update(gameTime, elapsedTime);
            }
        }

        public void Draw(IRenderer renderer)
        {
            //And draw the map and any objects
            foreach (MapTile mapTile in this.TilesMap)
            {
                mapTile.Draw(renderer);
            }
        }
    }
}