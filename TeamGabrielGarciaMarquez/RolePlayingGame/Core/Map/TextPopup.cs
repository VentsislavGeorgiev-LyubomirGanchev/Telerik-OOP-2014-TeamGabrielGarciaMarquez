using RolePlayingGame.UI;
using System.Drawing;

namespace RolePlayingGame.Core.Map
{
	internal class TextPopup
	{
		#region Static

		private static readonly Font _Font = new Font("Arial", 18);
		private static readonly Brush _BlackBrush = new SolidBrush(Color.Black);
		private static readonly Brush _RedBrush = new SolidBrush(Color.Red);
		private static readonly Brush _WhiteBrush = new SolidBrush(Color.White);

		#endregion Static

		public TextPopup(float x, float y, string text)
		{
			this.X = x;
			this.Y = y;
			this.Text = text;
		}

		public float Y { get; set; }

		public float X { get; set; }

		public string Text { get; set; }

		public void Draw(IRenderer renderer)
		{
			var textBrush = _WhiteBrush;
			if (this.Text != GameState.MissMessage)
			{
				textBrush = _RedBrush;
			}

			//Draw 4 text offsets to get an outline
			renderer.DrawString(this.Text, _Font, _BlackBrush, this.X + 2, this.Y);
			renderer.DrawString(this.Text, _Font, _BlackBrush, this.X - 1, this.Y);
			renderer.DrawString(this.Text, _Font, _BlackBrush, this.X, this.Y + 2);
			renderer.DrawString(this.Text, _Font, _BlackBrush, this.X, this.Y - 2);

			//Draw the actual text
			renderer.DrawString(this.Text, _Font, textBrush, this.X, this.Y);
		}
	}
}