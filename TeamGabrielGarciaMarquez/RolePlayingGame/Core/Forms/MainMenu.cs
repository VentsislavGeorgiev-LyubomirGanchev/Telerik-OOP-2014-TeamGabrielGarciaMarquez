using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Forms
{
    public partial class MainMenu : Form
    {

        public MainMenu()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            //Game game = new Game();
            //game.Hide();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game game = new Game();
            game.Show();
            this.Hide();
        }

        private void Txt_Title_TextChanged(object sender, EventArgs e)
        {

        }

        private void BTN_Settings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BTN_SaveGame_Click(object sender, EventArgs e)
        {
            //game.SaveGame(
        }
    }
}
