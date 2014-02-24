namespace RolePlayingGame.Core
{
    public struct TextPopup
    {
        public TextPopup(int x, int y, string text)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Text = text;
        }

        public int Y { get; set; }

        public int X { get; set; }

        public string Text { get; set; }
    }
}