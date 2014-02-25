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
        public const int FrameRate = 8;
        public const int EntitiesMoveSpeed = 200;

        public SizeF GameArea;
        public World World;
        public int Mana;
        public int Knowledge;
        public int Level;
        public int Health;
        public int Defense;
        public int Potions;
        public bool HasBrownKey;
        public bool HasGreenKey;
        public bool HasRedKey;
        public bool GameIsWon;

        private int _experience;
        private int _nextUpgrade;

        //TODO Add Tile Experience to The HUD
        private Sprite _experienceSprite;
        private Sprite _healthSprite;
        private Sprite _manaSprite;
        private Sprite _knowledgeSprite;
        private Sprite _defenseSprite;

        private static readonly Font _Font = new Font("Arial", 24);
        private static readonly Brush _Brush = new SolidBrush(Color.Black);
        private static readonly Random _Random = new Random();

        public GameEngine(SizeF gameArea)
        {
            GameArea = gameArea;

            //Create the sprites for the UI
            float hudSpacing = 1.5f;
            PointF hudPosition = new PointF(10.5f, 1);
            _experienceSprite = SpriteFactory.Create(hudPosition.X, hudPosition.Y, new Entity(EntityType.Experience));
            _healthSprite = SpriteFactory.Create(hudPosition.X, hudPosition.Y += hudSpacing, new Entity(EntityType.Burger));
            _manaSprite = SpriteFactory.Create(hudPosition.X, hudPosition.Y += hudSpacing, new Entity(EntityType.Water));
            _knowledgeSprite = SpriteFactory.Create(hudPosition.X, hudPosition.Y += hudSpacing, new Entity(EntityType.DotNet));
            _defenseSprite = SpriteFactory.Create(hudPosition.X, hudPosition.Y += hudSpacing, new Entity(EntityType.Keyboard));
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
                    Mana++;
                    Knowledge++;
                    //Each upgrade is a little harder to get
                    _nextUpgrade = _nextUpgrade + 20 * Level;
                    Level++;
                }
            }
        }

        public void Draw(IRenderer renderer)
        {
            this.World.Draw(renderer);

            //Draw the HUD
            _experienceSprite.Draw(renderer);
            _healthSprite.Draw(renderer);
            _manaSprite.Draw(renderer);
            _knowledgeSprite.Draw(renderer);
            _defenseSprite.Draw(renderer);

            //TODO Add Keys

            int hudSpacing = 97;
            Point hudPosition = new Point(750, 78);
            renderer.DrawString(this.Experience.ToString(), _Font, _Brush, hudPosition.X, hudPosition.Y);
            renderer.DrawString(this.Health.ToString(), _Font, _Brush, hudPosition.X, hudPosition.Y += hudSpacing);
            renderer.DrawString(this.Mana.ToString(), _Font, _Brush, hudPosition.X, hudPosition.Y += hudSpacing);
            renderer.DrawString(this.Knowledge.ToString(), _Font, _Brush, hudPosition.X, hudPosition.Y += hudSpacing);
            renderer.DrawString(this.Defense.ToString(), _Font, _Brush, hudPosition.X, hudPosition.Y += hudSpacing);

            //If the game is over then display the end game message
            if (this.Health == 0)
            {
                renderer.DrawString("You died!", _Font, _Brush, 200, 250);
                renderer.DrawString("Press 's' to play again", _Font, _Brush, 100, 300);
            }

            //If the game is won then show congratulations
            if (this.GameIsWon)
            {
                renderer.DrawString("You Won!", _Font, _Brush, 200, 250);
                renderer.DrawString("Press 's' to play again", _Font, _Brush, 100, 300);
            }
        }

        public void Update(double gameTime, double elapsedTime)
        {
            this.World.Update(gameTime, elapsedTime);
        }

        public void Initialize()
        {
            Sounds.Start();

            //Create all the main gameobjects
            this.World = new World(this);

            //Reset the game state
            this.Mana = 1;
            this.Potions = 10;
            this.Knowledge = 1;
            this.Experience = 0;
            this.Level = 1;
            this._nextUpgrade = 20;
            this.Health = 100;
            this.Defense = 0;
            this.GameIsWon = false;
        }

        public void KeyDown(Keys keys)
        {
            //If the game is not over then allow the user to play
            if (this.Health > 0 && !this.GameIsWon)
            {
                this.World.KeyDown(keys);
            }
            else
            {
                //If game is over then allow S to restart
                if (keys == Keys.S)
                {
                    this.Initialize();
                }
            }
        }
    }
}