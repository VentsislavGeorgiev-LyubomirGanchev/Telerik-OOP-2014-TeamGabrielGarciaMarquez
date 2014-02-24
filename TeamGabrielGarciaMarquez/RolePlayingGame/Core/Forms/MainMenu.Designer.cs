namespace RolePlayingGame.Core.Forms
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.BTN_NewGame = new System.Windows.Forms.Button();
            this.BTN_LoadGame = new System.Windows.Forms.Button();
            this.BTN_SaveGame = new System.Windows.Forms.Button();
            this.BTN_Credits = new System.Windows.Forms.Button();
            this.BTN_HighScores = new System.Windows.Forms.Button();
            this.BTN_Settings = new System.Windows.Forms.Button();
            this.TXT_Title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BTN_NewGame
            // 
            this.BTN_NewGame.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTN_NewGame.Location = new System.Drawing.Point(105, 164);
            this.BTN_NewGame.Name = "BTN_NewGame";
            this.BTN_NewGame.Size = new System.Drawing.Size(88, 36);
            this.BTN_NewGame.TabIndex = 1;
            this.BTN_NewGame.Text = "New Game";
            this.BTN_NewGame.UseVisualStyleBackColor = true;
            this.BTN_NewGame.Click += new System.EventHandler(this.Button1Click);
            // 
            // BTN_LoadGame
            // 
            this.BTN_LoadGame.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTN_LoadGame.Location = new System.Drawing.Point(328, 164);
            this.BTN_LoadGame.Name = "BTN_LoadGame";
            this.BTN_LoadGame.Size = new System.Drawing.Size(88, 36);
            this.BTN_LoadGame.TabIndex = 2;
            this.BTN_LoadGame.Text = "Load Game";
            this.BTN_LoadGame.UseVisualStyleBackColor = true;
            // 
            // BTN_SaveGame
            // 
            this.BTN_SaveGame.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTN_SaveGame.Location = new System.Drawing.Point(546, 164);
            this.BTN_SaveGame.Name = "BTN_SaveGame";
            this.BTN_SaveGame.Size = new System.Drawing.Size(88, 36);
            this.BTN_SaveGame.TabIndex = 3;
            this.BTN_SaveGame.Text = "Save Game";
            this.BTN_SaveGame.UseVisualStyleBackColor = true;
            this.BTN_SaveGame.Click += new System.EventHandler(this.BtnSaveGameClick);
            // 
            // BTN_Credits
            // 
            this.BTN_Credits.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTN_Credits.Location = new System.Drawing.Point(546, 276);
            this.BTN_Credits.Name = "BTN_Credits";
            this.BTN_Credits.Size = new System.Drawing.Size(88, 36);
            this.BTN_Credits.TabIndex = 4;
            this.BTN_Credits.Text = "Credits";
            this.BTN_Credits.UseVisualStyleBackColor = true;
            // 
            // BTN_HighScores
            // 
            this.BTN_HighScores.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTN_HighScores.Location = new System.Drawing.Point(328, 276);
            this.BTN_HighScores.Name = "BTN_HighScores";
            this.BTN_HighScores.Size = new System.Drawing.Size(88, 36);
            this.BTN_HighScores.TabIndex = 5;
            this.BTN_HighScores.Text = "High Scores";
            this.BTN_HighScores.UseVisualStyleBackColor = true;
            // 
            // BTN_Settings
            // 
            this.BTN_Settings.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTN_Settings.Location = new System.Drawing.Point(105, 276);
            this.BTN_Settings.Name = "BTN_Settings";
            this.BTN_Settings.Size = new System.Drawing.Size(88, 36);
            this.BTN_Settings.TabIndex = 6;
            this.BTN_Settings.Text = "Settings";
            this.BTN_Settings.UseVisualStyleBackColor = true;
            this.BTN_Settings.Click += new System.EventHandler(this.BtnSettingsClick);
            // 
            // TXT_Title
            // 
            this.TXT_Title.BackColor = System.Drawing.Color.Transparent;
            this.TXT_Title.Font = new System.Drawing.Font("Times New Roman", 17.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TXT_Title.Location = new System.Drawing.Point(89, 23);
            this.TXT_Title.Name = "TXT_Title";
            this.TXT_Title.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TXT_Title.Size = new System.Drawing.Size(236, 65);
            this.TXT_Title.TabIndex = 7;
            this.TXT_Title.Text = "The Game of Ages";
            this.TXT_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TXT_Title.Click += new System.EventHandler(this.Label1Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::RolePlayingGame.Properties.Resources._112526;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(728, 413);
            this.Controls.Add(this.TXT_Title);
            this.Controls.Add(this.BTN_Settings);
            this.Controls.Add(this.BTN_HighScores);
            this.Controls.Add(this.BTN_Credits);
            this.Controls.Add(this.BTN_SaveGame);
            this.Controls.Add(this.BTN_LoadGame);
            this.Controls.Add(this.BTN_NewGame);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BTN_NewGame;
        private System.Windows.Forms.Button BTN_LoadGame;
        private System.Windows.Forms.Button BTN_SaveGame;
        private System.Windows.Forms.Button BTN_Credits;
        private System.Windows.Forms.Button BTN_HighScores;
        private System.Windows.Forms.Button BTN_Settings;
        private System.Windows.Forms.Label TXT_Title;
    }
}