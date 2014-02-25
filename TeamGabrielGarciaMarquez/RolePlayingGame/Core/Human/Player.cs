using System;
using System.Linq;
using RolePlayingGame.Core.Map;
using RolePlayingGame.Core.Human.Enemies;
using System.Collections.Generic;

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

        #endregion

        #region Methods

        public void DoMagic(Area currentArea)
        {
            if (this.Mana > 0)
            {
                Sounds.Magic();

                this.Mana--;

                //_heroSpriteFighting = true;
                //_startFightTime = -1;

                var popups = new List<TextPopup>();

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

                            popups.Add(new TextPopup(enemy.Position.X + 40, enemy.Position.Y + 20, damage.ToString()));
                        }
                    }
                }
            }
        }
        #endregion
    }
}