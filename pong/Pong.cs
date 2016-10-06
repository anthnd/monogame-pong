using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Pong : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private static KeyboardState oldState;

        private static int winWidth = 800;
        private static int winHeight = 600;

        private Paddle playerPaddle;
        private Paddle computerPaddle;
        private Ball ball;

        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            playerPaddle = new Paddle(new Vector2(0, (winHeight-Paddle.height)/2));
            computerPaddle = new Paddle(new Vector2(winWidth - Paddle.width, (winHeight - Paddle.height) / 2));
            ball = new Ball(new Vector2((winWidth - Ball.Size) / 2, (winHeight - Ball.Size) / 2));


            graphics.PreferredBackBufferWidth = winWidth;
            graphics.PreferredBackBufferHeight = winHeight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Console.WriteLine("Start!");
            playerPaddle.Initialize(this.GraphicsDevice);
            computerPaddle.Initialize(this.GraphicsDevice);
            ball.Initialize(this.GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState state = Keyboard.GetState();

            playerPaddle.Update(state, this.GraphicsDevice);
            ball.Update(this.GraphicsDevice);

            CheckCollisions();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(30, 30, 30));

            spriteBatch.Begin();
            spriteBatch.Draw(playerPaddle.Texture, playerPaddle.Position);
            spriteBatch.Draw(computerPaddle.Texture, computerPaddle.Position);
            spriteBatch.Draw(ball.Texture, ball.Position);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void CheckCollisions()
        {
            if (ball.Rectangle.Intersects(playerPaddle.Rectangle) || ball.Rectangle.Intersects(computerPaddle.Rectangle))
            {
                Console.WriteLine("Collision!");
            }
        }

        public static KeyboardState OldState
        {
            get
            {
                return oldState;
            }

            set
            {
                oldState = value;
            }
        }

        public static int WinWidth
        {
            get
            {
                return winWidth;
            }

            set
            {
                winWidth = value;
            }
        }

        public static int WinHeight
        {
            get
            {
                return winHeight;
            }

            set
            {
                winHeight = value;
            }
        }
    }
}
