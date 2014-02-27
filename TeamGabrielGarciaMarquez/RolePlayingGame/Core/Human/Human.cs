using System;

namespace RolePlayingGame.Core.Human
{
	internal abstract class Human : Sprite, IHuman
	{
		#region Fields

		#endregion Fields

		#region Constructors

		public Human(float x, float y, Entity entity, bool flip)
			: base(x, y, entity, flip)
		{
			this.Position = new Point((int)x, (int)y);
			if (this.Entity.Special != string.Empty)
			{
				this.Level = Convert.ToInt32(this.Entity.Special);
			}
		}

		#endregion Constructors

		#region Properties

		public int Health { get; set; }

		public int Level { get; protected set; }

		#endregion Properties

		#region Methods

		public virtual void Die()
		{
			this.Health = 0;
			this.OnUpdateTile(EntityType.Bones);
		}

		public void Move()
		{
			//TODO: implementation
			//throw new System.NotImplementedException();
		}

		#endregion Methods
	}
}