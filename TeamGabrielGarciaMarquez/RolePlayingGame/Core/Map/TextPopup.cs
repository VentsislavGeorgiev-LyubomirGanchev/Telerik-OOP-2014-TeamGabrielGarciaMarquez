using RolePlayingGame.UI;
using System.Drawing;

namespace RolePlayingGame.Core.Map
{
	internal struct TextPopup
	{
		#region Static

		private static readonly Font _Font = new Font("Arial", 18);
		private static readonly Brush _BlackBrush = new SolidBrush(Color.Black);
		private static readonly Brush _RedBrush = new SolidBrush(Color.Red);
		private static readonly Brush _WhiteBrush = new SolidBrush(Color.White);

		#endregion Static

		public TextPopup(float x, float y, string text)
			: this()
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

			//Draw the actual text
			renderer.DrawStringWithOutline(this.Text, _Font, textBrush, _BlackBrush, this.X, this.Y);
		}
	}
}