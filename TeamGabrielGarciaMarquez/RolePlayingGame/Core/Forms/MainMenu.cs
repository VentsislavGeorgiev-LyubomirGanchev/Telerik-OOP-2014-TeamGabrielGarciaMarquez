using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RolePlayingGame.Core.Forms
{
    public partial class MainMenu : Form
    {
        Game game;
        public MainMenu()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            game = new Game();
            game.Hide();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            game.Close();
            game = new Game();
            game.Show();
            game.M_Menu = this;
            //this.Hide();
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
            string dir = @"c:\temp";
            string serializationFile = Path.Combine(dir, "salesmen.bin");
            //serialize
            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, game._gameState.World.map );
            }
        }

        private void BTN_LoadGame_Click(object sender, EventArgs e)
        {
            string dir = @"c:\temp";
            string serializationFile = Path.Combine(dir, "salesmen.bin");
            using (Stream stream = File.Open(serializationFile, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                game._gameState.World.map = (string)bformatter.Deserialize(stream);
            }

        }
    }
}
