namespace RolePlayingGame.Core.Human.Enemies
{
    internal class Student : Enemy
    {
        #region Constants
        private const int HealthCoefficient = 2;
        private const int HealthMultiplicator = 12;
        private const int StrengthDivisor = 2;
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
            return (this.Level * HealthMultiplicator) * HealthCoefficient;
        }

        public override int SetStrength()
        {
            return this.Health / StrengthDivisor;
        }

        #endregion Methods
    }
}