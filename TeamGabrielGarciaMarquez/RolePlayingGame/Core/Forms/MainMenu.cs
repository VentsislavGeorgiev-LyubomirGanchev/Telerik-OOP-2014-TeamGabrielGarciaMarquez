using System;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Forms
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.InitializeComponent();
            //Game game = new Game();
            //game.Hide();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
        }

        private void Button1Click(object sender, EventArgs e)
        {
            this.Hide();
            Game game = new Game();
            game.ShowDialog();
            this.Show();
        }

        private void BtnSettingsClick(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void Label1Click(object sender, EventArgs e)
        {
        }

        private void BtnSaveGameClick(object sender, EventArgs e)
        {
            //game.SaveGame(
        }
    }
}