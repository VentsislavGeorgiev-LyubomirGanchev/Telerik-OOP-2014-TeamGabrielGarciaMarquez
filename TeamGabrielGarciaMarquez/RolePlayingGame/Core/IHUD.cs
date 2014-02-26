using RolePlayingGame.Core.Human;
using RolePlayingGame.UI;

namespace RolePlayingGame.Core
{
	internal interface IHUD
	{
		bool GameIsWon { get; set; }
		int Health { get; set; }

		int Mana { get; set; }

		int Knowledge { get; set; }

		int Defense { get; set; }

		int Experience { get; set; }

		bool HasKey { get; set; }

		int Level { get; set; }

		void Draw(IRenderer renderer);

		void Update(IPlayer player);
	}
}