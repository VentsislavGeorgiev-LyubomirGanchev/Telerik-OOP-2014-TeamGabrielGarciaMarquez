using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Forms
{
    internal partial class Game : Form
    {
        private readonly Stopwatch _gameTimeTracker = new Stopwatch();
        private double _gameLastTimeUpdate;
        private GameState _gameState;

        public Game()
        {
            //Setup the form
            this.InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            //Startup the game state
            this._gameState = new GameState(this.ClientSize);

            this.Initialize();
        }

        private void Initialize()
        {
            _gameState.Initialize();

            //Initialise and start the timer
            _gameLastTimeUpdate = 0.0;
            _gameTimeTracker.Reset();
            _gameTimeTracker.Start();
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
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(GameState));

            System.IO.StreamReader file = new System.IO.StreamReader(
                @"C:\Users\hristo\Documents\GitHub\Telerik\TeamGabrielGarciaMarquez\RolePlayingGame\Content\Saved Games\save1.xml");
            GameState loadedGame = (GameState)reader.Deserialize(file);
            file.Close();
            return loadedGame;
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            //Work out how long since we were last here in seconds
            double gameTime = _gameTimeTracker.ElapsedMilliseconds / 1000.0;
            double elapsedTime = gameTime - _gameLastTimeUpdate;
            _gameLastTimeUpdate = gameTime;

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
            //Form help = new HelpForm();
            //help.Show();
            //help.Focus();
        }
    }
}