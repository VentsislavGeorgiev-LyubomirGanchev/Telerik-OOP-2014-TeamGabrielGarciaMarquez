using RolePlayingGame.Core.Interfaces;

namespace RolePlayingGame.Core.Human
{
    internal class Player : Human, IMovable
    {
        //Should be singleton design pattern
        public Player(int x, int y)
            : base(x, y, new Entity(EntityType.Player), true)
        {
        }
    }
}