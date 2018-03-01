// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Represents a keyboard layout.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameConsole.Input
{
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Represents a keyboard layout.
    /// </summary>
    public class KeyboardLayout
    {
        /// <summary>
        /// Activates the layout.
        /// </summary>
        private const uint KLF_ACTIVATE = 1;

        /// <summary>
        /// A value indicating the length of the keyboard buffer.
        /// </summary>
        private const int KeyboardLayoutNameLength = 9;

        /// <summary>
        /// A value indicating the code for English keyboard layouts.
        /// </summary>
        private const string LANG_EN_US = "00000409";

        /// <summary>
        /// A value indicating the code for Hebrew keyboard layouts.
        /// </summary>
        private const string LANG_HE_IL = "0001101A";


        /// <summary>
        /// Gets the name of this <see cref="KeyboardLayout"/>.
        /// </summary>
        /// <returns>
        /// The name of this <see cref="KeyboardLayout"/>.
        /// </returns>
        public static string GetName()
        {
            StringBuilder name = new StringBuilder(KeyboardLayoutNameLength);
            GetKeyboardLayoutName(name);
            return name.ToString();
        }

        /// <summary>
        /// Loads the specific <see cref="KeyboardLayout"/>.
        /// </summary>
        /// <param name="pwszKLID">
        /// A <see cref="string"/> indicating the locale identifier of the input.
        /// </param>
        /// <param name="Flags">
        /// A <see cref="uint"/> holding options of the local identifier of the input.
        /// </param>
        /// <returns>
        /// A <see cref="long"/> representing the operation.
        /// </returns>
        [DllImport("user32.dll")]
        private static extern long LoadKeyboardLayout(
              string pwszKLID,
              uint Flags
              );

        /// <summary>
        /// Gets the name of the current layout.
        /// </summary>
        /// <param name="pwszKLID">
        /// A <see cref="string"/> indicating the locale identifier of the input.
        /// </param>
        /// <returns>
        /// A <see cref="long"/> representing the operation.
        /// </returns>
        [DllImport("user32.dll")]
        private static extern long GetKeyboardLayoutName(
              StringBuilder pwszKLID
              );
    }
}
