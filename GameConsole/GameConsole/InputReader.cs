// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Links the input calls of <see cref="System.Console"/> with <see cref="GameConsole.Console"/>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameConsole
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Links the input calls of <see cref="System.Console"/> with <see cref="GameConsole.Console"/>.
    /// </summary>
    internal class InputReader : TextReader
    {
        /// <inheritdoc/>
        public override string ReadLine()
        {
            Console.ReadLine();
            return string.Empty;
        }

        private Task<string> ReadLineAsync()
        {
            throw new NotImplementedException();
        }
    }
}
