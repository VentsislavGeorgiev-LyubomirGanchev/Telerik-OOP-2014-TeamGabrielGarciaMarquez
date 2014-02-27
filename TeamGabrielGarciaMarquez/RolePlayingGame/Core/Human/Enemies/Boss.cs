namespace RolePlayingGame.Core.Human.Enemies
{
	internal class Boss : Enemy, IBoss
	{
		#region Constants

		private const int HealthMultiplicator = 100;
		private const int StrengthDivisor = 4;

		#endregion Constants

		#region Fields

		#endregion Fields

		#region Constructors

		public Boss(float x, float y, EntityType type)
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

		public override void Die()
		{
			this.Health = 0;
			this.OnUpdateTile(EntityType.Key);
		}

		/// <summary>
		/// Initialize the the health of the Boss. The health will increase her value depending of the boss level!
		/// </summary>
		/// <returns>Health in float</returns>
		public override int SetHealth()
		{
			return this.Level * HealthMultiplicator;
		}

		public override int SetStrength()
		{
			return this.Health / StrengthDivisor;
		}

		#endregion Methods
	}
}