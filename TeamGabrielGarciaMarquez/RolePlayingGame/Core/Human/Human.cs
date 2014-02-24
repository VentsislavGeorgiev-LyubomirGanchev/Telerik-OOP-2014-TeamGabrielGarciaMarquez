using RolePlayingGame.Core.Map.Tiles;
using System;
using System.Collections.Generic;

namespace RolePlayingGame.Core.Human
{
    //TODO fix namespace
    internal abstract class Human : Sprite, IRenderable
    {
        #region Const

        private const int LuckyScope = 10;
        private const int LuckyNumber = 3;
        private const string MissMessage = "miss";

        #endregion Const

        #region Fields

        public Point Position { get; private set; }

        #endregion Fields

        #region Constructors

        public Human(int x, int y, Entity entity, bool flip)
            : base(x, y, entity, flip)
        {
            this.Position = new Point(x, y);
        }

        #endregion Constructors

        public int Health { get; set; }

        #region Methods

        //TODO fix parameters with interfaces
        public static void Fight(Random random, Human firstFighter, Human secondFighter)
        {
            var player = firstFighter as Player;
            var enemy = secondFighter as Enemies.Enemies;

            var popups = new List<TextPopup>();

            //A monsters attack ability is 1/2 their max health. Compare that to your armour
            //If you outclass them then there is still a chance of a lucky hit
            if (random.Next((enemy.Health / 2) + 1) >= player.Defense
                || (enemy.Health / 2 < player.Defense && random.Next(LuckyScope) == LuckyNumber))
            {
                //Monsters do damage up to their max health - if they hit you.
                int playerDamage = random.Next(enemy.Health) + 1;
                player.Health -= playerDamage;

                if (player.Health <= 0)
                {
                    player.Health = 0;
                    player.Entity.Tile = new Tile(Entity.TileDescriptions[EntityType.Bones.ToString()]);
                }
                string message = playerDamage != 0 ? playerDamage.ToString() : MissMessage;
                popups.Add(new TextPopup(player.Position.X + 40, player.Position.Y + 20, message));
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
                    //Remove enemy
                    enemy.Entity.Tile = new Tile(Entity.TileDescriptions[EntityType.Bones.ToString()]);
                    return;
                }
                string message = enemyDamage != 0 ? enemyDamage.ToString() : MissMessage;
                popups.Add(new TextPopup(enemy.Position.X + 40, enemy.Position.Y + 20, message));
            }
            else
            {
                popups.Add(new TextPopup(enemy.Position.X + 40, enemy.Position.Y + 20, MissMessage));
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