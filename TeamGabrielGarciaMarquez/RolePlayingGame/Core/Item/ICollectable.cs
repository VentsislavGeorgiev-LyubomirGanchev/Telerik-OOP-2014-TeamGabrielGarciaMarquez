namespace RolePlayingGame.Core.Item
{
    interface ICollectable
    {
        EntityCategoryType Category { get;}

        int ItemRate { get; }
    }
}