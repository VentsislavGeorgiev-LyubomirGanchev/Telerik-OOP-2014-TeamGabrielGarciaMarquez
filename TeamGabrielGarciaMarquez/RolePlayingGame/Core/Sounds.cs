using System.Media;
using System.Windows.Forms;
using WMPLib;

namespace RolePlayingGame.Core
{
	/// <summary>
	/// Sounds is a static class for any other part of the program to use to play the sounds.
	/// </summary>
	public static class Sounds
	{
        private static readonly string AppDirectory = Application.StartupPath;
		private static readonly WindowsMediaPlayer _player = new WindowsMediaPlayer();
		private static readonly SoundPlayer _healthUp = new SoundPlayer(@"Content\Sounds\eatHealth.wav");
		private static readonly SoundPlayer _manaUp = new SoundPlayer(@"Content\Sounds\manaDrink.wav");
		private static readonly SoundPlayer _defenseUp = new SoundPlayer(@"Content\Sounds\DefenseUp.wav");
		private static readonly SoundPlayer _knowledgeUp = new SoundPlayer(@"Content\Sounds\powerUp.wav");
		private static readonly SoundPlayer _levelUp = new SoundPlayer(@"Content\Sounds\levelUp.wav");
		private static readonly SoundPlayer _bossFight = new SoundPlayer(@"Content\Sounds\bossFight.wav");
		private static readonly SoundPlayer _studentFight = new SoundPlayer(@"Content\Sounds\studentFight.wav");
		private static readonly SoundPlayer _doorOpen = new SoundPlayer(@"Content\Sounds\doorOpen.wav");
		private static readonly SoundPlayer _magic = new SoundPlayer(@"Content\Sounds\magicSound.wav");
		private static readonly SoundPlayer _pickUp = new SoundPlayer(@"Content\Sounds\pickup.wav");
		private static readonly SoundPlayer _Start = new SoundPlayer(@"Content\Sounds\start.wav");

		static Sounds()
		{
			//preload the sounds on construction.
			_healthUp.Load();
			_manaUp.Load();
			_defenseUp.Load();
			_knowledgeUp.Load();
			_levelUp.Load();
			_bossFight.Load();
			_studentFight.Load();
			_doorOpen.Load();
			_magic.Load();
			_pickUp.Load();
			_Start.Load();
		}

		public static void PlayBackgroundSound()
		{
			var path = AppDirectory + @"\Content\Sounds\MeadowOfThePast.mp3";
			_player.URL = path;
			_player.settings.setMode("loop", true);
			_player.controls.play();
		}

		public static void StopBackgroundSound()
		{
			_player.controls.stop();
		}

		public static void SetBackgroundSoundVolume(int volume)
		{
			_player.settings.volume = volume;
		}

		public static void HealthUp()
		{
			_healthUp.Play();
		}

		public static void ManaUp()
		{
			_manaUp.Play();
		}

		public static void DefenseUp()
		{
			_defenseUp.Play();
		}

		public static void KnowledgeUp()
		{
			_knowledgeUp.Play();
		}

		public static void LevelUp()
		{
			_levelUp.Play();
		}

		public static void BossFight()
		{
			_bossFight.Play();
		}

		public static void StudentFight()
		{
			_studentFight.Play();
		}

		public static void DoorOpen()
		{
			_doorOpen.Play();
		}

		public static void Magic()
		{
			_magic.Play();
		}

		public static void Pickup()
		{
			_pickUp.Play();
		}

		public static void Start()
		{
			//_start.Play();
		}
	}
}