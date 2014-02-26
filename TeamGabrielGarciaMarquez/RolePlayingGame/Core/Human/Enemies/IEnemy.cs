namespace RolePlayingGame.Core.Human.Enemies
{
	internal interface IEnemy : IHuman
	{
		int Strength { get; set; }

		int Health { get; set; }

		int GetDamage(int enemyDamage);
	}
}