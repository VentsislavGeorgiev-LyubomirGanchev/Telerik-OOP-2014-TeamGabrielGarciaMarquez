namespace RolePlayingGame.Core.Human.Enemies
{
    public class Student : Enemies
    {
        #region Constants
        private const int HEALTH_COEFFICIENT = 2;
        private const int HEALTH_MULTIPLICATOR = 10;
        #endregion Constants

        #region Fields
        #endregion Fields

        #region Constructors
        public Student(string name, float row, float col, int level)
            : base(name, row, col, level)
        {
            this.Health = SetHealth();
        }
        #endregion Constructos

        #region Properties
        #endregion Properties

        #region Methods
        /// <summary>
        /// Initialize the the health of the Student. The health will increase her value depending of the student level! 
        /// </summary>
        /// <returns>Health in float</returns>
        public override int SetHealth()
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