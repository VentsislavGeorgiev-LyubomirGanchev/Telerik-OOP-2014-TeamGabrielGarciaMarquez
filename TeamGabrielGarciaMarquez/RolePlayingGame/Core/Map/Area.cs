using RolePlayingGame.UI;
using System;
using System.IO;

namespace RolePlayingGame.Core.Map
{
	/// <summary>
	/// Area defines the NxN grid that contains a set of MapTiles
	/// </summary>

	internal class Area : IArea
	{
		#region Constants

		public const int AreaOffsetX = 0;
		public const int AreaOffsetY = 0;
		public const int MapSizeX = 10;
		public const int MapSizeY = 10;

		public const int MapSizeXMaxIndex = MapSizeX - 1;
		public const int MapSizeYMaxIndex = MapSizeY - 1;

		public const int MapSizeXMinIndex = 0;
		public const int MapSizeYMinIndex = 0;

		#endregion Constants

		#region Fields

		private MapTile[,] _tilesMap;

		#endregion Fields

		public Area(StreamReader stream)
		{
			this._tilesMap = new MapTile[MapSizeX, MapSizeY];
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

					var backgroundSprite = SpriteFactory.Create(col, row, new Entity(entityKey));
					MapTile mapTile = new MapTile(backgroundSprite);
					this._tilesMap[col, row] = mapTile;
				}
			}

			//Read game objects until the blank line
			while (!stream.EndOfStream && (line = stream.ReadLine().Trim()) != string.Empty)
			{
				//Each line is an x, y coordinate and a entityKey
				//Look up the entity and set the sprite
				string[] elements = line.Split(',');
				int x = Convert.ToInt32(elements[0]);
				int y = Convert.ToInt32(elements[1]);
				var entityKey = elements[2].ToString();

				var foregroundSprite = SpriteFactory.Create(x, y, new Entity(entityKey));
				MapTile mapTile = this._tilesMap[x, y];
				mapTile.SetForegroundSprite(foregroundSprite);
			}
		}

		#region Properties

		public string Name { get; private set; }

		public string NorthArea { get; private set; }

		public string EastArea { get; private set; }

		public string SouthArea { get; private set; }

		public string WestArea { get; private set; }

		#endregion Properties

		public MapTile GetNextMapTile(Direction direction, Point position)
		{
			Point nextPosition = new Point(position);
			switch (direction)
			{
				case Direction.Left:
					nextPosition.X -= 1;
					break;

				case Direction.Right:
					nextPosition.X += 1;
					break;

				case Direction.Up:
					nextPosition.Y -= 1;
					break;

				case Direction.Down:
					nextPosition.Y += 1;
					break;

				default:
					throw new InvalidOperationException();
			}
			return this.GetMapTile(nextPosition);
		}

		public MapTile GetMapTile(Point position)
		{
			return this.GetMapTile(position.X, position.Y);
		}

		public MapTile GetMapTile(int x, int y)
		{
			return this._tilesMap[x, y];
		}

		public void Update(double gameTime, double elapsedTime)
		{
			//Update all the map tiles and any objects
			foreach (MapTile mapTile in this._tilesMap)
			{
				mapTile.Update(gameTime, elapsedTime);
			}
		}

		public void Draw(IRenderer renderer)
		{
			//And draw the map and any objects
			foreach (MapTile mapTile in this._tilesMap)
			{
				mapTile.Draw(renderer);
			}
		}
	}
}