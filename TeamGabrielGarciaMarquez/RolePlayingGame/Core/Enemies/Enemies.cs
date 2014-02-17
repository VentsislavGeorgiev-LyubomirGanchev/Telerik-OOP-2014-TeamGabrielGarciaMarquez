namespace RolePlayingGame.Core.Enemies
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
        public abstract float SetHealth();

        public abstract void GetStrength();
        #endregion Methods
    }
}