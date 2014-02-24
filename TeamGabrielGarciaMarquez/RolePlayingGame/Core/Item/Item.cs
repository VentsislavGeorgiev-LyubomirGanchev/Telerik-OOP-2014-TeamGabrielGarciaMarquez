namespace RolePlayingGame.Core.Item
{
    internal abstract class Item : Sprite, IItem
    {
        #region Methods

        public Item(float x, float y, Entity entity, bool flip = false)
            : base(x, y, entity, flip)
        {
        }

        #endregion Methods
    }
}