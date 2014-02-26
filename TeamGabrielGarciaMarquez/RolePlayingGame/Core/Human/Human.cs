using RolePlayingGame.Core.Map;
using RolePlayingGame.Core.Map.Tiles;
using System;
using System.Collections.Generic;

namespace RolePlayingGame.Core.Human
{
    //TODO fix namespace
    internal abstract class Human : Sprite
    {
        #region Const

        private const int LuckyScope = 10;
        private const int LuckyNumber = 3;
        private const string MissMessage = "miss";

        #endregion Const

        #region Fields
        #endregion Fields

        #region Constructors

        public Human(float x, float y, Entity entity, bool flip)
            : base(x, y, entity, flip)
        {
            this.Position = new Point((int)x, (int)y);
            if (this.Entity.Special != string.Empty)
            {
                this.Level = Convert.ToInt32(this.Entity.Special);
            }
        }

        #endregion Constructors

        #region Properties
        public int Health { get; set; }
        public int Level { get; protected set; }
        public Point Position { get; private set; }
        #endregion
        
        #region Methods
        public static void Fight(Random random, IPlayer firstFighter, IEnemy secondFighter, IList<TextPopup> popups)
        {
            //TODO Refactore this method to improve abstraction
            var player = firstFighter as Player;
            var enemy = secondFighter as Enemies.Enemy;

            Sounds.Fight();
            player.IsHeroFighting = true;
            popups.Clear();

            //An enemy strength ability is 1/2 for boss and 1/3 for student of their max health. Compare that to your defense
            //If you outclass them then there is still a chance of a lucky hit
            int playerDamage = 0;

            if (random.Next(enemy.Strength + 1) >= player.Defense
                || (enemy.Strength < player.Defense && random.Next(LuckyScope) == LuckyNumber))
            {
                //Enemies do damage up to their max health - if they hit you.
                playerDamage = random.Next(enemy.Health) + 1;
                player.Health -= playerDamage;

                if (player.Health <= 0)
                {
                    player.Health = 0;
                    //TODO EVENT for dead
                    player.Entity.Tile = new Tile(Entity.TileDescriptions[EntityType.Bones.ToString()]);
                }

            }

            string playerDamageMessage = playerDamage != 0 ? playerDamage.ToString() : MissMessage;
            popups.Add(new TextPopup(player.Location.X + 40, player.Location.Y + 20, playerDamageMessage));

            //A enemy armour is 1/5 of their max health
            if (random.Next(player.Knowledge + 1) >= (enemy.Health / 5))
            {
                //Player damage is up to twice the attack rating
                int enemyDamage = random.Next(player.Knowledge * 2) + 1;
                if (enemyDamage > 0)
                {
                    int experiance = enemy.GetDamage(enemyDamage);
                    player.Experience += experiance;
                }
                string message = enemyDamage != 0 ? enemyDamage.ToString() : MissMessage;
                popups.Add(new TextPopup(enemy.Location.X + 40, enemy.Location.Y + 20, message));
            }
            else
            {
                popups.Add(new TextPopup(enemy.Location.X + 40, enemy.Location.Y + 20, MissMessage));
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