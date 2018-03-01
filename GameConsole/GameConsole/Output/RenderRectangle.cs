// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Draws a Rectangle.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameConsole.Output
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Draws a <see cref="Rectangle"/>.
    /// </summary>
    public class RenderRectangle
    {
        /// <summary>
        /// The <see cref="Texture2D"/> needed to draw this <see cref="RenderRectangle"/>.
        /// </summary>
        private Texture2D onePixel;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderRectangle"/> class.
        /// </summary>
        /// <param name="rectangle">
        /// The base <see cref="Rectangle"/> to be drawn.
        /// </param>
        /// <param name="color">
        /// A <see cref="Microsoft.Xna.Framework.Color"/> used to fill the <see cref="RenderRectangle"/>.
        /// </param>
        /// <param name="graphics">
        /// A <see cref="GraphicsDevice"/> containing needed graphical information.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="rectangle"/> and/or <paramref name="color"/> and/or <paramref name="graphics"/> is/are NULL.
        /// </exception>
        public RenderRectangle(Rectangle rectangle, Color color, GraphicsDevice graphics)
        {
            if (rectangle == null)
            {
                throw new ArgumentNullException("rectangle");
            }

            if (color == null)
            {
                throw new ArgumentNullException("color");
            }

            if (graphics == null)
            {
                throw new ArgumentNullException("game");
            }

            this.onePixel = new Texture2D(graphics, 1, 1);
            this.onePixel.SetData<Color>(new Color[] { Color.FromNonPremultiplied(255, 255, 255, 255) });
            this.Rectangle = rectangle;
            this.Color = color;
        }

        /// <summary>
        /// Gets or sets the <see cref="Microsoft.Xna.Framework.Color"/> of this <see cref="RenderRectangle"/>.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Rectangle"/> to be drawn.
        /// </summary>
        public Rectangle Rectangle { get; set; }

        /// <summary>
        /// Draws this <see cref="RenderRectangle"/>.
        /// </summary>
        /// <param name="spriteBatch">
        /// A <see cref="SpriteBatch"/> used to draw this <see cref="RenderRectangle"/>.
        /// </param>
        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.onePixel, this.Rectangle, this.Color);
        }
    }
}
