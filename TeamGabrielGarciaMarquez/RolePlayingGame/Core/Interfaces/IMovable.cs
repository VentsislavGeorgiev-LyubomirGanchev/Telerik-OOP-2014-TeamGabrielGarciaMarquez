namespace RolePlayingGame.Core.Interfaces
{
    interface IMovable
    {
        float Row { get; set; }
        float Col { get; set; }

        void Move();
    }
}