namespace RolePlayingGame.Core.Interfaces
{
    using RolePlayingGame.Core.Item;
    interface ICollectable
    {
        ItemCategoryType Category { get;}

        int ItemRate { get; }
    }
}