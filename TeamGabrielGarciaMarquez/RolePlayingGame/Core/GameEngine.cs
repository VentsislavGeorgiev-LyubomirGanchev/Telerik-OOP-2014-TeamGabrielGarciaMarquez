using RolePlayingGame.Core.Map;
using RolePlayingGame.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RolePlayingGame.Core
{
    [Serializable]
    internal class GameEngine
    {
        public SizeF GameArea;
        public World World;
        public int Attack;
        public int Armour;
        public int Level;
        public int Health;
        public int Treasure;
        public int Potions;
        public bool HasBrownKey;
        public bool HasGreenKey;
        public bool HasRedKey;
        public bool GameIsWon;

        private int _experience;
        private int _nextUpgrade;
        private Sprite _experienceSprite;
        private Sprite _attackSprite;
        private Sprite _armourSprite;
        private Sprite _healthSprite;
        private Sprite _treasureSprite;
        private Sprite _potionSprite;
        private Sprite _brownKeySprite;
        private Sprite _greenKeySprite;
        private Sprite _redKeySprite;

        private static readonly Font _Font = new Font("Arial", 24);
        private static readonly Brush _Brush = new SolidBrush(Color.White);
        private static readonly Random _Random = new Random();

        public GameEngine(SizeF gameArea)
        {
            GameArea = gameArea;

            //Create the sprites for the UI
            int y = 50;
            //_experienceSprite = new Sprite(580, y, new Entity("her"));
            //_healthSprite = new Sprite(580, y += 74, new Entity("fd1"));
            //_attackSprite = new Sprite(580, y += 74, new Entity("att"));
            //_armourSprite = new Sprite(580, y += 74, new Entity("arm"));
            //_treasureSprite = new Sprite(580, y += 74, new Entity("tr2"));
            //_potionSprite = new Sprite(580, y += 74, new Entity("pot"));
            //_brownKeySprite = new Sprite(580, y += 74, new Entity("kbr"));
            //_greenKeySprite = new Sprite(654, y, new Entity("kgr"));
            //_redKeySprite = new Sprite(728, y, new Entity("kre"));
        }

        //Experience property automatically upgrades your skill as the 'set' passes
        //the level threshold
        public int Experience
        {
            get
            {
                return _experience;
            }
            set
            {
                _experience = value;
                //If we hit the upgrade threshold then increase our abilities
                if (_experience > _nextUpgrade)
                {
                    Attack++;
                    Armour++;
                    //Each upgrade is a little harder to get
                    _nextUpgrade = _nextUpgrade + 20 * Level;
                    Level++;
                }
            }
        }

        public void Draw(IRenderer renderer)
        {
            World.Draw(renderer);

            //Draw the HUD
            //_experienceSprite.Draw(graphics);
            //_healthSprite.Draw(graphics);
            //_attackSprite.Draw(graphics);
            //_armourSprite.Draw(graphics);
            //_treasureSprite.Draw(graphics);
            //_potionSprite.Draw(graphics);
            //if (HasBrownKey) _brownKeySprite.Draw(graphics);
            //if (HasGreenKey) _greenKeySprite.Draw(graphics);
            //if (HasRedKey) _redKeySprite.Draw(graphics);
            int y = 65;
            renderer.DrawString(Experience.ToString(), _Font, _Brush, 650, y);
            renderer.DrawString(Health.ToString(), _Font, _Brush, 650, y += 74);
            renderer.DrawString(Attack.ToString(), _Font, _Brush, 650, y += 74);
            renderer.DrawString(Armour.ToString(), _Font, _Brush, 650, y += 74);
            renderer.DrawString(Treasure.ToString(), _Font, _Brush, 650, y += 74);
            renderer.DrawString(Potions.ToString(), _Font, _Brush, 650, y += 74);

            //If the game is over then display the end game message
            if (Health == 0)
            {
                renderer.DrawString("You died!", _Font, _Brush, 200, 250);
                renderer.DrawString("Press 's' to play again", _Font, _Brush, 100, 300);
            }

            //If the game is won then show congratulations
            if (GameIsWon)
            {
                renderer.DrawString("You Won!", _Font, _Brush, 200, 250);
                renderer.DrawString("Press 's' to play again", _Font, _Brush, 100, 300);
            }
        }

        public void Update(double gameTime, double elapsedTime)
        {
            World.Update(gameTime, elapsedTime);
        }

        public void Initialize()
        {
            Sounds.Start();

            //Create all the main gameobjects
            World = new World(this);

            //Reset the game state
            Attack = 1;
            Potions = 10;
            Armour = 1;
            Experience = 0;
            Level = 1;
            _nextUpgrade = 20;
            Health = 100;
            Treasure = 0;
            GameIsWon = false;
        }

        public void KeyDown(Keys keys)
        {
            //If the game is not over then allow the user to play
            if (Health > 0 && !GameIsWon)
            {
                World.KeyDown(keys);
            }
            else
            {
                //If game is over then allow S to restart
                if (keys == Keys.S)
                {
                    Initialize();
                }
            }
        }
    }
}