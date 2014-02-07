using System.Drawing;

namespace RolePlayingGame.Core
{
	public abstract class GameObject
	{
		public abstract void Update(double gameTime, double elapsedTime);

		public abstract void Draw(Graphics graphics);
	}
}