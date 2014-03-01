namespace RolePlayingGame.Core.Human.Enemies
{
	internal abstract class Enemy : Human, IEnemy
	{
		#region Constructors

		public Enemy(float x, float y, Entity entity)
			: base(x, y, entity, false)
		{
			this.IsAnimationForced = true;
		}

		#endregion Constructors

		#region Properties

		public int StartingHealth { get; set; }

		public int Strength { get; set; }

		public int Defense { get; set; }

		#endregion Properties

		#region Methods

		/// <summary>
		/// Check if the instance of enemy is alive or not.
		/// </summary>
		/// <returns>Bool state</returns>
		public bool IsAlive()
		{
			return (this.Health > 0) ? true : false;
		}

		public abstract int SetHealth();

		public abstract int SetStrength();

		/// <summary>
		/// Decrease health of the current enemy. When the enemy died, returns experiance.
		/// </summary>
		/// <param name="enemyDamage">Current damage to decrease</param>
		/// <returns>Experiance</returns>
		public int GetDamage(int enemyDamage)
		{
			this.Health -= enemyDamage;
			if (this.IsAlive() == false)
			{
				this.Die();
				//Experience is the monsters max health divided by 3
				return this.StartingHealth / 3;
			}
			return 0;
		}

		#endregion Methods
	}
}