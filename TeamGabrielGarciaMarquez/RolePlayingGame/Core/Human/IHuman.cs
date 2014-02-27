namespace RolePlayingGame.Core.Human
{
	internal interface IHuman : ISprite
	{
		int Level { get; }

		void Die();
	}
}