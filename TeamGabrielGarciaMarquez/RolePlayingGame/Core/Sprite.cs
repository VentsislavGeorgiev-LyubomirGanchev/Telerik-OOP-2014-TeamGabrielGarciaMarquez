using RolePlayingGame.Core.Map;
using RolePlayingGame.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace RolePlayingGame.Core
{
	internal delegate void EntityTypeEventHandler(EntityType entityType, Point location);

	internal class Sprite : GameEntity, ISprite
	{
		#region Constants

		private readonly Color _defaultColorKey = Color.FromArgb(75, 75, 75);
		private static readonly PointF _DefaultAcceleration = new PointF(GameState.EntitiesMoveSpeed, GameState.EntitiesMoveSpeed);

		#endregion Constants

		#region Static

		public static int CalculateNextFrame(double gameTime, int framesCount)
		{
			return (int)((gameTime * GameState.FrameRate) % (double)framesCount);
		}

		#endregion Static

		#region Fields

		private ImageAttributes _attributes;

		private List<Rectangle> _frameRectangles;

		private List<Bitmap> _frames;

		private bool _animationEnded;

		private Color _colorKey;

		#endregion Fields

		#region Constructors

		public Sprite(float x, float y, Entity entity, bool flip = false)
			: base(entity)
		{
			this.IsAnimationEnabled = true;
			this.CreateFrames(x, y, entity, flip);
		}

		#endregion Constructors

		#region Properties

		public event EntityTypeEventHandler UpdateSprite;

		public PointF Acceleration { get; set; }

		public int CurrentFrameIndex { get; set; }

		public bool Flip { get; set; }

		public PointF Location { get; set; }

		public Point Position { get; set; }

		public bool IsAnimationEnabled { get; set; }

		public bool IsStateChangable
		{
			get
			{
				return this.Entity.Category == EntityCategoryType.Door;
			}
		}

		public int FramesCount
		{
			get
			{
				return this._frames.Count;
			}
		}

		public SizeF Size { get; set; }

		public PointF Velocity { get; set; }

		#endregion Properties

		#region Methods

		protected void OnUpdateTile(EntityType type)
		{
			if (this.UpdateSprite != null)
			{
				this.UpdateSprite(type, this.Position);
			}
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
			int currentFrameIndex = this.CurrentFrameIndex;
			if (!this.IsAnimationEnabled)
			{
				currentFrameIndex = 0;
			}
			else
			{
				if (!this._animationEnded && this.IsStateChangable && currentFrameIndex == this._frames.Count - 1)
				{
					this._animationEnded = true;
					this.Entity.IsPassable = true;
				}

				if (this._animationEnded)
				{
					currentFrameIndex = this._frames.Count - 1;
				}
			}

			var currentFrameRectangle = this._frameRectangles[currentFrameIndex];
			var currentFrame = this._frames[currentFrameIndex];

			//Draw the correct frame at the current point
			if (currentFrameRectangle == Rectangle.Empty)
			{
				renderer.DrawImage(currentFrame, this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
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
					currentFrame, outputRect,
					currentFrameRectangle.X,
					currentFrameRectangle.Y,
					currentFrameRectangle.Width,
					currentFrameRectangle.Height,
					GraphicsUnit.Pixel,
					this._attributes);
			}
		}

		protected void CreateFrames(float x, float y, Entity entity, bool flip = false)
		{
			if (entity.Tile.IsTransparent)
			{
				this.SetColorKey(entity.ColorKey ?? this._defaultColorKey);
			}

			this.Flip = flip;
			this._frameRectangles = new List<Rectangle>();
			this._frames = new List<Bitmap>();
			this.Velocity = PointF.Empty;
			this.Acceleration = _DefaultAcceleration;

			if (x > Area.MapSizeX && y > Area.MapSizeY)
			{
				this.Location = new PointF(x, y);
			}
			else
			{
				this.Position = new Point((int)x, (int)y);
				this.Location = new PointF(x * Tile.TileSizeX + Area.AreaOffsetX,
											y * Tile.TileSizeY + Area.AreaOffsetY);
			}

			var entityRectangle = entity.Tile.Rectangle;
			var entityFramesCount = entity.Tile.FramesCount;
			var entityBitmap = entity.Tile.Bitmap;
			this.Size = new SizeF(entityRectangle.Width / entityFramesCount, entityRectangle.Height);

			for (int i = 0; i < entityFramesCount; i++)
			{
				this._frames.Add(entityBitmap);
				this._frameRectangles.Add(new Rectangle(entityRectangle.X + i * entityRectangle.Width / entityFramesCount,
					entityRectangle.Y, entityRectangle.Width / entityFramesCount, entityRectangle.Height));
			}
		}

		private void SetColorKey(Color value)
		{
			this._colorKey = value;
			//Set the color key for this sprite;
			this._attributes = new ImageAttributes();
			this._attributes.SetColorKey(this._colorKey, this._colorKey);
		}

		#endregion Methods
	}
}