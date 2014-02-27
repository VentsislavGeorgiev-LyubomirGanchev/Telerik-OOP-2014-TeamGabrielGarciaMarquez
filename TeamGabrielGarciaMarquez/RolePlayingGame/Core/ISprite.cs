namespace RolePlayingGame.Core
{
	internal interface ISprite : IGameEntity
	{
		PointF Location { get; set; }

		Point Position { get; }
	}
}