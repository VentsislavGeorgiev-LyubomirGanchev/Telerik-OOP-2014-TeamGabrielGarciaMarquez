using RolePlayingGame.Core.Human;
using RolePlayingGame.UI;
using System.Collections.Generic;
using System.IO;

namespace RolePlayingGame.Core.Map
{
	internal abstract class World : IRenderable
	{
		#region Constants

		private const string MapFilePath = @"Content\map.txt";
		private const string StartingAreaKey = "start";

		#endregion Constants

		public World(SaveGameData savegame = null)
		{
			this.WorldMap = new Dictionary<string, IArea>();

			//Read in the map file
			this.ReadMapFile(MapFilePath);

			//Find the start point
			this.CurrentArea = this.WorldMap[StartingAreaKey];
		}

		#region Properties

		protected IArea CurrentArea { get; set; }

		protected Dictionary<string, IArea> WorldMap { get; private set; }

		#endregion Properties

		#region Methods

		public virtual void Update(double gameTime, double elapsedTime)
		{
			//We only actually update the current area the rest all 'sleep'
			this.CurrentArea.Update(gameTime, elapsedTime);
		}

		public virtual void Draw(IRenderer renderer)
		{
			this.CurrentArea.Draw(renderer);
		}

		protected SaveGameData SaveGame(IPlayer player)
		{
			var savegame = new SaveGameData(player);
			savegame.Area = this.CurrentArea.Name;
			return savegame;
		}

		private void ReadMapFile(string filePath)
		{
			using (StreamReader stream = new StreamReader(filePath))
			{
				while (!stream.EndOfStream)
				{
					//Each area constructor will consume just one area
					Area area = new Area(stream);
					this.WorldMap.Add(area.Name, area);
				}
			}
		}

		#endregion Methods
	}
}