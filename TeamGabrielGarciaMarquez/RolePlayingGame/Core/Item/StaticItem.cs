namespace RolePlayingGame.Core.Item
{
    internal class StaticItem : Item, IObstacle
    {
        public bool IsStateChangable
        {
            get
            {
                return this.Entity.Category == EntityCategoryType.Door;
            }
        }

        public bool State { get; private set; }

        public StaticItem(float x, float y, Entity entity, bool flip = false)
            : base(x, y, entity, flip)
        {
        }

        public void ChangeState()
        {
            this.State = !this.State;
        }
    }
}