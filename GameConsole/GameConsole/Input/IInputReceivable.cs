// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Defines methods to receive input.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameConsole.Input
{
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Defines methods to receive input.
    /// </summary>
    public interface IInputReceivable
    {

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IInputRecievable"/> is focused.
        /// </summary>
        bool Focused { get; set; }

        /// <summary>
        /// Receives the last entered <see cref="char"/>.
        /// </summary>
        /// <param name="inputChar">
        /// The last entered <see cref="char"/>
        /// </param>
        void RecieveTextInput(char inputChar);

        /// <summary>
        /// Receives the last entered <see cref="string"/>.
        /// </summary>
        /// <param name="text">
        /// The last entered <see cref="string"/>.
        /// </param>
        void RecieveTextInput(string text);

        /// <summary>
        /// Receives the last command.
        /// </summary>
        /// <param name="command">
        /// The last command as <see cref="char"/>.
        /// </param>
        void RecieveCommandInput(char command);

        /// <summary>
        /// Receives the last entered <see cref="Key"/>.
        /// </summary>
        /// <param name="key">
        /// The last entered <see cref="Key"/>.
        /// </param>
        void RecieveSpecialInput(Keys key);
    }
}
