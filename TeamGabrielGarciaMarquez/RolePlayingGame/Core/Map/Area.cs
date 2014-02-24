using System;
using System.Drawing;
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

        public MapTile[,] Map = new MapTile[MapSizeX, MapSizeY];

        public string Name;
        public string NorthArea;
        public string EastArea;
        public string SouthArea;
        public string WestArea;

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
            for (int j = 0; j < MapSizeY; j++)
            {
                //Get a line of map characters
                line = stream.ReadLine();

                for (int i = 0; i < MapSizeX; i++)
                {
                    MapTile mapTile = new MapTile();
                    this.Map[i, j] = mapTile;
                    mapTile.SetBackgroundSprite(i, j, new Entity(line[i].ToString()));
                }
            }

            //Read game objects until the blank line
            while (!stream.EndOfStream && (line = stream.ReadLine().Trim()) != "")
            {
                //Each line is an x,y coordinate and a tile shortcut
                //Look up the tile and construct the sprite
                string[] elements = line.Split(',');
                int x = Convert.ToInt32(elements[0]);
                int y = Convert.ToInt32(elements[1]);
                var entityKey = line[2].ToString();
                MapTile mapTile = this.Map[x, y];
                mapTile.SetForegroundSprite(x, y, new Entity(entityKey));
            }
        }

        public void Update(double gameTime, double elapsedTime)
        {
            //Update all the map tiles and any objects
            foreach (MapTile mapTile in this.Map)
            {
                mapTile.Update(gameTime, elapsedTime);
            }
        }

        public void Draw(Graphics graphics)
        {
            //And draw the map and any objects
            foreach (MapTile mapTile in this.Map)
            {
                mapTile.Draw(graphics);
            }
        }
    }
}