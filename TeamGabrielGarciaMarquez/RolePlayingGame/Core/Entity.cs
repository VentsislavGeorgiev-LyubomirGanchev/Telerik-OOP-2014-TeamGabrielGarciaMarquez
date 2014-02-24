using RolePlayingGame.Core.Map.Tiles;
using System;
using System.Collections.Generic;
using System.IO;

namespace RolePlayingGame.Core
{
    internal class Entity
    {
        #region Static

        private static Dictionary<string, EntityRawData> _TileDescriptions;

        private static Dictionary<string, EntityRawData> TileDescriptions
        {
            get
            {
                if (Entity._TileDescriptions == null)
                {
                    Entity._TileDescriptions = new Dictionary<string, EntityRawData>();
                    using (StreamReader stream = new StreamReader(@"Content\Entities.csv"))
                    {
                        string line;
                        while ((line = stream.ReadLine()) != null)
                        {
                            string[] elements = line.Split(',');
                            var entityRawData = new EntityRawData(elements);
                            Entity._TileDescriptions.Add(entityRawData.Key, entityRawData);
                        }
                    }
                }
                return Entity._TileDescriptions;
            }
        }

        #endregion Static

        #region Fields

        public Tile Tile { get; set; }

        public string Name { get; set; }

        public EntityCategoryType Category { get; set; }

        public EntityType Type { get; set; }

        public string Key { get; set; }

        public bool IsPassable { get; private set; }

        public bool IsTransparent { get; private set; }

        public string Special { get; private set; }

        public EntityRawData Raw { get; private set; }

        #endregion Fields

        public Entity(EntityType type)
            : this(type.ToString())
        {
        }

        public Entity(string key)
        {
            var entityRawData = Entity.TileDescriptions[key];

            this.Name = entityRawData.Name;
            this.Key = entityRawData.Key;
            this.Category = (EntityCategoryType)Enum.Parse(typeof(EntityCategoryType), entityRawData.Category);
            this.Type = (EntityType)Enum.Parse(typeof(EntityType), entityRawData.Type);
            this.IsPassable = bool.Parse(entityRawData.IsPassable);
            this.IsTransparent = bool.Parse(entityRawData.IsTransparent);
            this.Tile = new Tile(entityRawData);
            this.Special = entityRawData.Special;
            this.Raw = entityRawData;
        }
    }
}