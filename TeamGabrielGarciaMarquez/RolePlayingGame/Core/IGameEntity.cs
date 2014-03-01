namespace RolePlayingGame.Core
{
	internal interface IGameEntity : IRenderable
	{
		Entity Entity { get; }
	}
}