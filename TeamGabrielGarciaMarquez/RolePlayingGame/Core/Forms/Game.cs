using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Forms
{
	public partial class Game : Form
	{
		private Stopwatch _timer = new Stopwatch();
		private double _lastTime;
		private long _frameCounter;
		private GameState _gameState;

		public Game()
		{
			//Setup the form
			InitializeComponent();
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

			//Startup the game state
			_gameState = new GameState(ClientSize);

			initialize();
		}

		private void initialize()
		{
			_gameState.Initialize();

			//Initialise and start the timer
			_lastTime = 0.0;
			_timer.Reset();
			_timer.Start();
		}

        public static void SaveGame(GameState gameToSave)
        {
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(GameState));

            System.IO.StreamWriter file = new System.IO.StreamWriter(
                @"C:\Users\hristo\Documents\GitHub\Telerik\TeamGabrielGarciaMarquez\RolePlayingGame\Content\Saved Games\save1.xml");
            writer.Serialize(file, gameToSave);
            file.Close();
        }

        public static GameState LoadGame()
        {
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(GameState));

            System.IO.StreamReader file = new System.IO.StreamReader(
                @"C:\Users\hristo\Documents\GitHub\Telerik\TeamGabrielGarciaMarquez\RolePlayingGame\Content\Saved Games\save1.xml");
            GameState loadedGame = (GameState)reader.Deserialize(file);
            file.Close();
            return loadedGame;
        }

		private void Game_Paint(object sender, PaintEventArgs e)
		{
			//Work out how long since we were last here in seconds
			double gameTime = _timer.ElapsedMilliseconds / 1000.0;
			double elapsedTime = gameTime - _lastTime;
			_lastTime = gameTime;
			_frameCounter++;

			//Perform any animation and updates
			_gameState.Update(gameTime, elapsedTime);

			//Draw everything
			_gameState.Draw(e.Graphics);

			//Force the next Paint()
			this.Invalidate();
		}

		private void Game_KeyDown(object sender, KeyEventArgs e)
		{
			_gameState.KeyDown(e.KeyCode);
		}

		private void Game_Shown(object sender, EventArgs e)
		{
			Form help = new HelpForm();
			help.Show();
			help.Focus();
		}
	}
}