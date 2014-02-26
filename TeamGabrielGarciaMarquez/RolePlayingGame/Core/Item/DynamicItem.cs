using System;

namespace RolePlayingGame.Core.Item
{
    internal class DynamicItem : Item, ICollectable
    {
        #region Fields

        public int ItemRate { get; private set; }

        #endregion Fields

        #region Methods

        public DynamicItem(float x, float y, Entity entity, bool flip = false)
            : base(x, y, entity, flip)
        {
            if (this.Entity.Special != string.Empty)
            {
                this.ItemRate = Convert.ToInt32(this.Entity.Special);
            }
        }

        #endregion Methods
    }
}