using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Forms
{
	public partial class MainMenu : Form
	{
		#region Constants

		private const string _SaveGameFileName = "save.bin";

		#endregion Constants

		#region Fields

		private GameState _gameState;
		private bool _loadedSaveGame;

		#endregion Fields

		public MainMenu()
		{
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.InitializeComponent();

			this._loadedSaveGame = false;
			this._gameState = null;
		}

		private void MainMenu_Load(object sender, EventArgs e)
		{
		}

		private void NewGame(object sender, EventArgs e)
		{
			try
			{
				var game = new Game(this._gameState);
				game.FormClosed += game_FormClosed;
				this._gameState = game.GameState;

				btn_NewGame.Text = "Continue";
				btn_Restart.Show();
				this.Hide();
				game.Show();
				this._loadedSaveGame = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), ex.Message);
				this.Close();
			}
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
				using (var stream = File.OpenWrite(_SaveGameFileName))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(stream, this._gameState.SaveGame());
				}
			}
		}

		private void BtnLoadGameClick(object sender, EventArgs e)
		{
			if (!File.Exists(_SaveGameFileName))
			{
				return;
			}

			using (var stream = File.OpenRead(_SaveGameFileName))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				this._gameState = new GameState((SaveGameData)binaryFormatter.Deserialize(stream));
				this._loadedSaveGame = true;
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
			btn_NewGame.Text = "New Game";
			btn_Restart.Hide();
		}
	}
}