using System.Collections.Generic;
using System.Drawing;

namespace RolePlayingGame.Core.Utils
{
	/// <summary>
	/// Provides a cache of bitmaps. Will return the existing reference if it exists or load a new one
	/// </summary>
	internal class BitmapCache
	{
        private static readonly Dictionary<string, Bitmap> _Bitmaps = new Dictionary<string, Bitmap>();

		public Bitmap this[string filename]
		{
			get
			{
				//If this bitmap is not in the cache then load it
				if (!_Bitmaps.ContainsKey(filename))
				{
					_Bitmaps.Add(filename, new Bitmap(filename));
				}
				return _Bitmaps[filename];
			}
		}
	}
}