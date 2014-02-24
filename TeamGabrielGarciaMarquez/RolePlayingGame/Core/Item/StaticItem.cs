namespace RolePlayingGame.Core.Item
{
    internal class StaticItem : Item, IObstacle
    {
        public StaticItem(float x, float y, Entity entity, bool flip = false)
            : base(x, y, entity, flip)
        {

        }

        public bool IsStateChangable
        {
            get { throw new System.NotImplementedException(); }
        }

        public void ChangeState()
        {
            throw new System.NotImplementedException();
        }
    }
}