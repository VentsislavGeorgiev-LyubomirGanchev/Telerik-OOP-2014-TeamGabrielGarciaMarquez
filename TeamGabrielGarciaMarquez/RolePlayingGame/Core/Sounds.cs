using RolePlayingGame.Core.Map;
using System;
using System.Media;
using System.Windows.Forms;
using WMPLib;

namespace RolePlayingGame.Core
{
	/// <summary>
	/// Sounds is a static class for any other part of the program to use to play the sounds.
	/// </summary>
	internal static class Sounds
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
		private static readonly SoundPlayer _start = new SoundPlayer(@"Content\Sounds\start.wav");

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
			_start.Load();
		}

		public static void Play(string path)
		{
			var filePath = AppDirectory + path;
			if (filePath != _player.URL)
			{
				StopSound();
				_player.URL = filePath;
				_player.settings.setMode("loop", true);
				_player.controls.play();
			}
		}

		public static void PlayBackgroundSound(LevelType levelType)
		{
			switch (levelType)
			{
				case LevelType.Start:
				case LevelType.Level1:
				case LevelType.Level2:
				case LevelType.Level3:
					Play(@"\Content\Sounds\level1.mp3");
					break;

				case LevelType.Level4:
					Play(@"\Content\Sounds\boss.mp3");
					break;

				case LevelType.Level5:
				case LevelType.Level6:
				case LevelType.Level7:
					Play(@"\Content\Sounds\level2.mp3");
					break;

				case LevelType.Level8:
					Play(@"\Content\Sounds\boss.mp3");
					break;

				case LevelType.Level9:
				case LevelType.Level10:
				case LevelType.Level11:
					Play(@"\Content\Sounds\level3.mp3");
					break;

				case LevelType.Level12:
					Play(@"\Content\Sounds\boss.mp3");
					break;

				case LevelType.Level13:
				case LevelType.Level14:
					Play(@"\Content\Sounds\level4.mp3");
					break;

				case LevelType.Level15:
					Play(@"\Content\Sounds\boss.mp3");
					break;

				case LevelType.Level16:
					Play(@"\Content\Sounds\boss5.mp3");
					break;

				default:
					throw new InvalidOperationException();
			}
		}

		public static void StopSound()
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

		public static void Win()
		{
			Play(@"\Content\Sounds\winner.mp3");
		}

		public static void End()
		{
			Play(@"\Content\Sounds\end.mp3");
		}
	}
}