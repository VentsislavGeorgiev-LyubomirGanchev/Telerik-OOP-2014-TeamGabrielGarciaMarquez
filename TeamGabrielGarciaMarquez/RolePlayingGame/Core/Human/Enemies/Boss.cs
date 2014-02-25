namespace RolePlayingGame.Core.Human.Enemies
{
    internal class Boss : Enemies
    {
        #region Constants

        private const byte HealthMultiplicator = 100;

        #endregion Constants

        #region Fields

        #endregion Fields

        #region Constructors

        public Boss(float x, float y, EntityType type)
            : base(x, y, new Entity(type))
        {
            this.Health = SetHealth();
        }

        #endregion Constructors

        #region Properties

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialize the the health of the Boss. The health will increase her value depending of the boss level!
        /// </summary>
        /// <returns>Health in float</returns>
        public override int SetHealth()
        {
            return this.Level * HealthMultiplicator;
        }

        public override void GetStrength()
        {
            //throw new System.NotImplementedException();
        }

        #endregion Methods
    }
}