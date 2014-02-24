namespace RolePlayingGame.Core
{
    internal class PointF
    {
        private static readonly PointF _Empty = new PointF(0, 0);

        public static PointF Empty
        {
            get
            {
                return (PointF)_Empty.MemberwiseClone();
            }
        }

        public float X { get; set; }

        public float Y { get; set; }

        public PointF(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}