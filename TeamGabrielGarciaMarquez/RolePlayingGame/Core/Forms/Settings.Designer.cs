namespace RolePlayingGame.Core.Forms
{
	partial class Settings
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
			this.lbl_Autosave = new System.Windows.Forms.Label();
			this.lbl_Sound = new System.Windows.Forms.Label();
			this.cb_Autosave = new System.Windows.Forms.CheckBox();
			this.track_Wave = new System.Windows.Forms.TrackBar();
			this.btn_Ok = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.track_Wave)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.lbl_Autosave.AutoSize = true;
			this.lbl_Autosave.Location = new System.Drawing.Point(31, 104);
			this.lbl_Autosave.Name = "label1";
			this.lbl_Autosave.Size = new System.Drawing.Size(38, 13);
			this.lbl_Autosave.TabIndex = 3;
			this.lbl_Autosave.Text = "Sound";
			// 
			// LBL_Sound
			// 
			this.lbl_Sound.AutoSize = true;
			this.lbl_Sound.Location = new System.Drawing.Point(31, 38);
			this.lbl_Sound.Name = "LBL_Sound";
			this.lbl_Sound.Size = new System.Drawing.Size(52, 13);
			this.lbl_Sound.TabIndex = 2;
			this.lbl_Sound.Text = "Autosave";
			// 
			// CB_Autosave
			// 
			this.cb_Autosave.AutoSize = true;
			this.cb_Autosave.Checked = true;
			this.cb_Autosave.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_Autosave.Location = new System.Drawing.Point(160, 37);
			this.cb_Autosave.Name = "CB_Autosave";
			this.cb_Autosave.Size = new System.Drawing.Size(15, 14);
			this.cb_Autosave.TabIndex = 0;
			this.cb_Autosave.UseVisualStyleBackColor = true;
			this.cb_Autosave.CheckedChanged += new System.EventHandler(this.cb_Autosave_Changed);
			// 
			// TRK_BAR_Sound
			// 
			this.track_Wave.Location = new System.Drawing.Point(160, 92);
			this.track_Wave.Name = "TRK_BAR_Sound";
			this.track_Wave.Size = new System.Drawing.Size(104, 45);
			this.track_Wave.TabIndex = 6;
			this.track_Wave.Value = 10;
			this.track_Wave.Scroll += new System.EventHandler(this.trackWave_Scroll);
			// 
			// BTN_Ok
			// 
			this.btn_Ok.Location = new System.Drawing.Point(39, 152);
			this.btn_Ok.Name = "BTN_Ok";
			this.btn_Ok.Size = new System.Drawing.Size(95, 34);
			this.btn_Ok.TabIndex = 10;
			this.btn_Ok.Text = "OK";
			this.btn_Ok.UseVisualStyleBackColor = true;
			this.btn_Ok.Click += new System.EventHandler(this.BtnOkClick);
			// 
			// Settings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(301, 209);
			this.Controls.Add(this.btn_Ok);
			this.Controls.Add(this.track_Wave);
			this.Controls.Add(this.lbl_Autosave);
			this.Controls.Add(this.lbl_Sound);
			this.Controls.Add(this.cb_Autosave);
			this.Name = "Settings";
			this.ShowIcon = false;
			this.Text = "Ninja.Net Settings";
			((System.ComponentModel.ISupportInitialize)(this.track_Wave)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_Autosave;
		private System.Windows.Forms.Label lbl_Sound;
		private System.Windows.Forms.CheckBox cb_Autosave;
		private System.Windows.Forms.TrackBar track_Wave;
		private System.Windows.Forms.Button btn_Ok;

	}
}