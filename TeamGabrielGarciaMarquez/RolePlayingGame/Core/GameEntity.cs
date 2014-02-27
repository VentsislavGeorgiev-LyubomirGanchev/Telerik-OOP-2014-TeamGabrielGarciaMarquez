using RolePlayingGame.UI;
using System;

namespace RolePlayingGame.Core
{
	
	internal abstract class GameEntity : IRenderable, IGameEntity
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

		public abstract void Draw(IRenderer graphics);

		#endregion Methods
	}
}