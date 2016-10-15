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
        // Default
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // GUI settings
        private static int winWidth = 800;
        private static int winHeight = 600;

        // Initializing objects and keyboard reader
        GameObject topWall;
        GameObject bottomWall;
        GameObject playerOne;
        GameObject playerTwo;
        GameObject ball;
        KeyboardState keyboardState;

        // Gane variables
        int wallThickness = 20;
        int paddleHeight = 100;
        int paddleThickness = 10;
        float paddleSpeed = 7.5f;
        int ballSize = 15;
        float maxBounceAngle = (float)(60 * (Math.PI / 180));
        int speedComponent = 6;
        int counter = 0;



        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

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

            Texture2D generalTexture = new Texture2D(this.GraphicsDevice, 1, 1);
            generalTexture.SetData(new[] { Color.White });

            topWall = new GameObject
                (
                    generalTexture,
                    Vector2.Zero,
                    GraphicsDevice.Viewport.Width,
                    wallThickness
                );
            bottomWall = new GameObject
                (
                    generalTexture,
                    new Vector2
                    (
                        0,
                        GraphicsDevice.Viewport.Height - wallThickness
                    ),
                    GraphicsDevice.Viewport.Width,
                    wallThickness
                );
            playerOne = new GameObject
                (
                    generalTexture,
                    new Vector2
                    (
                        0,
                        (GraphicsDevice.Viewport.Height - paddleHeight) / 2
                    ),
                    paddleThickness,
                    paddleHeight
                );
            playerTwo = new GameObject
                (
                    generalTexture,
                    new Vector2
                    (
                        GraphicsDevice.Viewport.Width - paddleThickness,
                        (GraphicsDevice.Viewport.Height - paddleHeight) / 2
                    ),
                    paddleThickness,
                    paddleHeight
                );
            ball = new GameObject
                (
                    generalTexture,
                    new Vector2
                    (
                        playerOne.Rectangle.Right + 5,
                        (GraphicsDevice.Viewport.Height - ballSize) / 2
                    ),
                    new Vector2((float)speedComponent, (float)-speedComponent),
                    ballSize,
                    ballSize
                );

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

            ball.Position += ball.Velocity;

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
                playerOne.Position.Y -= paddleSpeed;

            if (keyboardState.IsKeyDown(Keys.S))
                playerOne.Position.Y += paddleSpeed;

            if (keyboardState.IsKeyDown(Keys.Up))
                playerTwo.Position.Y -= paddleSpeed;

            if (keyboardState.IsKeyDown(Keys.Down))
                playerTwo.Position.Y += paddleSpeed;

            playerTwo.Position.Y = ball.Position.Y;

            CheckPaddleWallCollision();
            CheckBallCollision();

            if (counter % 60 == 0)
                Console.WriteLine(ball.Velocity);
            counter++;

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
            spriteBatch.Draw(topWall.Texture, topWall.Rectangle, Color.LimeGreen);
            spriteBatch.Draw(bottomWall.Texture, bottomWall.Rectangle, Color.LimeGreen);
            spriteBatch.Draw(playerOne.Texture, playerOne.Rectangle, new Color(32, 148, 250));
            spriteBatch.Draw(playerTwo.Texture, playerTwo.Rectangle, new Color(255, 59, 48));
            spriteBatch.Draw(ball.Texture, ball.Rectangle, new Color(255, 230, 31));
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Checks for ball collision with walls, paddle and out of boundary
        /// </summary>
        private void CheckBallCollision()
        {
            if (ball.Rectangle.Intersects(topWall.Rectangle) || ball.Rectangle.Intersects(bottomWall.Rectangle))
                ball.Velocity.Y *= -1;

            if (ball.Rectangle.Intersects(playerOne.Rectangle))
            {
                var relativeIntersectY = (playerOne.Rectangle.Center.Y) - ball.Rectangle.Center.Y;
                var normalizedRelativeIntersectionY = (double)relativeIntersectY / (paddleHeight / 2);
                var bounceAngle = normalizedRelativeIntersectionY * maxBounceAngle;
                ball.Velocity.X = Speed * (float)Math.Cos(bounceAngle);
                ball.Velocity.Y = Speed * (float)-Math.Sin(bounceAngle);
            }
            
            if (ball.Rectangle.Intersects(playerTwo.Rectangle))
            {
                var relativeIntersectY = (playerTwo.Rectangle.Center.Y) - ball.Rectangle.Center.Y;
                var normalizedRelativeIntersectionY = (double)relativeIntersectY / (paddleHeight / 2);
                var bounceAngle = normalizedRelativeIntersectionY * maxBounceAngle;
                ball.Velocity.X = Speed * (float)-Math.Cos(bounceAngle);
                ball.Velocity.Y = Speed * (float)-Math.Sin(bounceAngle);
            }

            if (ball.Position.X < -ball.Rectangle.Width || ball.Position.X > GraphicsDevice.Viewport.Width)
                SetInStartPosition();
        }

        /// <summary>
        /// Reset ball and paddles
        /// </summary>
        private void SetInStartPosition()
        {
            playerOne.Position.Y = (GraphicsDevice.Viewport.Height - paddleHeight) / 2;

            playerTwo.Position.Y = (GraphicsDevice.Viewport.Height - paddleHeight) / 2;

            ball.Position = new Vector2(playerOne.Rectangle.Right + 5, (GraphicsDevice.Viewport.Height - ballSize) / 2);
            ball.Velocity = new Vector2((float)speedComponent, (float)-speedComponent);
        }

        /// <summary>
        /// Restrict paddle from going past wall
        /// </summary>
        private void CheckPaddleWallCollision()
        {
            if (playerOne.Rectangle.Intersects(topWall.Rectangle))
                playerOne.Position.Y = topWall.Rectangle.Bottom;

            if (playerOne.Rectangle.Intersects(bottomWall.Rectangle))
                playerOne.Position.Y = bottomWall.BoundingBox.Y - playerOne.Height;

            if (playerTwo.Rectangle.Intersects(topWall.Rectangle))
                playerTwo.Position.Y = topWall.Rectangle.Bottom;

            if (playerTwo.Rectangle.Intersects(bottomWall.Rectangle))
                playerTwo.Position.Y = bottomWall.BoundingBox.Y - playerTwo.Height;

        }

        private float Speed
        {
            get
            {
                return (float) Math.Sqrt(2 * speedComponent * speedComponent);
            }
        }

    }
}
