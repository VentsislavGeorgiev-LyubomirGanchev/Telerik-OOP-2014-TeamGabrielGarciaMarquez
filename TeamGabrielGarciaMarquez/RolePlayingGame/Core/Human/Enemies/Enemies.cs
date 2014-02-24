namespace RolePlayingGame.Core.Human.Enemies
{
    public abstract class Enemies : Human
    {
        #region Fields
        #endregion Fields

        #region Constructors
        public Enemies(string name, float row, float col, int level)
            : base(row, col)
        {
            this.Name = name;
            this.Level = level;
        }
        #endregion Constructos

        #region Properties

        public string Name { get; private set; }
        public int Level { get; private set; }

        #endregion Properties

        #region Methods
        /// <summary>
        /// Check if the instance of enemy is alive or not.
        /// </summary>
        /// <returns>Bool state</returns>
        public bool IsAlive()
        {
            if (this.Health > 0)
            {
                return true;
            }
            else return false;
        }

        public abstract int SetHealth();

        public abstract void GetStrength();
        #endregion Methods
    }
}