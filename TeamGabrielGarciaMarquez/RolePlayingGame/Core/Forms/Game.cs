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
		public GameState _gameState;
        public MainMenu menu;
		public Game()
		{
			//Setup the form
			InitializeComponent();
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

			//Startup the game state
			_gameState = new GameState(ClientSize);

			initialize();
		}
        public MainMenu M_Menu { get; set; }
		private void initialize()
		{
			_gameState.Initialize();

			//Initialise and start the timer
			_lastTime = 0.0;
			_timer.Reset();
			_timer.Start();
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