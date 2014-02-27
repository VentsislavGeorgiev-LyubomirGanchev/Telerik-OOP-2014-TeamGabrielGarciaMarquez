namespace RolePlayingGame.Core.Human
{
	internal interface IPlayer : IHuman
	{
		int Health { get; set; }

		int Mana { get; set; }

		int Knowledge { get; set; }

		int Defense { get; set; }

		int Experience { get; set; }

		bool HasKey { get; set; }

		bool IsHeroFighting { get; set; }

		void LoadSaveGame(SaveGameData savegame);
	}
}