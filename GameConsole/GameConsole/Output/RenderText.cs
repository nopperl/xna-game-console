// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Displays a string.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameConsole.Output
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Displays a <see cref="string"/>.
    /// </summary>
    public class RenderText
    {
        /// <summary>
        /// A value indicating the drawing scale of the <see cref="Text"/>.
        /// </summary>
        private float textScale;

        /// <summary>
        /// A <see cref="Vector2"/> holding the position of this <see cref="RenderText"/>.
        /// </summary>
        private Vector2 textPosition;

        /// <summary>
        /// The <see cref="SpriteFont"/> used to draw the <see cref="Text"/>.
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderText"/> class.
        /// </summary>
        /// <param name="position">
        /// A <see cref="Vector2"/> holding the position of the <see cref="RenderText"/>.
        /// </param>
        /// <param name="text">
        /// The <see cref="string"/> to be drawn.
        /// </param>
        /// <param name="color">
        /// The <see cref="Color"/> in which the <see cref="Text"/> is drawn.
        /// </param>
        /// <param name="font">
        /// The <see cref="SpriteFont"/> used to draw the <see cref="Text"/>.
        /// </param>
        /// <param name="scaleMultiplier">
        /// A value indicating a scale multiplier for the drawing scale of the <see cref="Text"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="color"/> and/or <see cref="font"/> is/are NULL.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="text"/> must consist of at least one character.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="scaleMultiplier"/> must be greater than or equals 0.
        /// </exception>
        public RenderText(Vector2 position, string text, Color color, SpriteFont font, float scaleMultiplier)
        {
            if (color == null)
            {
                throw new ArgumentNullException("color");
            }

            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            if (text.Equals(string.Empty))
            {
                throw new ArgumentException("text must consist of at least one character.", "text");
            }

            if (scaleMultiplier < 0)
            {
                throw new ArgumentOutOfRangeException("scaleMultiplier must be greater than or equals 0.", "scaleMultiplier");
            }

            this.Text = text;

            this.TextColor = color;

            Vector2 measuredText = font.MeasureString(this.Text);
            this.WholeRectangle = new Rectangle((int)position.X, (int)position.Y, (int)measuredText.X, (int)measuredText.Y);

            this.textScale = scaleMultiplier;
            this.textPosition = position;

            this.font = font;

            measuredText = new Vector2(measuredText.X * scaleMultiplier, measuredText.Y * scaleMultiplier);
            this.WholeRectangle = new Rectangle((int)position.X, (int)position.Y, (int)measuredText.X, (int)measuredText.Y);
        }

        /// <summary>
        /// Gets the whole <see cref="Rectangle"/> drawn by this <see cref="RenderText"/>.
        /// </summary>
        public Rectangle WholeRectangle { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="string"/> to be drawn.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Color"/> in which the <see cref="Text"/> is drawn.
        /// </summary>
        public Color TextColor { get; set; }

        /// <inheritdoc/>
        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.font, this.Text, this.textPosition, this.TextColor, 0F, Vector2.Zero, this.textScale, SpriteEffects.None, 1F);
        }

        /// <inheritdoc/>
        public void SetPosition(float x, float y)
        {
            this.WholeRectangle = new Rectangle((int)x, (int)y, this.WholeRectangle.Width, this.WholeRectangle.Height);
            this.textPosition = new Vector2(x, y);
        }
    }
}
