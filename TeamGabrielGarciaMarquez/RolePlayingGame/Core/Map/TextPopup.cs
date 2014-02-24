namespace RolePlayingGame.Core.Map
{
    internal struct TextPopup
    {
        public int X;
        public int Y;
        public string Text;

        public TextPopup(int x, int y, string text)
        {
            this.X = x;
            this.Y = y;
            this.Text = text;
        }
    }
}