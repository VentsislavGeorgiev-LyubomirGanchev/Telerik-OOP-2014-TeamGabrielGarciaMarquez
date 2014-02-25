using System;
using System.Linq;

namespace RolePlayingGame.Core.Human
{
    internal class Player : Human, IMovable
    {
        #region Fields
        private int _level;
        #endregion

        #region Constructors
        //Just base constructor
        //Should be singleton design pattern
        public Player(float x, float y)
            : base(x, y, new Entity(EntityType.Player), true)
        {
        }
        #endregion

        #region Properties

        public int Mana { get; set; }

        public int Knowledge { get; set; }

        public int Defense { get; set; }

        public int Experience { get; set; }

        public int Level 
        {
            get
            {
                return this._level;
            }
            set
            {
                if (value < 0)
        {
                    throw new ArgumentException("Can't decrease level value!");
                }
                this._level = value;
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
