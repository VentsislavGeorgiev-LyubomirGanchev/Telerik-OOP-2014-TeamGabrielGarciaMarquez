namespace RolePlayingGame.Core.Human
{
    using System.Collections.Generic;
    using UI = RolePlayingGame.UI;
    using System;

    //TODO fix namespace
    public abstract class Human : UI.Tile
    {
        #region Const
        private const int LUCKY_SCOPE = 10;
        private const int LUCKY_NUMBER = 3;
        private const string MISS_MESSAGE = "miss";
        #endregion

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
                if (IsPositionAvailable(value, this._col))
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

        public int Health { get; set; }

        #region Methods
        public bool IsPositionAvailable(float row, float col)
        {
            //Need implementation
            return true;
        }

        //TODO fix parameters with interfaces
        public static void Fight(Random random, Human firstFighter, Human secondFighter)
        {
            var player = firstFighter as Player;
            var enemy = secondFighter as Enemies.Enemies;

            var popups = new List<TextPopup>();

            //A monsters attack ability is 1/2 their max health. Compare that to your armour
            //If you outclass them then there is still a chance of a lucky hit
            if (random.Next((enemy.Health / 2) + 1) >= player.Defense
                || (enemy.Health / 2 < player.Defense && random.Next(LUCKY_SCOPE) == LUCKY_NUMBER))
            {
                //Monsters do damage up to their max health - if they hit you.
                int playerDamage = random.Next(enemy.Health) + 1;
                player.Health -= playerDamage;

                if (player.Health <= 0)
                {
                    player.Health = 0;
                    //TODO
                    //_heroSprite = new Sprite(null, _heroPosition.X * Tile.TileSizeX + Area.AreaOffsetX,
                    //        _heroPosition.Y * Tile.TileSizeY + Area.AreaOffsetY,
                    //        _tiles["bon"].Bitmap, _tiles["bon"].Rectangle, _tiles["bon"].NumberOfFrames);
                    //_heroSprite.ColorKey = Color.FromArgb(75, 75, 75);
                }
                string message = playerDamage != 0 ? playerDamage.ToString() : MISS_MESSAGE;
                popups.Add(new TextPopup((int)player.Row + 40, (int)player.Col + 20, message));
            }

            //A monsters armour is 1/5 of their max health
            if (random.Next(player.Knowledge + 1) >= (enemy.Health / 5))
            {
                //Hero damage is up to twice the attack rating
                int enemyDamage = random.Next(player.Knowledge * 2) + 1;
                enemy.Health -= enemyDamage;
                if (enemy.Health <= 0)
                {
                    //Experience is the monsters max health
                    player.Experience += enemy.Health;
                    enemy.Health = 0;
                    //remove enemy
                    //mapTile.ObjectTile = _tiles["bon"];
                    //mapTile.SetObjectSprite(x, y);
                    return;
                }
                string message = enemyDamage != 0 ? enemyDamage.ToString() : MISS_MESSAGE;
                popups.Add(new TextPopup((int)enemy.Row + 40, (int)enemy.Col + 20, message));
            }
            else
            {
                popups.Add(new TextPopup((int)enemy.Row + 40, (int)enemy.Col + 20, MISS_MESSAGE));
            }
        }

        public void Move()
        {
            //TODO: implementation
            //throw new System.NotImplementedException();
        }
        #endregion Methods
    }
}