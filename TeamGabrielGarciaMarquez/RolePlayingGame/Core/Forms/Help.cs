using System;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Forms
{
	public partial class HelpForm : Form
	{
		public HelpForm()
		{
			InitializeComponent();
		}

		private void close_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}