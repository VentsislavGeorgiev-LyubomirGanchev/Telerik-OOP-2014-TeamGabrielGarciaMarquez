namespace RolePlayingGame.Core.Item
{
    interface IObstacle
    {
        bool IsStateChangable { get; }

        void ChangeState();

        bool State { get; }
    }
}