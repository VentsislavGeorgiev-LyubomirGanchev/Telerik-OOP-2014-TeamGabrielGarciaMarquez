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
            this.label1 = new System.Windows.Forms.Label();
            this.LBL_Resolution = new System.Windows.Forms.Label();
            this.LBL_Sound = new System.Windows.Forms.Label();
            this.CB_Autosave = new System.Windows.Forms.CheckBox();
            this.TRK_BAR_Sound = new System.Windows.Forms.TrackBar();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.BTN_Cancel = new System.Windows.Forms.Button();
            this.BTN_Ok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TRK_BAR_Sound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sound";
            this.label1.Click += new System.EventHandler(this.Label1Click);
            // 
            // LBL_Resolution
            // 
            this.LBL_Resolution.AutoSize = true;
            this.LBL_Resolution.Location = new System.Drawing.Point(31, 176);
            this.LBL_Resolution.Name = "LBL_Resolution";
            this.LBL_Resolution.Size = new System.Drawing.Size(57, 13);
            this.LBL_Resolution.TabIndex = 5;
            this.LBL_Resolution.Text = "Resolution";
            this.LBL_Resolution.Click += new System.EventHandler(this.Resolution_Click);
            // 
            // LBL_Sound
            // 
            this.LBL_Sound.AutoSize = true;
            this.LBL_Sound.Location = new System.Drawing.Point(31, 38);
            this.LBL_Sound.Name = "LBL_Sound";
            this.LBL_Sound.Size = new System.Drawing.Size(52, 13);
            this.LBL_Sound.TabIndex = 2;
            this.LBL_Sound.Text = "Autosave";
            // 
            // CB_Autosave
            // 
            this.CB_Autosave.AutoSize = true;
            this.CB_Autosave.Checked = true;
            this.CB_Autosave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_Autosave.Location = new System.Drawing.Point(160, 37);
            this.CB_Autosave.Name = "CB_Autosave";
            this.CB_Autosave.Size = new System.Drawing.Size(15, 14);
            this.CB_Autosave.TabIndex = 0;
            this.CB_Autosave.UseVisualStyleBackColor = true;
            this.CB_Autosave.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
            // 
            // TRK_BAR_Sound
            // 
            this.TRK_BAR_Sound.Location = new System.Drawing.Point(160, 92);
            this.TRK_BAR_Sound.Name = "TRK_BAR_Sound";
            this.TRK_BAR_Sound.Size = new System.Drawing.Size(104, 45);
            this.TRK_BAR_Sound.TabIndex = 6;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(160, 163);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 7;
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.Location = new System.Drawing.Point(172, 213);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(87, 34);
            this.BTN_Cancel.TabIndex = 9;
            this.BTN_Cancel.Text = "Cancel";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // BTN_Ok
            // 
            this.BTN_Ok.Location = new System.Drawing.Point(34, 213);
            this.BTN_Ok.Name = "BTN_Ok";
            this.BTN_Ok.Size = new System.Drawing.Size(95, 34);
            this.BTN_Ok.TabIndex = 10;
            this.BTN_Ok.Text = "OK";
            this.BTN_Ok.UseVisualStyleBackColor = true;
            this.BTN_Ok.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.BTN_Ok);
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.TRK_BAR_Sound);
            this.Controls.Add(this.LBL_Resolution);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LBL_Sound);
            this.Controls.Add(this.CB_Autosave);
            this.Name = "Settings";
            this.ShowIcon = false;
            this.Text = "Ninja.Net Settings";
            ((System.ComponentModel.ISupportInitialize)(this.TRK_BAR_Sound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LBL_Resolution;
        private System.Windows.Forms.Label LBL_Sound;
        private System.Windows.Forms.CheckBox CB_Autosave;
        private System.Windows.Forms.TrackBar TRK_BAR_Sound;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button BTN_Cancel;
        private System.Windows.Forms.Button BTN_Ok;

    }
}