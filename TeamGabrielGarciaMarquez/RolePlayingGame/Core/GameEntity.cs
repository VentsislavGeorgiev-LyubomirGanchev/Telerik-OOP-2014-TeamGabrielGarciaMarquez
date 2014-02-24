using System.Drawing;

namespace RolePlayingGame.Core
{
    internal abstract class GameEntity : IRenderable
    {
        #region Fields

        public Entity Entity { get; private set; }

        public EntityCategoryType Category
        {
            get
            {
                return this.Entity.Category;
            }
        }

        public bool IsPassable
        {
            get
            {
                return this.Entity.IsPassable;
            }
        }

        #endregion Fields

        #region Methods

        public GameEntity(Entity entity)
        {
            this.Entity = entity;
        }

        public abstract void Update(double gameTime, double elapsedTime);

        public abstract void Draw(Graphics graphics);

        #endregion Methods
    }
}