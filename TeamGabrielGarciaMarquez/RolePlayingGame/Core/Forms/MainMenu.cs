using System;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Forms
{
	public partial class MainMenu : Form
	{
		#region Fields

		private GameState _gameState;

		#endregion Fields

		public MainMenu()
		{
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.InitializeComponent();

			this._gameState = null;
			MessageForm = new Form() { WindowState = FormWindowState.Maximized, TopMost = true };
		}

		public static Form MessageForm { get; private set; }

		private void NewGame(object sender, EventArgs e)
		{
			var game = new Game(this._gameState);
			game.FormClosed += game_FormClosed;
			this._gameState = game.GameState;

			btn_NewGame.Text = "Continue";
			btn_Restart.Show();
			this.Hide();
			game.Show();
		}

		private void BtnSettingsClick(object sender, EventArgs e)
		{
			Settings settings = new Settings();
			settings.Show();
		}

		private void BtnSaveGameClick(object sender, EventArgs e)
		{
			if (this._gameState != null)
			{
				this._gameState.SaveGame();
			}
		}

		private void BtnLoadGameClick(object sender, EventArgs e)
		{
			var savedGameState = GameState.LoadGame();
			if (savedGameState != null)
			{
				this._gameState = savedGameState;
				this.NewGame(sender, e);
			}
		}

		private void game_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Show();
		}

		private void btn_Restart_Click(object sender, EventArgs e)
		{
			this._gameState = null;
			Sounds.StopSound();
			btn_NewGame.Text = "New Game";
			btn_Restart.Hide();
		}

		private void btn_Credits_Click(object sender, EventArgs e)
		{
			var credits = new Credits();
			credits.ShowDialog();
		}
	}
}