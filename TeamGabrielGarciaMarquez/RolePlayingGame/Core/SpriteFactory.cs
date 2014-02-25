using RolePlayingGame.Core.Human;
using RolePlayingGame.Core.Human.Enemies;
using RolePlayingGame.Core.Item;
using System;

namespace RolePlayingGame.Core
{
    internal class SpriteFactory
    {
        public static Sprite Create(int x, int y, Entity entity)
        {
            return Create((float)x, (float)y, entity);
        }

        public static Sprite Create(float x, float y, Entity entity)
        {
            Sprite sprite = null;
            switch (entity.Category)
            {
                case EntityCategoryType.Human:
                    switch (entity.Type)
                    {
                        case EntityType.Player:
                            sprite = new Player(x, y);
                            break;

                        case EntityType.Boss1:
                        case EntityType.Boss2:
                        case EntityType.Boss3:
                        case EntityType.Boss4:
                        case EntityType.Boss5:
                            sprite = new Boss(x, y, entity.Type);
                            break;

                        case EntityType.Student1:
                        case EntityType.Student2:
                        case EntityType.Student3:
                        case EntityType.Student4:
                            sprite = new Student(x, y, entity.Type);
                            break;

                        default:
                            throw new InvalidOperationException();
                    }
                    break;

                case EntityCategoryType.Health:
                case EntityCategoryType.Mana:
                case EntityCategoryType.Knowledge:
                case EntityCategoryType.Defense:
                    sprite = new DynamicItem(x, y, entity);
                    break;

                case EntityCategoryType.Key:
                case EntityCategoryType.Door:
                case EntityCategoryType.WorldItems:
                    sprite = new StaticItem(x, y, entity);
                    break;

                default:
                    throw new InvalidOperationException();
            }
            return sprite;
        }
    }
}