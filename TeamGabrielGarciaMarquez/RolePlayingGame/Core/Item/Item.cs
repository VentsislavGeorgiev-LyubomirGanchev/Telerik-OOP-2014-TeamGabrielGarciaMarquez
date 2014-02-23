namespace RolePlayingGame.Core.Item
{
    using System;
    using System.Collections.Generic;
    using UI = RolePlayingGame.UI;

    //TODO fix when remove the old class Tile
    public abstract class Item : UI.Tile
    {
        #region Constants
        #endregion 

        #region Fields
        #endregion

        #region Constructors
        public Item(string name, ItemCategoryEnum category, ItemTypeEnum type) 
        {
            this.Name = name;
            this.Category = category;
            this.Type = type;
        }
        #endregion

        #region Properties
        public string Name { get; set; }

        //public int Position { get; set; }
        public ItemCategoryEnum Category { get; set; }

        public ItemTypeEnum Type { get; set; }
        #endregion

        #region Methods
        #endregion
    }
}
