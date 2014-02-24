﻿namespace RolePlayingGame.Core.Human.Enemies
{
    internal class Student : Enemies
    {
        #region Constants

        private const float HEALTH_COEFFICIENT = 1.5F;
        private const byte HEALTH_MULTIPLICATOR = 10;

        #endregion Constants

        #region Fields

        #endregion Fields

        #region Constructors

        public Student(int x, int y)
            : base(x, y, new Entity(EntityType.Student))
        {
            this.Health = SetHealth();
        }

        #endregion Constructors

        #region Properties

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialize the the health of the Student. The health will increase her value depending of the student level!
        /// </summary>
        /// <returns>Health in float</returns>
        public override float SetHealth()
        {
            return (this.Level * HEALTH_MULTIPLICATOR) * HEALTH_COEFFICIENT;
        }

        public override void GetStrength()
        {
            //throw new System.NotImplementedException();
        }

        #endregion Methods
    }
}