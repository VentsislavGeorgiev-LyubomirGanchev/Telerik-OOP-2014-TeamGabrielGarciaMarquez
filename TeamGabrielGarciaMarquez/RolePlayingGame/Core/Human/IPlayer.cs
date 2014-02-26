namespace RolePlayingGame.Core.Human
{
    interface IPlayer
    {
        int Health { get; set; }

        int Mana { get; set; }

        int Knowledge { get; set; }

        int Defense { get; set; }

        int Experience { get; set; }

        bool HasKey { get; set; }
    }
}
