using RolePlayingGame.Core.Human;
using RolePlayingGame.Core.Map;
using RolePlayingGame.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RolePlayingGame.Core
{
    [Serializable]
    internal class GameEngine : IPlayer
    {
        public const int FrameRate = 8;
        public const int EntitiesMoveSpeed = 200;

        public SizeF GameArea;
        public World World;
        public bool GameIsWon;

        //TODO Add Tile Experience to The HUD
        private readonly Sprite _experienceSprite;

        private readonly Sprite _healthSprite;
        private readonly Sprite _manaSprite;
        private readonly Sprite _knowledgeSprite;
        private readonly Sprite _defenseSprite;

        private static readonly Font _Font = new Font("Arial", 24);
        private static readonly Brush _Brush = new SolidBrush(Color.Black);

        public GameEngine(SizeF gameArea)
        {
            GameArea = gameArea;

            //Create the sprites for the UI
            float hudSpacing = 1.5f;
            PointF hudPosition = new PointF(10.5f, 1);
            _experienceSprite = SpriteFactory.Create(hudPosition.X, hudPosition.Y, new Entity(EntityType.Experience));
            _healthSprite = SpriteFactory.Create(hudPosition.X, hudPosition.Y += hudSpacing, new Entity(EntityType.Burger));
            _manaSprite = SpriteFactory.Create(hudPosition.X, hudPosition.Y += hudSpacing, new Entity(EntityType.Beer));
            _knowledgeSprite = SpriteFactory.Create(hudPosition.X, hudPosition.Y += hudSpacing, new Entity(EntityType.IntroCSharp));
            _defenseSprite = SpriteFactory.Create(hudPosition.X, hudPosition.Y += hudSpacing, new Entity(EntityType.Keyboard));
        }

        #region Properties
        public int Health { get; set; }

        public int Mana { get; set; }

        public int Knowledge { get; set; }

        public int Defense { get; set; }

        public int Experience { get; set; }

        public bool HasKey { get; set; }

        public int Level { get; set; }
        #endregion  

        #region Methods
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
        #endregion
    }
}