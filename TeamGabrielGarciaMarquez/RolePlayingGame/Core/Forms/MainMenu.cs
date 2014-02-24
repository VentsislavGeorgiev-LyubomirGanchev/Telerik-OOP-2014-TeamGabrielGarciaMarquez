using System;
using System.Windows.Forms;
using System.IO;

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
