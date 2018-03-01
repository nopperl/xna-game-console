namespace GameConsole
{
    using GameConsole.Input;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// A <see cref="Game"/> solely existing to test the <see cref="Console"/>.
    /// </summary>
    public class TestGame : Game
    {
        /// <summary>
        /// The <see cref="GraphicsDeviceManager"/> used to set up graphics.
        /// </summary>
        private GraphicsDeviceManager graphics;

        /// <summary>
        /// The <see cref="SpriteBatch"/> used to draw.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// A standard <see cref="SpriteFont"/> used to test the <see cref="Console"/>.
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestGame"/> class.
        /// </summary>
        public TestGame()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            ////graphics.ToggleFullScreen();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.font = this.Content.Load<SpriteFont>("Courier New");
            Console.Initialize(this, font);
            System.Console.WriteLine("Hello world!");
            System.Console.Error.WriteLine("Hello ");
            System.Console.Error.Write("error!");
            System.Console.Out.Write("Press keys!");
            System.Console.ReadLine();
            KeyboardDispatcher keyboardDispatcher = new KeyboardDispatcher(this.Window);
            keyboardDispatcher.Receiver = new Console();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.
        /// </param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            Console.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.
        /// </param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();
            Console.Draw(this.spriteBatch);
            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
