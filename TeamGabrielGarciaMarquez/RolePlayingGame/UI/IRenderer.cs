using System.Drawing;
using System.Drawing.Imaging;

namespace RolePlayingGame.UI
{
	internal interface IRenderer
	{
		// Summary:
		//     Draws the specified text string at the specified location with the specified
		//     System.Drawing.Brush and System.Drawing.Font objects.
		//
		// Parameters:
		//   s:
		//     String to draw.
		//
		//   font:
		//     System.Drawing.Font that defines the text format of the string.
		//
		//   brush:
		//     System.Drawing.Brush that determines the color and texture of the drawn text.
		//
		//   x:
		//     The x-coordinate of the upper-left corner of the drawn text.
		//
		//   y:
		//     The y-coordinate of the upper-left corner of the drawn text.
		//
		// Exceptions:
		//   System.ArgumentNullException:
		//     brush is null.-or-s is null.

		void DrawString(string s, Font font, Brush brush, float x, float y);

		void DrawStringWithOutline(string s, Font font, Brush brush, Brush outlineBrush, float x, float y);

		//
		// Summary:
		//     Draws the specified text string at the specified location with the specified
		//     System.Drawing.Brush and System.Drawing.Font objects using the formatting
		//     attributes of the specified System.Drawing.StringFormat.
		//
		// Parameters:
		//   s:
		//     String to draw.
		//
		//   font:
		//     System.Drawing.Font that defines the text format of the string.
		//
		//   brush:
		//     System.Drawing.Brush that determines the color and texture of the drawn text.
		//
		//   point:
		//     System.Drawing.PointF structure that specifies the upper-left corner of the
		//     drawn text.
		//
		//   format:
		//     System.Drawing.StringFormat that specifies formatting attributes, such as
		//     line spacing and alignment, that are applied to the drawn text.
		//
		// Exceptions:
		//   System.ArgumentNullException:
		//     brush is null.-or-s is null.
		void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format);

		//
		// Summary:
		//     Draws the specified text string in the specified rectangle with the specified
		//     System.Drawing.Brush and System.Drawing.Font objects using the formatting
		//     attributes of the specified System.Drawing.StringFormat.
		//
		// Parameters:
		//   s:
		//     String to draw.
		//
		//   font:
		//     System.Drawing.Font that defines the text format of the string.
		//
		//   brush:
		//     System.Drawing.Brush that determines the color and texture of the drawn text.
		//
		//   layoutRectangle:
		//     System.Drawing.RectangleF structure that specifies the location of the drawn
		//     text.
		//
		//   format:
		//     System.Drawing.StringFormat that specifies formatting attributes, such as
		//     line spacing and alignment, that are applied to the drawn text.
		//
		// Exceptions:
		//   System.ArgumentNullException:
		//     brush is null.-or-s is null.
		void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format);

		//
		// Summary:
		//     Draws the specified text string at the specified location with the specified
		//     System.Drawing.Brush and System.Drawing.Font objects using the formatting
		//     attributes of the specified System.Drawing.StringFormat.
		//
		// Parameters:
		//   s:
		//     String to draw.
		//
		//   font:
		//     System.Drawing.Font that defines the text format of the string.
		//
		//   brush:
		//     System.Drawing.Brush that determines the color and texture of the drawn text.
		//
		//   x:
		//     The x-coordinate of the upper-left corner of the drawn text.
		//
		//   y:
		//     The y-coordinate of the upper-left corner of the drawn text.
		//
		//   format:
		//     System.Drawing.StringFormat that specifies formatting attributes, such as
		//     line spacing and alignment, that are applied to the drawn text.
		//
		// Exceptions:
		//   System.ArgumentNullException:
		//     brush is null.-or-s is null.
		void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format);

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
		void DrawImage(Image image, float x, float y, float width, float height);

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
		void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs);

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
		void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit);
	}
}