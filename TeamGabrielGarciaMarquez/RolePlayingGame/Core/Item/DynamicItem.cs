﻿using System;

namespace RolePlayingGame.Core.Item
{
    internal class DynamicItem : Item, ICollectable
    {
        #region Fields

        public int ItemRate { get; private set; }

        #endregion Fields

        #region Methods

        public DynamicItem(int x, int y, Entity entity, bool flip = false)
            : base(x, y, entity, flip)
        {
            //TODO Add item rates
            if (this.Entity.Special != string.Empty)
            {
                this.ItemRate = Convert.ToInt32(this.Entity.Special);
            }
        }

        #endregion Methods
    }
}