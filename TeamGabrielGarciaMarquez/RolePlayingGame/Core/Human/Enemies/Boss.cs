namespace RolePlayingGame.Core.Human.Enemies
{
    public class Boss : Enemies
    {
        #region Constants
        private const byte HEALTH_MULTIPLICATOR = 100;
        #endregion Constants

        #region Fields
        #endregion Fields

        #region Constructors
        public Boss(string name, float row, float col, int level)
            : base(name, row, col, level)
        {
            this.Health = SetHealth();
        }
        #endregion Constructos

        #region Properties

        #endregion Properties

        #region Methods
        /// <summary>
        /// Initialize the the health of the Boss. The health will increase her value depending of the boss level! 
        /// </summary>
        /// <returns>Health in float</returns>
        public override float SetHealth()
        {
            return this.Level * HEALTH_MULTIPLICATOR;
        }

        public override void GetStrength()
        {
            //throw new System.NotImplementedException();
        }
        #endregion Methods
    }
}