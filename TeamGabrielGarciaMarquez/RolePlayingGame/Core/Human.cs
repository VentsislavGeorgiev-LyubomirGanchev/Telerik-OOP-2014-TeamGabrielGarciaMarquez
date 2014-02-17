namespace RolePlayingGame.Core
{
    using RolePlayingGame.Core.Interfaces;
    using System;
    public abstract class Human: IMovable
    {
        #region Fields
        private float _row;
        private float _col;
        #endregion Fields

        #region Constructors
        public Human(float row, float col)
        {
            this.Row = row;
            this.Col = col;
        }
        #endregion Constructos

        #region Properties
        public float Row
        {
            get
            {
                return this._row;
            }
            set
            {
                if(IsPositionAvailable(value, this._col))
                {
                    this._row = value;
                }
                throw new IndexOutOfRangeException(String.Format("Can't set value: {0} for row of {1}", value, this.GetType()));
            }
        }

        public float Col
        {
            get
            {
                return this._col;
            }
            set
            {
                if (IsPositionAvailable(this._row, value))
                {
                    this._col = value;
                }
                throw new IndexOutOfRangeException(String.Format("Can't set value: {0} for column of {1}", value, this.GetType()));
            }
        }
        #endregion Properties

        public float Health { get; set; }

        #region Methods
        public bool IsPositionAvailable(float row, float col)
        { 
            //Need implementation
            return true;
        }

        public void Move()
        {
            //TODO: implementation
            //throw new System.NotImplementedException();
        }
        #endregion Methods
    }
}