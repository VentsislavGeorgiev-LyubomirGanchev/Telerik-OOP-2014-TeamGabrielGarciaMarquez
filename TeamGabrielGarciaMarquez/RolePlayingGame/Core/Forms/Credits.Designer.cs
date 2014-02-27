using System.Windows.Forms;
namespace RolePlayingGame.Core.Forms
{
	partial class Credits
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Credits));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			this.linkLabel5 = new System.Windows.Forms.LinkLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(3, 112);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(275, 22);
			this.label1.TabIndex = 0;
			this.label1.Text = "This game was brought to you by";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(47, 219);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(31, 22);
			this.label2.TabIndex = 1;
			this.label2.Text = "for";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.linkLabel1.LinkColor = System.Drawing.SystemColors.Highlight;
			this.linkLabel1.Location = new System.Drawing.Point(94, 219);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(144, 22);
			this.linkLabel1.TabIndex = 2;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Telerik Academy";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(446, 100);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(133, 22);
			this.label3.TabIndex = 3;
			this.label3.Text = "Game creators:";
			// 
			// linkLabel2
			// 
			this.linkLabel2.ActiveLinkColor = System.Drawing.Color.Black;
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.linkLabel2.LinkColor = System.Drawing.SystemColors.Highlight;
			this.linkLabel2.Location = new System.Drawing.Point(524, 144);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(170, 22);
			this.linkLabel2.TabIndex = 4;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Ventsislav Georgiev";
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
			// 
			// linkLabel3
			// 
			this.linkLabel3.AutoSize = true;
			this.linkLabel3.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.linkLabel3.LinkColor = System.Drawing.SystemColors.Highlight;
			this.linkLabel3.Location = new System.Drawing.Point(524, 188);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(151, 22);
			this.linkLabel3.TabIndex = 5;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "Lubomir Ganchev";
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
			// 
			// linkLabel4
			// 
			this.linkLabel4.AutoSize = true;
			this.linkLabel4.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.linkLabel4.LinkColor = System.Drawing.SystemColors.Highlight;
			this.linkLabel4.Location = new System.Drawing.Point(524, 233);
			this.linkLabel4.Name = "linkLabel4";
			this.linkLabel4.Size = new System.Drawing.Size(111, 22);
			this.linkLabel4.TabIndex = 6;
			this.linkLabel4.TabStop = true;
			this.linkLabel4.Text = "Mihail Vakov";
			this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
			// 
			// linkLabel5
			// 
			this.linkLabel5.AutoSize = true;
			this.linkLabel5.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.linkLabel5.LinkColor = System.Drawing.SystemColors.Highlight;
			this.linkLabel5.Location = new System.Drawing.Point(524, 271);
			this.linkLabel5.Name = "linkLabel5";
			this.linkLabel5.Size = new System.Drawing.Size(139, 22);
			this.linkLabel5.TabIndex = 7;
			this.linkLabel5.TabStop = true;
			this.linkLabel5.Text = "Hristo Buyukliev";
			this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(12, 168);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(255, 22);
			this.label4.TabIndex = 8;
			this.label4.Text = " Team Gabriel Garcia Marquez";
			// 
			// Credits
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(729, 424);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.linkLabel5);
			this.Controls.Add(this.linkLabel4);
			this.Controls.Add(this.linkLabel3);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Credits";
			this.Text = "Credits";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.LinkLabel linkLabel4;
		private System.Windows.Forms.LinkLabel linkLabel5;
		private Label label4;
	}
}