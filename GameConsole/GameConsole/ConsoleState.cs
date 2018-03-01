// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Indicates the state of the <see cref="GameConsole.Console"/>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameConsole
{
    /// <summary>
    /// Indicates the state of the <see cref="GameConsole.Console"/>.
    /// </summary>
    public enum ConsoleState
    {
        /// <summary>
        /// The console is doing nothing.
        /// </summary>
        Nothing,

        /// <summary>
        /// The console is writing.
        /// </summary>
        Writing,

        /// <summary>
        /// The console is reading.
        /// </summary>
        Reading
    }
}
