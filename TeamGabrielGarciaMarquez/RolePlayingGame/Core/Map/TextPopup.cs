namespace RolePlayingGame.Core.Map
{
    internal struct TextPopup
    {
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
    }
}