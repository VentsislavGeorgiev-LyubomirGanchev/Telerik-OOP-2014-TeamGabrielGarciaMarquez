using System;

namespace RolePlayingGame.Core.Human
{
	internal abstract class Human : Sprite, IHuman, IMovable
	{
		#region Constructors

		public Human(float x, float y, Entity entity, bool flip)
			: base(x, y, entity, flip)
		{
			this.Position = new Point((int)x, (int)y);
			if (this.Entity.Special != string.Empty)
			{
				this.Level = Convert.ToInt32(this.Entity.Special);
			}

			this.FightStartTime = -1.0;
			this.NextLocation = this.Location;
		}

		#endregion Constructors

		#region Properties

		public double FightStartTime { get; set; }

		public int Health { get; set; }

		public int Level { get; protected set; }

		public Direction Direction { get; private set; }

		public PointF NextLocation { get; private set; }

		public bool IsFighting { get; set; }

		public bool IsAnimationForced { get; set; }

		#endregion Properties

		#region Methods

		public virtual void Die()
		{
			this.Health = 0;
			this.OnUpdateTile(EntityType.Bones);
		}

		public void MoveRight(int speed)
		{
			Move(Direction.Right, speed);
		}

		public void MoveLeft(int speed)
		{
			Move(Direction.Left, speed);
		}

		public void MoveUp(int speed)
		{
			Move(Direction.Up, speed);
		}

		public void MoveDown(int speed)
		{
			Move(Direction.Down, speed);
		}

		public void Move(Direction direction, int speed)
		{
			this.Direction = direction;
			this.IsAnimationEnabled = true;
			switch (direction)
			{
				case Direction.Left:
					this.Velocity = new PointF(-speed, 0);
					this.Flip = false;
					this.Position.X--;
					break;

				case Direction.Right:
					this.Velocity = new PointF(speed, 0);
					this.Flip = true;
					this.Position.X++;
					break;

				case Direction.Up:
					this.Velocity = new PointF(0, -speed);
					this.Position.Y--;
					break;

				case Direction.Down:
					this.Velocity = new PointF(0, speed);
					this.Position.Y++;
					break;

				default:
					throw new InvalidOperationException();
			}
		}

		public bool CanMoveRight(int mapSizeXMaxIndex)
		{
			return CanMoveDirection(Direction.Left, mapSizeXMaxIndex);
		}

		public bool CanMoveLeft(int mapSizeXMinIndex)
		{
			return CanMoveDirection(Direction.Right, mapSizeXMinIndex);
		}

		public bool CanMoveUp(int mapSizeYMinIndex)
		{
			return CanMoveDirection(Direction.Up, mapSizeYMinIndex);
		}

		public bool CanMoveDown(int mapSizeYMaxIndex)
		{
			return CanMoveDirection(Direction.Down, mapSizeYMaxIndex);
		}

		public bool CanMoveDirection(Direction direction, int indexToCheck)
		{
			bool canMove = false;
			switch (direction)
			{
				case Direction.Left:
					canMove = this.Position.X < indexToCheck;
					break;

				case Direction.Right:
					canMove = this.Position.X > indexToCheck;
					break;

				case Direction.Up:
					canMove = this.Position.Y > indexToCheck;
					break;

				case Direction.Down:
					canMove = this.Position.Y < indexToCheck;
					break;

				default:
					throw new InvalidOperationException();
			}
			return canMove;
		}

		/// <summary>
		/// Calculate the next position of the player
		/// </summary>
		public void CalculateSpriteLocation(bool setTheLocation, int tileSizeX, int tileSizeY, int offsetX, int offsetY)
		{
			//Calculate the eventual sprite destination based on the area grid coordinates
			this.NextLocation = new PointF(this.Position.X * tileSizeX + offsetX,
											this.Position.Y * tileSizeY + offsetY);
			if (setTheLocation)
			{
				this.Location = this.NextLocation;
			}
		}

		public override void Update(double gameTime, double elapsedTime)
		{
			base.Update(gameTime, elapsedTime);
			this.AnimateMovement(gameTime);
		}

		private void AnimateMovement(double gameTime)
		{
			//If the entity is moving we need to check if we are there yet
			if (!this.IsAnimationForced && this.IsAnimationEnabled && this.CheckDestinationReached())
			{
				//We have arrived. Stop moving and animating
				this.Location = this.NextLocation;
				this.Velocity = PointF.Empty;
				this.IsAnimationEnabled = false;
			}

			//The entity gets animated when moving or fighting
			if (this.IsAnimationEnabled || this.IsFighting)
			{
				this.CurrentFrameIndex = Sprite.CalculateNextFrame(gameTime, this.FramesCount);
				this.IsAnimationEnabled = true;
			}
			else
			{
				//Otherwise use frame 0
				this.CurrentFrameIndex = 0;
			}

			//If we are fighting then keep animating for a period of time
			if (this.IsFighting)
			{
				if (this.FightStartTime < 0)
				{
					this.FightStartTime = gameTime;
				}
				else
				{
					if (gameTime - this.FightStartTime > 1.0)
					{
						this.IsFighting = false;
						this.IsAnimationEnabled = false;
					}
				}
			}
		}

		private bool CheckDestinationReached()
		{
			bool arrived = false;
			//Depending on the direction we are moving we check different bounds of the destination
			switch (this.Direction)
			{
				case Direction.Right:
					arrived = this.Location.X >= this.NextLocation.X;
					break;

				case Direction.Left:
					arrived = this.Location.X <= this.NextLocation.X;
					break;

				case Direction.Up:
					arrived = this.Location.Y <= this.NextLocation.Y;
					break;

				case Direction.Down:
					arrived = this.Location.Y >= this.NextLocation.Y;
					break;

				default:
					throw new ArgumentException("Direction is not set correctly");
			}
			return arrived;
		}

		#endregion Methods
	}
}