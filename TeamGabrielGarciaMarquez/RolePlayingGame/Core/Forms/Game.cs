using RolePlayingGame.UI;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Forms
{
    internal partial class Game : Form
    {
        private readonly Stopwatch _gameTimeTracker = new Stopwatch();
        private double _gameLastTimeUpdate;
        private GameEngine _gameEngine;
        private GDIRenderer _gameRenderer;

        public MainMenu MMenu { get; set; }

        public Game()
        {
            //Setup the form
            this.InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            //Startup the game state
            this._gameEngine = new GameEngine();
            this._gameRenderer = new GDIRenderer();
            this.Initialize();
        }

        private void Initialize()
        {
            this._gameEngine.Initialize();

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
            this._gameEngine.Update(gameTime, elapsedTime);

            //Draw everything
            this._gameRenderer.SetGraphics(e.Graphics);
            this._gameEngine.Draw(this._gameRenderer);

            //Force the next Paint()
            this.Invalidate();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            _gameEngine.KeyDown(e.KeyCode);
        }

        private void Game_Shown(object sender, EventArgs e)
        {
            //Form help = new HelpForm();
            //help.Show();
            //help.Focus();
        }
    }
}