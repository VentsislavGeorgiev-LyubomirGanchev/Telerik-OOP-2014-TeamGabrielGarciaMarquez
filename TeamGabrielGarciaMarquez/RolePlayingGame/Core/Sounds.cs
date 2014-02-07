using System.Media;

namespace RolePlayingGame.Core
{
	/// <summary>
	/// Sounds is a static class for any other part of the program to use to play the sounds.
	/// </summary>
	public static class Sounds
	{
		private static SoundPlayer _eat = new SoundPlayer(@"Content\Sounds\eat.wav");
		private static SoundPlayer _pickup = new SoundPlayer(@"Content\Sounds\pickup.wav");
		private static SoundPlayer _fight = new SoundPlayer(@"Content\Sounds\fight.wav");
		private static SoundPlayer _kiss = new SoundPlayer(@"Content\Sounds\kiss.wav");
		private static SoundPlayer _magic = new SoundPlayer(@"Content\Sounds\magic.wav");
		private static SoundPlayer _start = new SoundPlayer(@"Content\Sounds\start.wav");

		static Sounds()
		{
			//preload the sounds on construction.
			_eat.Load();
			_pickup.Load();
			_fight.Load();
			_kiss.Load();
			_magic.Load();
			_start.Load();
		}

		public static void Eat()
		{
			_eat.Play();
		}

		public static void Pickup()
		{
			_pickup.Play();
		}

		public static void Fight()
		{
			_fight.Play();
		}

		public static void Kiss()
		{
			_kiss.Play();
		}

		public static void Magic()
		{
			_magic.Play();
		}

		public static void Start()
		{
			//_start.Play();
		}
	}
}