// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Shows a console and enables console-like user interaction.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using GameConsole.Input;
    using GameConsole.Output;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System.Threading.Tasks;

    /// <summary>
    /// Shows a console and enables console-like user interaction.
    /// </summary>
    public class Console : IInputReceivable
    {
        /// <summary>
        /// A value indicating the count of lines held and displayed by the <see cref="Console"/>. The <see cref="Console"/> will adapt the given lines dynamically, so, to be fully functional,
        /// a value between 1 and 60 must be given.
        /// </summary>
        private static int lineCount = 10;

        /// <summary>
        /// A value indicating the <see cref="SpriteFont"/>-scale used in the <see cref="Console"/>.
        /// </summary>
        private static float textScale;

        /// <summary>
        /// A value indicating the alpha value of the <see cref="BackgroundColor"/> of the <see cref="Console"/>.
        /// </summary>
        private static float alpha;

        /// <summary>
        /// A <see cref="Color"/> indicating the <see cref="Color"/> used to display <see cref="OutputType.Info"/>.
        /// </summary>
        private static Color outputColor;

        /// <summary>
        /// A <see cref="Color"/> indicating the <see cref="Color"/> used to display <see cref="OutputType.Error"/>.
        /// </summary>
        private static Color errorColor;

        /// <summary>
        /// A <see cref="Color"/> indicating the <see cref="Color"/> used to display <see cref="OutputType.UserInput"/>.
        /// </summary>
        private static Color inputColor;

        /// <summary>
        /// A <see cref="Color"/> indicating the <see cref="Color"/> used to fill the <see cref="Console"/>.
        /// </summary>
        private static Color backgroundColor;

        /// <summary>
        /// The current <see cref="ConsoleState"/> of the <see cref="Console"/>.
        /// </summary>
        private static ConsoleState state;

        /// <summary>
        /// The <see cref="List{string}"/> holding the text of every line of output.
        /// </summary>
        private static List<string> outputStrings;

        /// <summary>
        /// The <see cref="List{string}"/> holding the <see cref="OutputType"/> of every output line.
        /// </summary>
        private static List<OutputType> outputTypes;

        /// <summary>
        /// The <see cref="RenderRectangle"/> used to fill the background of the <see cref="Console"/>.
        /// </summary>
        private static RenderRectangle rectangle;

        /// <summary>
        /// The <see cref="SpriteFont"/> used for <see cref="OutputType.Info"/>.
        /// </summary>
        private static SpriteFont outputFont;

        /// <summary>
        /// The <see cref="SpriteFont"/> used for <see cref="OutputType.Error"/>.
        /// </summary>
        private static SpriteFont errorFont;

        /// <summary>
        /// The <see cref="SpriteFont"/> used for <see cref="OutputType.UserInput"/>.
        /// </summary>
        private static SpriteFont inputFont;

        /// <summary>
        /// The <see cref="TextWriter"/> used to write the standard output of the <see cref="Game"/>.
        /// </summary>
        private static TextWriter outputWriter;

        /// <summary>
        /// The <see cref="TextWriter"/> used to write the standard error output of the <see cref="Game"/>.
        /// </summary>
        private static TextWriter errorWriter;

        /// <summary>
        /// The <see cref="TextWriter"/> used to read the standard input of the <see cref="Game"/>.
        /// </summary>
        private static TextReader inputReader;

        /// <summary>
        /// A value indicating the currently entered input.
        /// </summary>
        private static string input;

        /// <summary>
        /// A value indicating whether the user input was quited.
        /// </summary>
        private static bool inputQuitted;

        /// <summary>
        /// A <see cref="List{RenderText}"/> holding every <see cref="RenderText"/> to be drawn.
        /// </summary>
        private static List<RenderText> renderOutput;

        /// <summary>
        /// The <see cref="Game"/> the <see cref="Console"/> is bound to.
        /// </summary>
        private static Game parent;

        /// <summary>
        /// Gets or sets the standard output stream.
        /// </summary>
        public static TextWriter Out
        {
            get
            {
                return Console.outputWriter;
            }

            set
            {
                Console.outputWriter = value;
            }
        }

        /// <summary>
        /// Gets or sets the standard error output stream.
        /// </summary>
        public static TextWriter Error
        {
            get
            {
                return Console.errorWriter;
            }

            set
            {
                Console.errorWriter = value;
            }
        }

        /// <summary>
        /// Gets or sets the standard input stream.
        /// </summary>
        public static TextReader In
        {
            get
            {
                return Console.inputReader;
            }

            set
            {
                Console.inputReader = value;
            }
        }

        /// <summary>
        /// Gets or sets the position of the <see cref="Console"/> in the <see cref="Game"/>.
        /// </summary>
        public static Rectangle Position
        {
            get
            {
                return Console.rectangle.Rectangle;
            }

            set
            {
                Console.rectangle.Rectangle = value;
            }
        }

        /// <summary>
        /// Gets or sets the background <see cref="Color"/> of the <see cref="Console"/>.
        /// </summary>
        public static Color BackgroundColor
        {
            get
            {
                return Console.backgroundColor;
            }

            set
            {
                Console.backgroundColor = value;
                Console.rectangle.Color = value;
            }
        }

        /// <summary>
        /// Gets or sets the alpha value of the <see cref="Console"/>. Must be within 0F and 1F.
        /// </summary>
        public static float Alpha
        {
            get
            {
                return Console.alpha;
            }
            
            set
            {
                Console.alpha = value;
                Console.rectangle.Color = Console.backgroundColor * value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color"/> which the output lines are faded to.
        /// </summary>
        public static Color FadeColor { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SpriteFont"/> used to draw standard output <see cref="string"/>s.
        /// </summary>
        public static SpriteFont OutputFont
        {
            get
            {
                return Console.outputFont;
            }
            set
            {
                Console.outputFont = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="SpriteFont"/> used to draw error output <see cref="string"/>s.
        /// </summary>
        public static SpriteFont ErrorFont
        {
            get
            {
                return Console.errorFont;
            }
            set
            {
                Console.errorFont = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="SpriteFont"/> used to draw standard input <see cref="string"/>s.
        /// </summary>
        public static SpriteFont InputFont
        {
            get
            {
                return Console.inputFont;
            }
            set
            {
                Console.inputFont = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color"/> used to draw standard output <see cref="string"/>s.
        /// </summary>
        public static Color OutputColor
        {
            get
            {
                return Console.outputColor;
            }

            set
            {
                Console.outputColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color"/> used to draw error output <see cref="string"/>s.
        /// </summary>
        public static Color ErrorColor
        {
            get
            {
                return Console.errorColor;
            }

            set
            {
                Console.errorColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color"/> used to draw standard input <see cref="string"/>s.
        /// </summary>
        public static Color InputColor
        {
            get
            {
                return Console.inputColor;
            }

            set
            {
                Console.inputColor = value;
            }
        }

        public static ConsoleState State { get; set; }

        /// <summary>
        /// Initializes the <see cref="Console"/>. 
        /// Sets up the link to <see cref="System.Console"/>, the <see cref="Console"/> will be visual when <see cref="Update(GameTime)"/> and <see cref="Draw(SpriteBatch)"/> are called.
        /// </summary>
        /// <param name="game">
        /// A <see cref="Game"/> this <see cref="Console"/> should be bound to.
        /// </param>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> used to draw every <see cref="string"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="game"/> and/or <paramref name="font"/> is/are NULL.
        /// </exception>
        public static void Initialize(Game game, SpriteFont font, int lineCount = 10)
        {
            if (game == null)
            {
                throw new ArgumentNullException("game");
            }

            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            if (lineCount < 0)
            {
                throw new ArgumentOutOfRangeException("lineCount", lineCount, "lineCount must be greater than or equals 0.");
            }

            Console.lineCount = lineCount;
            InitializeMembers(game, font);
        }

        /// <summary>
        /// Initializes the <see cref="Console"/> with a custom <see cref="textScale"/>. 
        /// Sets up the link to <see cref="System.Console"/>, the <see cref="Console"/> will be visual when <see cref="Update(GameTime)"/> and <see cref="Draw(SpriteBatch)"/> are called.
        /// </summary>
        /// <param name="game">
        /// A <see cref="Game"/> this <see cref="Console"/> should be bound to.
        /// </param>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> used to draw every <see cref="string"/>.
        /// </param>
        /// <param name="textScale">
        /// A value indicating the scale of the drawn <see cref="string"/>s.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="textScale"/> must be within 0f and 1f.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="game"/> and/or <paramref name="font"/> is/are NULL.
        /// </exception>
        public static void Initialize(Game game, SpriteFont font, float textScale)
        {
            if (game == null)
            {
                throw new ArgumentNullException("game");
            }

            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            if (textScale < 0f || textScale > 1f)
            {
                throw new ArgumentOutOfRangeException("textScale", textScale, "textScale must be within 0f and 1f.");
            }

            InitializeMembers(game, font, textScale);
        }

        /// <summary>
        /// Initializes the <see cref="Console"/> with different <see cref="SpriteFont"/>s.
        /// Sets up the link to <see cref="System.Console"/>, the <see cref="Console"/> will be visual when <see cref="Update(GameTime)"/> and <see cref="Draw(SpriteBatch)"/> are called.
        /// </summary>
        /// <param name="game">
        /// A <see cref="Game"/> this <see cref="Console"/> should be bound to.
        /// </param>
        /// <param name="outputFont">
        /// A <see cref="SpriteFont"/> used to draw standard output <see cref="string"/>s.
        /// </param>
        /// <param name="errorFont">
        /// A <see cref="SpriteFont"/> used to draw error output <see cref="string"/>s.
        /// </param>
        /// <param name="inputFont">
        /// A <see cref="SpriteFont"/> used to draw standard input <see cref="string"/>s.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// One or more of the parameters is/are NULL.
        /// </exception>
        public static void Initialize(Game game, SpriteFont outputFont, SpriteFont errorFont, SpriteFont inputFont)
        {
            if (game == null)
            {
                throw new ArgumentNullException("game");
            }

            if (outputFont == null)
            {
                throw new ArgumentNullException("outputFont");
            }

            if (errorFont == null)
            {
                throw new ArgumentNullException("outputFont");
            }

            if (inputFont == null)
            {
                throw new ArgumentNullException("outputFont");
            }

            InitializeMembers(game, null, outputFont: outputFont, errorFont: errorFont, inputFont: inputFont);
        }

        /// <summary>
        /// Initializes the <see cref="Console"/> with different <see cref="SpriteFont"/>s.
        /// Sets up the link to <see cref="System.Console"/>, the <see cref="Console"/> will be visual when <see cref="Update(GameTime)"/> and <see cref="Draw(SpriteBatch)"/> are called.
        /// </summary>
        /// <param name="game">
        /// A <see cref="Game"/> this <see cref="Console"/> should be bound to.
        /// </param>
        /// <param name="outputFont">
        /// A <see cref="SpriteFont"/> used to draw standard output <see cref="string"/>s.
        /// </param>
        /// <param name="errorFont">
        /// A <see cref="SpriteFont"/> used to draw error output <see cref="string"/>s.
        /// </param>
        /// <param name="inputFont">
        /// A <see cref="SpriteFont"/> used to draw standard input <see cref="string"/>s.
        /// </param>
        /// <param name="textScale">
        /// A value indicating the scale of the drawn <see cref="string"/>s.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// One or more of the parameters is/are NULL.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="textScale"/> must be within 0f and 1f.
        /// </exception>
        public static void Initialize(Game game, SpriteFont outputFont, SpriteFont errorFont, SpriteFont inputFont, float textScale)
        {
            if (game == null)
            {
                throw new ArgumentNullException("game");
            }

            if (outputFont == null)
            {
                throw new ArgumentNullException("outputFont");
            }

            if (errorFont == null)
            {
                throw new ArgumentNullException("outputFont");
            }

            if (inputFont == null)
            {
                throw new ArgumentNullException("outputFont");
            }

            if (textScale < 0f || textScale > 1f)
            {
                throw new ArgumentOutOfRangeException("textScale", textScale, "textScale must be within 0f and 1f.");
            }

            InitializeMembers(game, null, outputFont: outputFont, errorFont: errorFont, inputFont: inputFont);
        }

        /// <summary>
        /// Initializes the <see cref="Console"/> with custom <see cref="Color"/>s.
        /// Sets up the link to <see cref="System.Console"/>, the <see cref="Console"/> will be visual when <see cref="Update(GameTime)"/> and <see cref="Draw(SpriteBatch)"/> are called.
        /// </summary>
        /// <param name="game">
        /// A <see cref="Game"/> this <see cref="Console"/> should be bound to.
        /// </param>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> used to draw standard output <see cref="string"/>s.
        /// </param>
        /// <param name="backgroundColor">
        /// A <see cref="Color"/> used to draw the background.
        /// </param>
        /// <param name="outputColor">
        /// A <see cref="Color"/> used to draw standard input <see cref="string"/>s.
        /// </param>
        /// <param name="errorColor">
        /// A <see cref="Color"/> used to draw standard input <see cref="string"/>s.
        /// </param>
        /// <param name="inputColor">
        /// A <see cref="Color"/> used to draw standard input <see cref="string"/>s.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// One or more of the parameters is/are NULL.
        /// </exception>
        public static void Initialize(Game game, SpriteFont font, Color backgroundColor, Color outputColor, Color errorColor, Color inputColor)
        {
            if (game == null)
            {
                throw new ArgumentNullException("game");
            }

            if (backgroundColor == null)
            {
                throw new ArgumentNullException("backgroundColor");
            }

            if (outputColor == null)
            {
                throw new ArgumentNullException("outputColor");
            }

            if (errorColor == null)
            {
                throw new ArgumentNullException("errorColor");
            }

            if (inputColor == null)
            {
                throw new ArgumentNullException("inputColor");
            }

            InitializeMembers(game, null, outputFont: outputFont, errorFont: errorFont, inputFont: inputFont);
        }

        /// <summary>
        /// Initializes the <see cref="Console"/> with custom values.
        /// Sets up the link to <see cref="System.Console"/>, the <see cref="Console"/> will be visual when <see cref="Update(GameTime)"/> and <see cref="Draw(SpriteBatch)"/> are called.
        /// </summary>
        /// <param name="game">
        /// A <see cref="Game"/> this <see cref="Console"/> should be bound to.
        /// </param>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> used to draw standard output <see cref="string"/>s.
        /// </param>
        /// <param name="textScale">
        /// A value indicating the scale of the drawn <see cref="string"/>s.
        /// </param>
        /// <param name="position">
        /// A <see cref="Rectangle"/> indicating the position of the <see cref="Console"/>.
        /// </param>
        /// <param name="backgroundColor">
        /// A <see cref="Color"/> used to draw the background.
        /// </param>
        /// <param name="alpha">
        /// A value indicating the alpha value of the <see cref="Console"/>.
        /// </param>
        /// <param name="outputColor">
        /// A <see cref="Color"/> used to draw standard input <see cref="string"/>s.
        /// </param>
        /// <param name="errorColor">
        /// A <see cref="Color"/> used to draw standard input <see cref="string"/>s.
        /// </param>
        /// <param name="inputColor">
        /// A <see cref="Color"/> used to draw standard input <see cref="string"/>s.
        /// </param>
        /// <param name="outputFont">
        /// A <see cref="SpriteFont"/> used to draw standard output <see cref="string"/>s.
        /// </param>
        /// <param name="errorFont">
        /// A <see cref="SpriteFont"/> used to draw error output <see cref="string"/>s.
        /// </param>
        /// <param name="inputFont">
        /// A <see cref="SpriteFont"/> used to draw standard input <see cref="string"/>s.
        /// </param>
        /// <param name="fadeColor">
        /// A <see cref="Color"/> to be used to fade the output <see cref="string"/>s.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// One or more of the parameters is/are NULL.
        /// </exception>
        public static void Initialize(
            Game game,
            SpriteFont font,
            float textScale,
            Rectangle position,
            Color backgroundColor,
            float alpha,
            Color outputColor,
            Color errorColor,
            Color inputColor,
            SpriteFont outputFont,
            SpriteFont errorFont,
            SpriteFont inputFont,
            Color fadeColor)
        {
            if (game == null)
            {
                throw new ArgumentNullException("game");
            }

            if (position == null)
            {
                throw new ArgumentNullException("position");
            }

            if (backgroundColor == null)
            {
                throw new ArgumentNullException("backgroundColor");
            }

            if (outputColor == null)
            {
                throw new ArgumentNullException("outputColor");
            }

            if (errorColor == null)
            {
                throw new ArgumentNullException("errorColor");
            }

            if (inputColor == null)
            {
                throw new ArgumentNullException("inputColor");
            }

            if (fadeColor == null)
            {
                throw new ArgumentNullException("fadeColor");
            }

            if (textScale < 0f)
            {
                throw new ArgumentOutOfRangeException("textScale", textScale, "textScale must be greater than or equals 0f.");
            }

            if (alpha < 0f || alpha > 1f)
            {
                throw new ArgumentOutOfRangeException("alpha", alpha, "alpha must be within 0f and 1f.");
            }

            InitializeMembers(game, null, outputFont: outputFont, errorFont: errorFont, inputFont: inputFont);
        }

        /// <summary>
        /// Updates the <see cref="Console"/>.
        /// </summary>
        /// <param name="gameTime">
        /// The currently elapsed <see cref="GameTime"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="gameTime"/> was passed as NULL.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Console.Initialize(Game, SpriteFont)"/> was not called.
        /// </exception>
        public static void Update(GameTime gameTime)
        {
            if (Console.outputStrings == null)
            {
                throw new InvalidOperationException("Console is not initialized. Call Console.Initialize(Game, SpriteFont) to initialize the members of the Console.");
            }

            if (gameTime == null)
            {
                throw new ArgumentNullException("gameTime");
            }

            if (Console.state == ConsoleState.Reading)
            {
                if (inputQuitted)
                {
                    Console.WriteLine(Console.input, OutputType.UserInput);
                    Console.state = ConsoleState.Nothing;
                }
            }

            Console.UpdateOutput(gameTime);

            if (Console.state == ConsoleState.Writing)
            {
                Console.state = ConsoleState.Nothing;
            }
        }

        /// <summary>
        /// Draws the content of the <see cref="Console"/>.
        /// </summary>
        /// <param name="spriteBatch">
        /// A <see cref="SpriteBatch"/> used to draw the content.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Console.Initialize(Game, SpriteFont)"/> was not called.
        /// </exception>
        public static void Draw(SpriteBatch spriteBatch)
        {
            if (Console.renderOutput == null)
            {
                throw new InvalidOperationException("Console is not initialized. Call Console.Initialize(Game, SpriteFont) to initialize the members of the Console.");
            }

            rectangle.Render(spriteBatch);
            foreach (var text in renderOutput)
            {
                text.Render(spriteBatch);
            }
        }

        /// <summary>
        /// Displays the given <see cref="string"/> associated to the given <see cref="OutputType"/> in the current line.
        /// </summary>
        /// <param name="output">
        /// A <see cref="string"/> to be displayed.
        /// </param>
        /// <param name="outputType">
        /// An <see cref="OutputType"/> indicating the way the <paramref name="output"/> should be displayed. If the <paramref name="outputType"/> differs from the <see cref="OutputType"/> of the current
        /// line, <see cref="WriteLine(string, OutputType)"/> is called.
        /// </param>
        internal static void Write(string output, OutputType outputType)
        {
            if ((Console.outputStrings.Count == 0 || Console.outputTypes.Count == 0) || !outputType.Equals(Console.outputTypes.Last<OutputType>()))
            {
                Console.WriteLine(output, outputType);
            }
            else
            {
                string newOutputLine = Console.outputStrings.Last<string>() + output;
                Console.outputStrings[Console.outputStrings.Count - 1] = newOutputLine;
            }
        }

        /// <summary>
        /// Gets the input of the <see cref="Console"/>.
        /// </summary>
        /// <returns>
        /// The input of the <see cref="Console"/> as <see cref="string"/> or NULL, if the user has not yet quit the input.
        /// </returns>
        internal static Task<string> GetInput()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Displays the given <see cref="string"/> associated to the given <see cref="OutputType"/> in the next line.
        /// </summary>
        /// <param name="output">
        /// A <see cref="string"/> to be displayed.
        /// </param>
        /// <param name="outputType">
        /// An <see cref="OutputType"/> indicating the way the <paramref name="output"/> should be displayed.
        /// </param>
        internal static void WriteLine(string output, OutputType outputType)
        {
            Console.outputStrings.Add(output);
            Console.outputTypes.Add(outputType);
        }

        /// <summary>
        /// Starts the process of reading the next line of user input.
        /// </summary>
        internal static void ReadLine()
        {
            Console.state = ConsoleState.Reading;
        }

        /// <summary>
        /// Updates the output.
        /// </summary>
        /// <param name="gameTime">
        /// The currently elapsed <see cref="GameTime"/>.
        /// </param>
        private static void UpdateOutput(GameTime gameTime)
        {
            if (Console.outputStrings.Count > Console.lineCount)
            {
                List<string> stringsToRemove = new List<string>();
                for (int index = 0; index < Console.outputStrings.Count - Console.lineCount; index++)
                {
                    stringsToRemove.Add(Console.outputStrings[index]);
                }

                foreach (string toRemove in stringsToRemove)
                {
                    Console.outputTypes.Remove(Console.outputTypes[Console.outputStrings.IndexOf(toRemove)]);
                    Console.outputStrings.Remove(toRemove);
                }
            }

            for (int line = 0; line < Console.outputStrings.Count; line++)
            {
                Color drawColor = Console.outputColor;
                SpriteFont drawFont = Console.outputFont;
                string drawString = (Console.state == ConsoleState.Reading && line == Console.outputStrings.Count) ?
                    (gameTime.TotalGameTime.Seconds % 2 == 0 ? Console.input + "|" : Console.input + " ") :
                    Console.outputStrings[line];
                switch (Console.outputTypes[line])
                {
                    case OutputType.Error:
                        drawColor = Console.errorColor;
                        drawFont = Console.errorFont;
                        break;
                    case OutputType.UserInput:
                        drawColor = Console.inputColor;
                        drawFont = Console.inputFont;
                        break;
                }

                foreach (char character in drawString)
                {
                    if (!drawFont.Characters.Contains(character) && character != '\r' && character != '\n')
                    {
                        drawString.Replace(character, ' ');
                    }
                }

                Console.renderOutput[line] = new RenderText(
                    new Vector2(Console.Position.X, Console.Position.Y + (line * 20F)),
                    drawString,
                    Color.Lerp(drawColor, Console.FadeColor, line == 0 ? (float)(Console.outputStrings.Count - 1) / Console.outputStrings.Count : (float)(Console.outputStrings.Count - (line + 1)) / Console.outputStrings.Count),
                    drawFont,
                    Console.textScale);
            }
        }

        /// <summary>
        /// Initializes the members of the <see cref="Console"/>
        /// </summary>
        /// <param name="game">
        /// A <see cref="Game"/> this <see cref="Console"/> should be bound to.
        /// </param>
        /// <param name="font">
        /// A <see cref="SpriteFont"/> used to draw standard output <see cref="string"/>s.
        /// </param>
        /// <param name="textScale">
        /// A value indicating the scale of the drawn <see cref="string"/>s.
        /// </param>
        /// <param name="width">
        /// A value indicating the width to be subtracted of the <see cref="Game"/> width.
        /// </param>
        /// <param name="height">
        /// A value indicating the height to be subtracted of the <see cref="Game"/> height.
        /// </param>
        /// <param name="position">
        /// A <see cref="Rectangle"/> indicating the position of the <see cref="Console"/>.
        /// </param>
        /// <param name="backgroundColor">
        /// A <see cref="Color"/> used to draw the background.
        /// </param>
        /// <param name="alpha">
        /// A value indicating the alpha value of the <see cref="Console"/>.
        /// </param>
        /// <param name="outputColor">
        /// A <see cref="Color"/> used to draw standard input <see cref="string"/>s.
        /// </param>
        /// <param name="errorColor">
        /// A <see cref="Color"/> used to draw standard input <see cref="string"/>s.
        /// </param>
        /// <param name="inputColor">
        /// A <see cref="Color"/> used to draw standard input <see cref="string"/>s.
        /// </param>
        /// <param name="outputFont">
        /// A <see cref="SpriteFont"/> used to draw standard output <see cref="string"/>s.
        /// </param>
        /// <param name="errorFont">
        /// A <see cref="SpriteFont"/> used to draw error output <see cref="string"/>s.
        /// </param>
        /// <param name="inputFont">
        /// A <see cref="SpriteFont"/> used to draw standard input <see cref="string"/>s.
        /// </param>
        /// <param name="fadeColor">
        /// A <see cref="Color"/> to be used to fade the output <see cref="string"/>s.
        /// </param>
        private static void InitializeMembers(
            Game game,
            SpriteFont font,
            float textScale = 0.5f,
            int width = -1,
            int height = -1,
            Rectangle position = new Rectangle(),
            Color backgroundColor = new Color(),
            float alpha = 0.5f,
            Color outputColor = new Color(),
            Color errorColor = new Color(),
            Color inputColor = new Color(),
            SpriteFont outputFont = null,
            SpriteFont errorFont = null,
            SpriteFont inputFont = null,
            Color fadeColor = new Color())
        {
            Console.outputWriter = new OutputWriter(OutputType.Info);
            Console.errorWriter = new OutputWriter(OutputType.Error);
            Console.inputReader = new InputReader();
            Console.parent = game;
            Console.textScale = textScale;
            Console.alpha = alpha;
            Console.outputStrings = new List<string>();
            Console.outputTypes = new List<OutputType>();
            Console.renderOutput = new List<RenderText>();
            Console.state = ConsoleState.Nothing;
            Console.inputQuitted = false;
            if (backgroundColor.Equals(new Color()))
            {
                Console.backgroundColor = Color.Black;
            }
            else
            {
                Console.backgroundColor = backgroundColor;
            }

            Console.rectangle = new RenderRectangle(new Rectangle(0, 0, 0, 0), Console.backgroundColor * Console.alpha, Console.parent.GraphicsDevice);
            if (outputColor.Equals(new Color()))
            {
                Console.outputColor = Color.White;
            }
            else
            {
                Console.outputColor = outputColor;
            }

            if (errorColor.Equals(new Color()))
            {
                Console.errorColor = Color.Red;
            }
            else
            {
                Console.errorColor = errorColor;
            }

            if (inputColor.Equals(new Color()))
            {
                Console.inputColor = Color.Cyan;
            }
            else
            {
                Console.inputColor = inputColor;
            }

            if (fadeColor.Equals(new Color()))
            {
                Console.FadeColor = Color.Transparent;
            }
            else
            {
                Console.FadeColor = fadeColor;
            }

            if (width < 0)
            {
                width = Console.parent.GraphicsDevice.Viewport.Width / 3;
            }

            if (height < 0)
            {
                height = (int)((Console.lineCount) * (font.LineSpacing * Console.textScale));
            }

            if (position.Equals(new Rectangle()))
            {
                Console.Position = new Rectangle(Console.parent.GraphicsDevice.Viewport.Width - width, Console.parent.GraphicsDevice.Viewport.Height - height, width, height);
            }

            if (outputFont == null)
            {
                Console.outputFont = font;
            }
            else
            {
                Console.outputFont = outputFont;
            }

            if (errorFont == null)
            {
                Console.errorFont = font;
            }
            else
            {
                Console.errorFont = errorFont;
            }

            if (inputFont == null)
            {
                Console.inputFont = font;
            }

            else
            {
                Console.inputFont = inputFont;
            }

            for (int i = 0; i < Console.lineCount; i++)
            {
                Console.renderOutput.Add(new RenderText(new Vector2(), " ", Console.outputColor, Console.outputFont, Console.textScale));
            }

            System.Console.SetOut(Console.outputWriter);
            System.Console.SetError(Console.errorWriter);
            System.Console.SetIn(Console.inputReader);
        }

        public bool Focused
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        public void RecieveTextInput(char inputChar)
        {
            Console.input += inputChar.ToString();
        }

        public void RecieveTextInput(string text)
        {
            Console.input += text;
            Console.WriteLine(input, OutputType.UserInput);
        }

        public void RecieveCommandInput(char command)
        {
            switch (command)
            {
                case '\b':
                    if (input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                    }

                    break;
                default:
                    break;
            }
        }

        public void RecieveSpecialInput(Keys key)
        {
            if (key == Keys.Enter)
            {
                Console.inputQuitted = true;
            }
        }
    }
}
