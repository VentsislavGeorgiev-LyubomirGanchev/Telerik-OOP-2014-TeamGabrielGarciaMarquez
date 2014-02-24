namespace RolePlayingGame.Core.Human
{
    //TODO fix namespace
    internal abstract class Human : Sprite, IRenderable
    {
        #region Fields

        public Point Position { get; private set; }

        #endregion Fields

        #region Constructors

        public Human(int x, int y, Entity entity, bool flip)
            : base(x, y, entity, flip)
        {
            this.Position = new Point(x, y);
        }

        #endregion Constructors

        public float Health { get; set; }

        #region Methods

        public void Move()
        {
            //TODO: implementation
            //throw new System.NotImplementedException();
        }

        #endregion Methods
    }
}