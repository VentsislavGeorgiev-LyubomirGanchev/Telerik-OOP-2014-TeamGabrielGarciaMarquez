namespace RolePlayingGame.Core.Item
{
    using RolePlayingGame.Core.Interfaces;
    using UI = RolePlayingGame.UI;

    //TODO fix when remove the old class Tile
    public abstract class Item : UI.Tile, IItem
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Constructors
        public Item(string name, ItemCategoryType category, ObjectType type, bool isPassable)
        {
            this.Name = name;
            this.Category = category;
            this.Type = type;
            this.IsPassable = isPassable;
        }
        #endregion

        #region Properties
        public string Name { get; set; }

        public ItemCategoryType Category { get; set; }

        public ObjectType Type { get; set; }

        public bool IsPassable { get; private set; }
        #endregion

        #region Methods
        #endregion
    }
}