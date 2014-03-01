using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RolePlayingGame.Core.Forms
{
	public partial class Settings : Form
	{
		[DllImport("winmm.dll")]
		public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

		[DllImport("winmm.dll")]
		public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

		public Settings()
		{
			this.InitializeComponent();
			// By the default set the volume to 0
			uint CurrVol = 0;
			// At this point, CurrVol gets assigned the volume
			waveOutGetVolume(IntPtr.Zero, out CurrVol);
			// Calculate the volume
			ushort CalcVol = (ushort)(CurrVol & 0x0000ffff);
			// Get the volume on a scale of 1 to 10 (to fit the trackbar)
			track_Wave.Value = CalcVol / (ushort.MaxValue / 10);
		}

		private void cb_Autosave_Changed(object sender, EventArgs e)
		{
		}

		private void BtnOkClick(object sender, EventArgs e)
		{
			this.Close();
		}

		private void trackWave_Scroll(object sender, EventArgs e)
		{
			// Calculate the volume that's being set. BTW: this is a trackbar!
			int newVolume = ((ushort.MaxValue / 10) * track_Wave.Value);
			// Set the same volume for both the left and the right channels
			uint newVolumeAllChannels = (((uint)newVolume & 0x0000ffff) | ((uint)newVolume << 16));
			// Set the volume
			waveOutSetVolume(IntPtr.Zero, newVolumeAllChannels);
			Sounds.SetBackgroundSoundVolume(track_Wave.Value * 10);
		}
	}
}