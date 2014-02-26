using RolePlayingGame.Core.Map;
using RolePlayingGame.Core.Map.Tiles;
using RolePlayingGame.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace RolePlayingGame.Core
{
	[Serializable]
	internal class Sprite : GameEntity, ISprite
	{
		#region Constants

		private readonly Color _defaultColorKey = Color.FromArgb(75, 75, 75);
		private static readonly PointF _DefaultAcceleration = new PointF(GameEngine.EntitiesMoveSpeed, GameEngine.EntitiesMoveSpeed);

		#endregion Constants

		#region Static

		public static int CalculateNextFrame(double gameTime, int framesCount)
		{
			return (int)((gameTime * GameEngine.FrameRate) % (double)framesCount);
		}

		#endregion Static

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
				this.SetColorKey(entity.ColorKey ?? this._defaultColorKey);
			}

			this.Flip = flip;
			this._frameRectangles = new List<Rectangle>();
			this.Frames = new List<Bitmap>();
			this.Velocity = PointF.Empty;
			this.Acceleration = _DefaultAcceleration;
			this.Location = new PointF(x * Tile.TileSizeX + Area.AreaOffsetX,
										y * Tile.TileSizeY + Area.AreaOffsetY);

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

			//Add in any acceleration
			this.Velocity.X += Math.Sign(this.Velocity.X) * this.Acceleration.X * (float)elapsedTime;
			this.Velocity.Y += Math.Sign(this.Velocity.Y) * this.Acceleration.Y * (float)elapsedTime;
		}

		/// <summary>
		/// Draw graphics on the screen
		/// </summary>
		/// <param name="renderer"></param>
		public override void Draw(IRenderer renderer)
		{
			//if (this.Category == EntityCategoryType.Door)
			//{
			//    var door = this as StaticItem;
			//    if (door.State == false)
			//    {
			//        var firstFrame = this.Frames.First();
			//        this.Frames.Clear();
			//        this.Frames.Add(firstFrame);
			//        var firstRec = this._frameRectangles[0];
			//        this._frameRectangles.Clear();
			//        this._frameRectangles.Add(firstRec);
			//    }
			//}

			//Draw the correct frame at the current point
			if (this._frameRectangles[this.CurrentFrame] == Rectangle.Empty)
			{
				renderer.DrawImage(this.Frames[this.CurrentFrame], this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
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

				renderer.DrawImage(
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