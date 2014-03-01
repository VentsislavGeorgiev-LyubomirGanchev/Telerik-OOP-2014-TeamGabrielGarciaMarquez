using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime;

namespace RolePlayingGame.UI
{
	/// <summary>
	/// GDI Graphics class wrapper
	/// </summary>
	internal class GDIRenderer : IRenderer
	{
		private Graphics _graphics;

		public GDIRenderer()
		{
		}

		public GDIRenderer(Graphics graphics)
		{
			this._graphics = graphics;
		}

		public void SetGraphics(Graphics graphics)
		{
			this._graphics = graphics;
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn text. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		public void DrawString(string s, Font font, Brush brush, float x, float y)
		{
			this.DrawString(s, font, brush, new RectangleF(x, y, 0f, 0f), null);
		}

		public void DrawStringWithOutline(string s, Font font, Brush brush, Brush outlineBrush, float x, float y)
		{
			this.DrawString(s, font, outlineBrush, x + 2, y);
			this.DrawString(s, font, outlineBrush, x - 1, y);
			this.DrawString(s, font, outlineBrush, x, y + 2);
			this.DrawString(s, font, outlineBrush, x, y - 2);
			this.DrawString(s, font, brush, x, y);
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the upper-left corner of the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		public void DrawString(string s, Font font, Brush brush, PointF point)
		{
			this.DrawString(s, font, brush, new RectangleF(point.X, point.Y, 0f, 0f), null);
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn text. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn text. </param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format)
		{
			this.DrawString(s, font, brush, new RectangleF(x, y, 0f, 0f), format);
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the upper-left corner of the drawn text. </param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
		{
			this.DrawString(s, font, brush, new RectangleF(point.X, point.Y, 0f, 0f), format);
		}

		/// <summary>Draws the specified text string in the specified rectangle with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="layoutRectangle">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location of the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle)
		{
			this.DrawString(s, font, brush, layoutRectangle, null);
		}

		/// <summary>Draws the specified text string in the specified rectangle with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="layoutRectangle">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location of the drawn text. </param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
		{
			this._graphics.DrawString(s, font, brush, layoutRectangle, format);
		}

		//
		// Summary:
		//     Draws the specified System.Drawing.Image at the specified location and with
		//     the specified size.
		//
		// Parameters:
		//   image:
		//     System.Drawing.Image to draw.
		//
		//   x:
		//     The x-coordinate of the upper-left corner of the drawn image.
		//
		//   y:
		//     The y-coordinate of the upper-left corner of the drawn image.
		//
		//   width:
		//     Width of the drawn image.
		//
		//   height:
		//     Height of the drawn image.
		//
		// Exceptions:
		//   System.ArgumentNullException:
		//     image is null.
		public void DrawImage(Image image, float x, float y, float width, float height)
		{
			this._graphics.DrawImage(image, x, y, width, height);
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcWidth">Width of the portion of the source image to draw. </param>
		/// <param name="srcHeight">Height of the portion of the source image to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit)
		{
			this.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, null);
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcWidth">Width of the portion of the source image to draw. </param>
		/// <param name="srcHeight">Height of the portion of the source image to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle. </param>
		/// <param name="imageAttrs">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs)
		{
			this._graphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, null);
		}
	}
}