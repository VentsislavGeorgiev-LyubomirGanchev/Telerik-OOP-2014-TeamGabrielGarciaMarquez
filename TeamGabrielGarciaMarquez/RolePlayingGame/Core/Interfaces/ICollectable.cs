namespace RolePlayingGame.Core.Interfaces
{
    interface ICollectable
    {
        EntityCategoryType Category { get;}

        int ItemRate { get; }
    }
}