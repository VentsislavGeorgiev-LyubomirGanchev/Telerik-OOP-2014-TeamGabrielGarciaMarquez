using System;
using RolePlayingGame.Core.Map.Tiles;

namespace RolePlayingGame.Core.Human.Enemies
{
    internal abstract class Enemy : Human
    {
        public int Level { get; private set; }

        #region Constructors

        public Enemy(float x, float y, Entity entity)
            : base(x, y, entity, false)
        {
            this.Level = Convert.ToInt32(this.Entity.Special);
        }

        #endregion Constructors

        #region Properties

        public int StartingHealth { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Check if the instance of enemy is alive or not.
        /// </summary>
        /// <returns>Bool state</returns>
        public bool IsAlive()
        {
            return (this.Health > 0) ? true : false;
        }

        public abstract int SetHealth();

        public abstract void GetStrength();

        /// <summary>
        /// Decrease health of the current enemy. When the enemy died, returns experiance.
        /// </summary>
        /// <param name="enemyDamage">Current damage to decrease</param>
        /// <returns>Experiance</returns>
        public int GetDamage(int enemyDamage)
        {
            this.Health -= enemyDamage;
            if (this.IsAlive() == false)
            {
                this.Health = 0;
                //Remove the enemy and set bones
                this.Entity.Tile = new Tile(Entity.TileDescriptions[EntityType.Bones.ToString()]);

                //Experience is the monsters max health
                return this.StartingHealth;
            }
            return 0;
        }

        #endregion Methods
    }
}