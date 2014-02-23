namespace RolePlayingGame.Core.Interfaces
{
    interface IObstacle
    {
        bool IsStateChangable { get; }

        void ChangeState();
    }
}