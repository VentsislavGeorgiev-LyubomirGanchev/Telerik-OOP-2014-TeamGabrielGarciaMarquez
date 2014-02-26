using System.Media;

namespace RolePlayingGame.Core
{
	/// <summary>
	/// Sounds is a static class for any other part of the program to use to play the sounds.
	/// </summary>
	public static class Sounds
	{
        private static readonly SoundPlayer _healthUp = new SoundPlayer(@"Content\Sounds\eatHealth.wav");
        private static readonly SoundPlayer _manaUp = new SoundPlayer(@"Content\Sounds\manaDrink.wav");
        private static readonly SoundPlayer _defenseUp = new SoundPlayer(@"Content\Sounds\DefenseUp.wav");
        private static readonly SoundPlayer _knowledgeUp = new SoundPlayer(@"Content\Sounds\powerUp.wav");
        private static readonly SoundPlayer _levelUp = new SoundPlayer(@"Content\Sounds\levelUp.wav");
        private static readonly SoundPlayer _bossFight = new SoundPlayer(@"Content\Sounds\bossFight.wav");
        private static readonly SoundPlayer _studentFight = new SoundPlayer(@"Content\Sounds\studentFight.wav");
        private static readonly SoundPlayer _doorOpen = new SoundPlayer(@"Content\Sounds\doorOpen.wav");
        private static readonly SoundPlayer _magic = new SoundPlayer(@"Content\Sounds\magicSound.wav");
        private static readonly SoundPlayer _Start = new SoundPlayer(@"Content\Sounds\start.wav");

		static Sounds()
		{
			//preload the sounds on construction.
            _healthUp.Load();
            _manaUp.Load();
            _defenseUp.Load();
            _knowledgeUp.Load();
            _levelUp.Load();
			_Start.Load();
		}

		public static void Eat()
		{
            _healthUp.Play();
		}

		public static void Pickup()
		{
            _manaUp.Play();
		}

		public static void Fight()
		{
            _defenseUp.Play();
		}

		public static void Kiss()
		{
            _knowledgeUp.Play();
		}

		public static void Magic()
		{
            _levelUp.Play();
		}

		public static void Start()
		{
			//_start.Play();
		}
	}
}