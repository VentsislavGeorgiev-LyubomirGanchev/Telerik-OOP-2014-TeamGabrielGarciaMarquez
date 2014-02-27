using RolePlayingGame.Core.Human;
using System;

namespace RolePlayingGame.Core
{
	[Serializable]
	internal class SaveGameData
	{
		public SaveGameData(IPlayer player)
		{
			this.Health = player.Health;
			this.Mana = player.Mana;
			this.Knowledge = player.Knowledge;
			this.Defense = player.Defense;
			this.Experience = player.Experience;
			this.IsHeroFighting = player.IsHeroFighting;
			this.Level = player.Level;
			this.Location = player.Location;
			this.Position = player.Position;
		}

		public int Health { get; set; }

		public int Mana { get; set; }

		public int Knowledge { get; set; }

		public int Defense { get; set; }

		public int Experience { get; set; }

		public bool IsHeroFighting { get; set; }

		public int Level { get; set; }

		public PointF Location { get; set; }

		public Point Position { get; set; }
	}
}