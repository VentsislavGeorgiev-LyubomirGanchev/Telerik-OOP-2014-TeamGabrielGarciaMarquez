using System.Media;

namespace RolePlayingGame.Core
{
	/// <summary>
	/// Sounds is a static class for any other part of the program to use to play the sounds.
	/// </summary>
	public static class Sounds
	{
        private static readonly SoundPlayer _Eat = new SoundPlayer(@"Content\Sounds\eat.wav");
        private static readonly SoundPlayer _Pickup = new SoundPlayer(@"Content\Sounds\pickup.wav");
        private static readonly SoundPlayer _Fight = new SoundPlayer(@"Content\Sounds\fight.wav");
        private static readonly SoundPlayer _Kiss = new SoundPlayer(@"Content\Sounds\kiss.wav");
        private static readonly SoundPlayer _Magic = new SoundPlayer(@"Content\Sounds\magic.wav");
        private static readonly SoundPlayer _Start = new SoundPlayer(@"Content\Sounds\start.wav");

		static Sounds()
		{
			//preload the sounds on construction.
			_Eat.Load();
			_Pickup.Load();
			_Fight.Load();
			_Kiss.Load();
			_Magic.Load();
			_Start.Load();
		}

		public static void Eat()
		{
			_Eat.Play();
		}

		public static void Pickup()
		{
			_Pickup.Play();
		}

		public static void Fight()
		{
			_Fight.Play();
		}

		public static void Kiss()
		{
			_Kiss.Play();
		}

		public static void Magic()
		{
			_Magic.Play();
		}

		public static void Start()
		{
			//_start.Play();
		}
	}
}