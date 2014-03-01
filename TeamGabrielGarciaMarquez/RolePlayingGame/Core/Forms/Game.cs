using RolePlayingGame.UI;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Forms
{
	internal partial class Game : Form
	{
		#region Constants

		private const string _GameTitle = "Ninja.Net";

		#endregion Constants

		#region Fields

		private readonly Stopwatch _gameTimeTracker;
		private double _gameLastTimeUpdate;
		private GDIRenderer _gameRenderer;
		private bool _hasSavedState;

		#endregion Fields

		public Game(GameState state = null)
		{
			this._gameTimeTracker = new Stopwatch();

			//Setup the form
			this.InitializeComponent();
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

			//Startup the game state
			this.GameState = state ?? new GameState();
			this._hasSavedState = state != null;
			this._gameRenderer = new GDIRenderer();
			this.Initialize();
		}

		#region Properties

		public GameState GameState { get; set; }

		#endregion Properties

		public void SaveGame()
		{
			if (this.GameState != null)
			{
				this.GameState.SaveGame();
				this.GameState.HUD.DrawMessage(this._gameRenderer, new[] { "Game saved!" });
			}
		}

		public void LoadGame()
		{
			var savedGameState = GameState.LoadGame();
			if (savedGameState != null)
			{
				this.GameState = savedGameState;
				this.GameState.HUD.DrawMessage(this._gameRenderer, new[] { "Game loaded!" });
			}
		}

		private void Initialize()
		{
			if (!this._hasSavedState)
			{
				this.GameState.Initialize();
			}

			//Initialise and start the timer
			_gameLastTimeUpdate = 0.0;
			_gameTimeTracker.Reset();
			_gameTimeTracker.Start();
		}

		private void Game_Paint(object sender, PaintEventArgs e)
		{
			//Work out how long since we were last here in seconds
			double gameTime = _gameTimeTracker.ElapsedMilliseconds / 1000.0;
			double elapsedTime = gameTime - _gameLastTimeUpdate;
			_gameLastTimeUpdate = gameTime;

			//Perform any animation and updates
			this.GameState.Update(gameTime, elapsedTime);

			//Draw everything
			this._gameRenderer.SetGraphics(e.Graphics);
			this.GameState.Draw(this._gameRenderer);

			//Force the next Paint()
			this.Invalidate();
		}

		private void Game_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					this.SaveGame();
					break;

				case Keys.F6:
					this.LoadGame();
					break;

				default:
					GameState.KeyDown(e);
					break;
			}
		}

		private void Game_Shown(object sender, EventArgs e)
		{
			if (!this._hasSavedState)
			{
				Form help = new HelpForm();
				help.Show();
				help.Focus();
			}
		}
	}
}