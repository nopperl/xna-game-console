// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Contains event data for the <see cref="Key"/> event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameConsole.Input
{
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Contains event data for the <see cref="Key"/> event.
    /// </summary>
    public class KeyEventArgs
    {
        /// <summary>
        /// The <see cref="Key"/> associated with this <see cref="KeyEventArgs"/>.
        /// </summary>
        private Keys keyCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEventArgs"/> class.
        /// </summary>
        /// <param name="keyCode">
        /// A <see cref="Key"/> associated with the <see cref="KeyEventArgs"/>.
        /// </param>
        public KeyEventArgs(Keys keyCode)
        {
            this.keyCode = keyCode;
        }

        /// <summary>
        /// Gets the <see cref="Key"/> associated with this <see cref="KeyEventArgs"/>.
        /// </summary>
        public Keys KeyCode
        {
            get { return this.keyCode; }
        }
    }
}
