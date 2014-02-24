using System.Collections.Generic;

namespace RolePlayingGame.Core.Utils
{
    /// <summary>
    /// Provides a cache of entities. Will return the existing reference if it exists or load a new one.
    /// </summary>
    internal class EntitiesCache
    {
        private static readonly Dictionary<string, Entity> _Entities = new Dictionary<string, Entity>();

        public Entity this[string key]
        {
            get
            {
                //If this entity is not in the cache then load it
                if (!_Entities.ContainsKey(key))
                {
                    _Entities.Add(key, new Entity(key));
                }
                return _Entities[key];
            }
        }
    }
}