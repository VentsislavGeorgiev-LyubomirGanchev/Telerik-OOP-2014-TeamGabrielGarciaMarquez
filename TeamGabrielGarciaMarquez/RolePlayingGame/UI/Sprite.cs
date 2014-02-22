namespace RolePlayingGame.UI
{
    using RolePlayingGame.Core;
    using RolePlayingGame.Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class Sprite : IRenderable
    {
        #region Constants
        private readonly Color defaultColorKey = Color.FromArgb(75, 75, 75);
        #endregion
        #region Fields

        private PointF _acceleration;

        private ImageAttributes _attributes;

        private Color _colorKey;

        private List<Bitmap> _frames;

        private GameState _gameState;

        private PointF _location;

        private List<Rectangle> _rectangle;

        private PointF _velocity;
        #endregion

        #region Constructors
        public Sprite(GameState gameState, float x, float y, Bitmap bitmap, Rectangle rectangle, int numberAnimationFrames, bool flip = false)
        {
            this.ColorKey = defaultColorKey;
            this.Flip = flip;
            this._rectangle = new List<Rectangle>();
            this.Frames = new List<Bitmap>();

            this.GameStateVal = gameState;
            this.Location = new PointF(x, y);
            this.Size = new SizeF(rectangle.Width / numberAnimationFrames, rectangle.Height);

            for (int i = 0; i < numberAnimationFrames; i++)
            {
                _frames.Add(bitmap);
                _rectangle.Add(new Rectangle(rectangle.X + i * rectangle.Width / numberAnimationFrames,
                    rectangle.Y, rectangle.Width / numberAnimationFrames, rectangle.Height));
            }
        }
        #endregion

        #region Properties

        public PointF Acceleration
        {
            get
            {
                return _acceleration;
            }
            set
            {
                _acceleration = value;
            }
        }

        public Color ColorKey
        {
            get { return _colorKey; }
            set
            {
                _colorKey = value;
                //Set the color key for this sprite;
                _attributes = new ImageAttributes();
                _attributes.SetColorKey(_colorKey, _colorKey);
            }
        }

        public int CurrentFrame { get; set; }

        public bool Flip { get; set; }

        protected GameState GameStateVal
        {
            get
            {
                return this._gameState;
            }
            set
            {
                this._gameState = value;
            }
        }

        protected List<Bitmap> Frames
        {
            get
            {
                return this._frames;
            }
            set
            {
                this._frames = value;
            }
        }

        public PointF Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        public int NumberOfFrames
        {
            get
            {
                return _frames.Count;
            }
        }

        public SizeF Size { get; set; }

        public PointF Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Update the instance of sprite
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="elapsedTime"></param>
        public void Update(double gameTime, double elapsedTime)
        {
            //Move the sprite
            this._location.X += this.Velocity.X * (float)elapsedTime;
            this._location.Y += this.Velocity.Y * (float)elapsedTime;

            //TODO check why acceleration wasnt get any undefault values !!!
            //Add in any acceleration
            this._velocity.X += Math.Sign(this.Velocity.X) * this.Acceleration.X * (float)elapsedTime;
            this._velocity.Y += Math.Sign(this.Velocity.Y) * this.Acceleration.Y * (float)elapsedTime;
        }

        /// <summary>
        /// Draw graphics on the screen
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            //Draw the correct frame at the current point
            if (this._rectangle[CurrentFrame] == Rectangle.Empty)
            {
                graphics.DrawImage(this.Frames[CurrentFrame], this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
            }
            else
            {
                Rectangle outputRect = Rectangle.Empty;
                if (this.Flip)
                {
                    outputRect = new Rectangle((int)this.Location.X + (int)this.Size.Width, (int)this.Location.Y, -(int)this.Size.Width, (int)this.Size.Height);
                }
                else
                {
                    outputRect = new Rectangle((int)this.Location.X, (int)this.Location.Y, (int)this.Size.Width, (int)this.Size.Height);
                }

                if (this._attributes == null)
                {
                    graphics.DrawImage(this.Frames[CurrentFrame], outputRect, this._rectangle[CurrentFrame].X, this._rectangle[CurrentFrame].Y,
                        this._rectangle[CurrentFrame].Width, this._rectangle[CurrentFrame].Height, GraphicsUnit.Pixel);
                }
                else
                {
                    graphics.DrawImage(this.Frames[CurrentFrame], outputRect, this._rectangle[CurrentFrame].X, this._rectangle[CurrentFrame].Y,
                        this._rectangle[CurrentFrame].Width, this._rectangle[CurrentFrame].Height, GraphicsUnit.Pixel, this._attributes);
                }
            }
        }
        #endregion
    }
}