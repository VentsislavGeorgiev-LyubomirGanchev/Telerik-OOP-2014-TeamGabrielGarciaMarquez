namespace RolePlayingGame.Core.Human.Enemies
{
    internal class Student : Enemies
    {
        #region Constants
        private const int HealthCoefficient = 2;
        private const int HealthMultiplicator = 10;
        #endregion Constants

        #region Fields

        #endregion Fields

        #region Constructors

        public Student(float x, float y, EntityType type)
            : base(x, y, new Entity(type))
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
        public override int SetHealth()
        {
            return (this.Level * HealthMultiplicator) * HealthCoefficient;
        }

        public override void GetStrength()
        {
            //throw new System.NotImplementedException();
        }

        #endregion Methods
    }
}