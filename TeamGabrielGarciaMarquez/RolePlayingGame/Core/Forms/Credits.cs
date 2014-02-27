using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Forms
{
	public partial class Credits : Form
	{
		public Credits()
		{
			InitializeComponent();

			LinkLabel.Link link = new LinkLabel.Link();
			link.LinkData = "http://telerikacademy.com/";
			this.linkLabel1.Links.Add(link);

			link = new LinkLabel.Link();
			link.LinkData = "https://github.com/ventsislav-georgiev";
			this.linkLabel2.Links.Add(link);

			link = new LinkLabel.Link();
			link.LinkData = "https://github.com/luboganchev";
			this.linkLabel3.Links.Add(link);

			link = new LinkLabel.Link();
			link.LinkData = "https://github.com/mivak";
			this.linkLabel4.Links.Add(link);

			link = new LinkLabel.Link();
			link.LinkData = "https://github.com/HristoBuyukliev";
			this.linkLabel5.Links.Add(link);
		}

		private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(e.Link.LinkData as string);
		}
	}
}
