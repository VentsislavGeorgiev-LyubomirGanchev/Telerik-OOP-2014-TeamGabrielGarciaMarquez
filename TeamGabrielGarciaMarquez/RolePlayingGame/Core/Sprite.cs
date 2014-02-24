using RolePlayingGame.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace RolePlayingGame.Core
{
    internal class Sprite : GameEntity, IRenderable
    {
        #region Constants

        private readonly Color _defaultColorKey = Color.FromArgb(75, 75, 75);

        #endregion Constants

        #region Fields

        private ImageAttributes _attributes;

        private readonly List<Rectangle> _frameRectangles;

        private Color _colorKey;

        #endregion Fields

        #region Constructors

        public Sprite(float x, float y, Entity entity, bool flip = false)
            : base(entity)
        {
            if (entity.Tile.IsTransparent)
            {
                this.SetColorKey(this._defaultColorKey);
            }

            this.Flip = flip;
            this._frameRectangles = new List<Rectangle>();
            this.Frames = new List<Bitmap>();
            this.Velocity = PointF.Empty;
            this.Acceleration = PointF.Empty;
            this.Location = new PointF(x, y);

            var entityRectangle = entity.Tile.Rectangle;
            var entityFramesCount = entity.Tile.FramesCount;
            var entityBitmap = entity.Tile.Bitmap;
            this.Size = new SizeF(entityRectangle.Width / entityFramesCount, entityRectangle.Height);

            for (int i = 0; i < entityFramesCount; i++)
            {
                this.Frames.Add(entityBitmap);
                this._frameRectangles.Add(new Rectangle(entityRectangle.X + i * entityRectangle.Width / entityFramesCount,
                    entityRectangle.Y, entityRectangle.Width / entityFramesCount, entityRectangle.Height));
            }
        }

        #endregion Constructors

        #region Properties

        public PointF Acceleration { get; set; }

        public int CurrentFrame { get; set; }

        public bool Flip { get; set; }

        protected List<Bitmap> Frames { get; set; }

        public PointF Location { get; set; }

        public int FramesCount
        {
            get
            {
                return this.Frames.Count;
            }
        }

        public SizeF Size { get; set; }

        public PointF Velocity { get; set; }

        #endregion Properties

        #region Methods

        private void SetColorKey(Color value)
        {
            this._colorKey = value;
            //Set the color key for this sprite;
            this._attributes = new ImageAttributes();
            this._attributes.SetColorKey(this._colorKey, this._colorKey);
        }

        /// <summary>
        /// Update the instance of sprite
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="elapsedTime"></param>
        public override void Update(double gameTime, double elapsedTime)
        {
            //Move the sprite
            this.Location.X += this.Velocity.X * (float)elapsedTime;
            this.Location.Y += this.Velocity.Y * (float)elapsedTime;

            //TODO check why acceleration wasnt get any undefault values !!!
            //Add in any acceleration
            this.Velocity.X += Math.Sign(this.Velocity.X) * this.Acceleration.X * (float)elapsedTime;
            this.Velocity.Y += Math.Sign(this.Velocity.Y) * this.Acceleration.Y * (float)elapsedTime;
        }

        /// <summary>
        /// Draw graphics on the screen
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            //Draw the correct frame at the current point
            if (this._frameRectangles[this.CurrentFrame] == Rectangle.Empty)
            {
                graphics.DrawImage(this.Frames[this.CurrentFrame], this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
            }
            else
            {
                Rectangle outputRect = Rectangle.Empty;
                if (this.Flip)
                {
                    outputRect = new Rectangle(
                        (int)this.Location.X + (int)this.Size.Width, 
                        (int)this.Location.Y, -(int)this.Size.Width, 
                        (int)this.Size.Height);
                }
                else
                {
                    outputRect = new Rectangle(
                        (int)this.Location.X, 
                        (int)this.Location.Y, 
                        (int)this.Size.Width, 
                        (int)this.Size.Height);
                }

                graphics.DrawImage(
                    this.Frames[this.CurrentFrame], outputRect, 
                    this._frameRectangles[this.CurrentFrame].X, 
                    this._frameRectangles[this.CurrentFrame].Y,
                    this._frameRectangles[this.CurrentFrame].Width, 
                    this._frameRectangles[this.CurrentFrame].Height, 
                    GraphicsUnit.Pixel, 
                    this._attributes);
            }
        }

        #endregion Methods
    }
}