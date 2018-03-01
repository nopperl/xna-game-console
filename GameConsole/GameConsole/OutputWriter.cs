// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Links the output of <see cref="System.Console"/> with <see cref="GameConsole.Console"/>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameConsole
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Links the output of <see cref="System.Console"/> with <see cref="GameConsole.Console"/>.
    /// </summary>
    internal class OutputWriter : TextWriter
    {
        /// <summary>
        /// The <see cref="OutputType"/> of the outputs of this <see cref="ConsoleOutputWriter"/>.
        /// </summary>
        private readonly OutputType outputType;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputWriter"/> class.
        /// </summary>
        /// <param name="outputType">
        /// The <see cref="OutputType"/> of the data passed to this <see cref="OutputWriter"/>.
        /// </param>
        public OutputWriter(OutputType outputType)
        {
            this.outputType = outputType;
        }

        /// <inheritdoc/>
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        /// <inheritdoc/>
        public override void Write(bool value)
        {
            Console.Write(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(char value)
        {
            Console.Write(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(char[] buffer)
        {
            Console.Write(new string(buffer), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(char[] buffer, int index, int count)
        {
            Console.Write(new string(buffer, index, count), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(decimal value)
        {
            Console.Write(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(double value)
        {
            Console.Write(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(float value)
        {
            Console.Write(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(int value)
        {
            Console.Write(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(long value)
        {
            Console.Write(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(object value)
        {
            Console.Write(value.ToString(), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(string format, object arg0)
        {
            Console.Write(string.Format(format, arg0), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(string format, object arg0, object arg1)
        {
            Console.Write(string.Format(format, arg0, arg1), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            Console.Write(string.Format(format, arg0, arg1, arg2), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(string format, params object[] arg)
        {
            Console.Write(string.Format(format, arg), this.outputType);

        }

        /// <inheritdoc/>
        public override void Write(string value)
        {
            Console.Write(value, this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(uint value)
        {
            Console.Write(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void Write(ulong value)
        {
            Console.Write(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine()
        {
            Console.WriteLine(string.Empty, this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(bool value)
        {
            Console.WriteLine(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(char value)
        {
            Console.WriteLine(Convert.ToString(value), this.outputType);
        }
        
        /// <inheritdoc/>
        public override void WriteLine(char[] buffer)
        {
            Console.WriteLine(new string(buffer), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(char[] buffer, int index, int count)
        {
            Console.WriteLine(new string(buffer, index, count), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(decimal value)
        {
            Console.WriteLine(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(double value)
        {
            Console.WriteLine(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(float value)
        {
            Console.WriteLine(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(int value)
        {
            Console.WriteLine(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(long value)
        {
            Console.WriteLine(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(object value)
        {
            Console.WriteLine(value.ToString(), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(string format, object arg0)
        {
            Console.WriteLine(string.Format(format, arg0), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(string format, object arg0, object arg1)
        {
            Console.WriteLine(string.Format(format, arg0, arg1), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            Console.WriteLine(string.Format(format, arg0, arg1, arg2), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(string.Format(format, arg), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(string value)
        {
            Console.WriteLine(value, this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(uint value)
        {
            Console.WriteLine(Convert.ToString(value), this.outputType);
        }

        /// <inheritdoc/>
        public override void WriteLine(ulong value)
        {
            Console.WriteLine(Convert.ToString(value), this.outputType);
        }
    }
}
