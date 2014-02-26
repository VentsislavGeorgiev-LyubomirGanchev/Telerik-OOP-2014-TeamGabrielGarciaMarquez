using System;
using System.Linq;
using RolePlayingGame.Core.Map;
using RolePlayingGame.Core.Human.Enemies;
using System.Collections.Generic;

namespace RolePlayingGame.Core.Human
{
    internal class Player : Human, IMovable
    {
        #region Const
        private const int DefaultHealth = 500;
        private const int DefaultMana = 5;
        private const int DefaultKnowledge = 30;
        private const int DefaultDefense = 50;
        private const int DefaultExperience = 0;
        private const int DefaultLevel = 1;
        #endregion

        #region Fields
        #endregion

        #region Constructors
        //Just base constructor
        //Should be singleton design pattern
        public Player(float x, float y)
            : base(x, y, new Entity(EntityType.Player), true)
        {
            this.Health = DefaultHealth;
            this.Mana = DefaultMana;
            this.Knowledge = DefaultKnowledge;
            this.Defense = DefaultDefense;
            this.Experience = DefaultExperience;
            this.Level = DefaultLevel;
            this.IsHeroFighting = false;
            this.IsHeroAnimating = false;
        }
        #endregion

        #region Properties

        public int Mana { get; set; }

        public int Knowledge { get; set; }

        public int Defense { get; set; }

        public int Experience { get; set; }

        public bool IsHeroFighting { get; set; }

        public bool IsHeroAnimating { get; set; }

        #endregion

        #region Methods

        public void DoMagic(Area currentArea, IList<TextPopup> popups)
        {
            if (this.Mana > 0)
            {
                Sounds.Magic();

                this.Mana--;

                this.IsHeroFighting = true;
                //_startFightTime = -1;

                popups.Clear();

                //All monsters on the screen take maximum damage
                for (int i = 0; i < Area.MapSizeX; i++)
                {
                    for (int j = 0; j < Area.MapSizeY; j++)
                    {
                        Enemy enemy = currentArea.TilesMap[i, j].Sprite as Enemy;
                        if (enemy != null)
                        {
                            //Player damage is up to twice the attack rating
                            int damage = this.Knowledge * 2;
                            int experiance = enemy.GetDamage(damage);
                            this.Experience = experiance;

                            popups.Add(new TextPopup(enemy.Location.X + 40, enemy.Location.Y + 20, damage.ToString()));
                        }
                    }
                }
            }
        }
        #endregion
    }
}