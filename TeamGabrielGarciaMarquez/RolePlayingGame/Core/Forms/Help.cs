using System;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Forms
{
	public partial class HelpForm : Form
	{
        public HelpForm()
        {
            this.InitializeComponent();
        }

		private void CloseClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}