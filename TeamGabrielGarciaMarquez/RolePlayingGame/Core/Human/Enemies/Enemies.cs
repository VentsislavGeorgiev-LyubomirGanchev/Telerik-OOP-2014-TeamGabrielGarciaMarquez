using System;

namespace RolePlayingGame.Core.Human.Enemies
{
    internal abstract class Enemies : Human
    {
        public int Level { get; private set; }

        #region Constructors

        public Enemies(int x, int y, Entity entity)
            : base(x, y, entity, false)
        {
            this.Level = Convert.ToInt32(this.Entity.Special);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Check if the instance of enemy is alive or not.
        /// </summary>
        /// <returns>Bool state</returns>
        public bool IsAlive()
        {
            return (this.Health > 0) ? true : false;
        }

        public abstract float SetHealth();

        public abstract void GetStrength();

        #endregion Methods
    }
}