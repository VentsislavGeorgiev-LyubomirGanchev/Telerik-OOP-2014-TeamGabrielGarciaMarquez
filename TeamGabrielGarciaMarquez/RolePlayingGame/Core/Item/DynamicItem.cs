namespace RolePlayingGame.Core.Item
{
    using RolePlayingGame.Core.Interfaces;
    public class DynamicItem : Item, ICollectable
    {
        public DynamicItem(string name, ItemCategoryType category, ObjectType type, int itemRate, bool isPassable)
            : base(name, category, type, isPassable)
        {
            this.ItemRate = itemRate;
        }

        public int ItemRate { get; private set; }
    }
}