namespace RolePlayingGame.Core.Enemies
{
    public class Boss : Enemies
    {
        #region Fields
        #endregion Fields

        #region Constructors
        public Boss(string name, float row, float col, int level)
            : base(name, row, col, level)
        {
            this.Health = SetHelth();
        }
        #endregion Constructos

        #region Properties

        #endregion Properties

        #region Methods
        //TODO: need implementation
        public override float SetHelth()
        {
            return 0;
            //throw new System.NotImplementedException();
        }

        public override void GetStrength()
        {
            //throw new System.NotImplementedException();
        }
        #endregion Methods
    }
}