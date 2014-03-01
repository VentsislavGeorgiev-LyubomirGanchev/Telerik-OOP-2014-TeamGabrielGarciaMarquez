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
			this.btn_NewGame = new System.Windows.Forms.Button();
			this.btn_LoadGame = new System.Windows.Forms.Button();
			this.btn_SaveGame = new System.Windows.Forms.Button();
			this.btn_Credits = new System.Windows.Forms.Button();
			this.btn_HighScores = new System.Windows.Forms.Button();
			this.btn_Settings = new System.Windows.Forms.Button();
			this.txt_Title = new System.Windows.Forms.Label();
			this.btn_Restart = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btn_NewGame
			// 
			this.btn_NewGame.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btn_NewGame.Location = new System.Drawing.Point(22, 110);
			this.btn_NewGame.Name = "btn_NewGame";
			this.btn_NewGame.Size = new System.Drawing.Size(262, 36);
			this.btn_NewGame.TabIndex = 1;
			this.btn_NewGame.Text = "New Game";
			this.btn_NewGame.UseVisualStyleBackColor = true;
			this.btn_NewGame.Click += new System.EventHandler(this.NewGame);
			// 
			// btn_LoadGame
			// 
			this.btn_LoadGame.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btn_LoadGame.Location = new System.Drawing.Point(22, 199);
			this.btn_LoadGame.Name = "btn_LoadGame";
			this.btn_LoadGame.Size = new System.Drawing.Size(262, 36);
			this.btn_LoadGame.TabIndex = 2;
			this.btn_LoadGame.Text = "Load Game (F6)";
			this.btn_LoadGame.UseVisualStyleBackColor = true;
			this.btn_LoadGame.Click += new System.EventHandler(this.BtnLoadGameClick);
			// 
			// btn_SaveGame
			// 
			this.btn_SaveGame.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btn_SaveGame.Location = new System.Drawing.Point(22, 285);
			this.btn_SaveGame.Name = "btn_SaveGame";
			this.btn_SaveGame.Size = new System.Drawing.Size(262, 36);
			this.btn_SaveGame.TabIndex = 3;
			this.btn_SaveGame.Text = "Save Game (F5)";
			this.btn_SaveGame.UseVisualStyleBackColor = true;
			this.btn_SaveGame.Click += new System.EventHandler(this.BtnSaveGameClick);
			// 
			// btn_Credits
			// 
			this.btn_Credits.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btn_Credits.Location = new System.Drawing.Point(442, 285);
			this.btn_Credits.Name = "btn_Credits";
			this.btn_Credits.Size = new System.Drawing.Size(262, 36);
			this.btn_Credits.TabIndex = 4;
			this.btn_Credits.Text = "Credits";
			this.btn_Credits.UseVisualStyleBackColor = true;
			this.btn_Credits.Click += new System.EventHandler(this.btn_Credits_Click);
			// 
			// btn_HighScores
			// 
			this.btn_HighScores.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btn_HighScores.Location = new System.Drawing.Point(442, 199);
			this.btn_HighScores.Name = "btn_HighScores";
			this.btn_HighScores.Size = new System.Drawing.Size(262, 36);
			this.btn_HighScores.TabIndex = 5;
			this.btn_HighScores.Text = "High Scores";
			this.btn_HighScores.UseVisualStyleBackColor = true;
			// 
			// btn_Settings
			// 
			this.btn_Settings.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btn_Settings.Location = new System.Drawing.Point(442, 110);
			this.btn_Settings.Name = "btn_Settings";
			this.btn_Settings.Size = new System.Drawing.Size(262, 36);
			this.btn_Settings.TabIndex = 6;
			this.btn_Settings.Text = "Settings";
			this.btn_Settings.UseVisualStyleBackColor = true;
			this.btn_Settings.Click += new System.EventHandler(this.BtnSettingsClick);
			// 
			// txt_Title
			// 
			this.txt_Title.BackColor = System.Drawing.Color.Transparent;
			this.txt_Title.Font = new System.Drawing.Font("Times New Roman", 35F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.txt_Title.ForeColor = System.Drawing.Color.Lime;
			this.txt_Title.Location = new System.Drawing.Point(233, 9);
			this.txt_Title.Name = "txt_Title";
			this.txt_Title.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txt_Title.Size = new System.Drawing.Size(236, 65);
			this.txt_Title.TabIndex = 7;
			this.txt_Title.Text = "Ninja.Net";
			this.txt_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btn_Restart
			// 
			this.btn_Restart.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btn_Restart.Location = new System.Drawing.Point(229, 354);
			this.btn_Restart.Name = "btn_Restart";
			this.btn_Restart.Size = new System.Drawing.Size(262, 36);
			this.btn_Restart.TabIndex = 8;
			this.btn_Restart.Text = "Restart Game";
			this.btn_Restart.UseVisualStyleBackColor = true;
			this.btn_Restart.Visible = false;
			this.btn_Restart.Click += new System.EventHandler(this.btn_Restart_Click);
			// 
			// MainMenu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::RolePlayingGame.Properties.Resources.ninjaNet;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(728, 413);
			this.Controls.Add(this.btn_Restart);
			this.Controls.Add(this.txt_Title);
			this.Controls.Add(this.btn_Settings);
			this.Controls.Add(this.btn_HighScores);
			this.Controls.Add(this.btn_Credits);
			this.Controls.Add(this.btn_SaveGame);
			this.Controls.Add(this.btn_LoadGame);
			this.Controls.Add(this.btn_NewGame);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainMenu";
			this.Text = "Ninja.Net";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_NewGame;
		private System.Windows.Forms.Button btn_LoadGame;
		private System.Windows.Forms.Button btn_SaveGame;
		private System.Windows.Forms.Button btn_Credits;
		private System.Windows.Forms.Button btn_HighScores;
		private System.Windows.Forms.Button btn_Settings;
		private System.Windows.Forms.Label txt_Title;
		private System.Windows.Forms.Button btn_Restart;
	}
}