using System;
using System.Windows.Forms;
using System.IO;

namespace RolePlayingGame.Core.Forms
{
    public partial class MainMenu : Form
    {
        private readonly Game _game;
        public MainMenu()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.InitializeComponent();
            _game = new Game();
            _game.Hide();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
        }

        private void Button1Click(object sender, EventArgs e)
        {
            this.Hide();
            _game.ShowDialog();
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

                //bformatter.Serialize(stream, _game.GameState.World.map);
            }
        }

        private void BtnLoadGameClick(object sender, EventArgs e)
        {
            string dir = @"c:\temp";
            string serializationFile = Path.Combine(dir, "salesmen.bin");
            using (Stream stream = File.Open(serializationFile, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                //_game.GameState.World.map = (string)bformatter.Deserialize(stream);
            }

        }
    }
}
