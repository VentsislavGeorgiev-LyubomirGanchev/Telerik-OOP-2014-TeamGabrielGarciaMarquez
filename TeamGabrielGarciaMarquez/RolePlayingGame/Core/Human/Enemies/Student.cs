namespace RolePlayingGame.Core.Human.Enemies
{
    internal class Student : Enemy
    {
        #region Constants

        private const int HealthMultiplicator = 20;
        private const int StrengthPercentage = 80;
		private const int DefenseDivisor = 5;
        #endregion Constants

        #region Fields
        #endregion Fields

        #region Constructors

        public Student(float x, float y, EntityType type)
            : base(x, y, new Entity(type))
        {
            this.Health = SetHealth();
            this.StartingHealth = this.Health;
            this.Strength = this.SetStrength();
			this.Defense = this.Health / DefenseDivisor;
        }

        #endregion Constructors

        #region Properties

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialize the the health of the Student. The health will increase her value depending of the student level! 
        /// </summary>
        /// <returns>Health in float</returns>
        public override int SetHealth()
        {
            return (this.Level * HealthMultiplicator);
        }

        public override int SetStrength()
        {
			return (int)(((float)this.Health / 100) * StrengthPercentage);
        }

        #endregion Methods
    }
}