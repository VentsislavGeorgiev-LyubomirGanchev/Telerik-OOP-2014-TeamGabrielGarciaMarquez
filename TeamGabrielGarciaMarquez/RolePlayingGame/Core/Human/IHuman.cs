namespace RolePlayingGame.Core.Human
{
	internal interface IHuman : ISprite
	{
		int Level { get; }

		Point Position { get; }

		void Die();
	}
}