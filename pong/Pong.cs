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
        private static int winWidth = 640;
        private static int winHeight = 480;

        // Initializing objects and keyboard reader
        GameObject topWall, bottomWall, playerOne, playerTwo, ball;
        KeyboardState keyboardState;
        SpriteFont font;

        // Gane variables
        int wallThickness = 20;

        int paddleHeight = 95;
        int paddleThickness = 12;
        float paddleSpeed = 5.5f;

        int ballSize = 17;
        float maxBounceAngle = (float)(60 * (Math.PI / 180));
        float speedComponent = 7.0f;

        int playerOneScore = 0;
        int playerTwoScore = 0;




        /// <summary>
        /// Constructor
        /// </summary>
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
            // Make texture for all GameObjects
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
                        (GraphicsDevice.Viewport.Width - ballSize) / 2,
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
            font = Content.Load<SpriteFont>("Score");
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

            keyboardState = Keyboard.GetState();

            ball.Position += ball.Velocity;

            MovePaddlesOnInput();
            FollowBall();
            CheckPaddleWallCollision();
            CheckBallCollision();

            base.Update(gameTime);
        }

        /// <summary>
        /// Allows playerTwo's paddle to loosely follow the ball
        /// </summary>
        private void FollowBall()
        {
            if (ball.Rectangle.Center.Y > playerTwo.Rectangle.Center.Y)
                playerTwo.Position.Y += paddleSpeed;
            if (ball.Rectangle.Center.Y <= playerTwo.Rectangle.Center.Y)
                playerTwo.Position.Y -= paddleSpeed;
        }

        /// <summary>
        /// Detects keyboard input and moves paddles accordingly
        /// Controls for playerOne are W to go up and S to go down
        /// Controls for playerTwo are Up to go up and Down to go down
        /// </summary>
        private void MovePaddlesOnInput()
        {
            // Player one
            if (keyboardState.IsKeyDown(Keys.W))
                playerOne.Position.Y -= paddleSpeed;
            if (keyboardState.IsKeyDown(Keys.S))
                playerOne.Position.Y += paddleSpeed;

            // Player two
            if (keyboardState.IsKeyDown(Keys.Up))
                playerTwo.Position.Y -= paddleSpeed;
            if (keyboardState.IsKeyDown(Keys.Down))
                playerTwo.Position.Y += paddleSpeed;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(30, 30, 38));

            spriteBatch.Begin();

            // GameObjects
            topWall.Draw(spriteBatch, Color.LimeGreen);
            bottomWall.Draw(spriteBatch, Color.LimeGreen);
            playerOne.Draw(spriteBatch, new Color(32, 148, 250));
            playerTwo.Draw(spriteBatch, new Color(255, 59, 48));
            ball.Draw(spriteBatch, new Color(255, 230, 31));

            // Score
            spriteBatch.DrawString(font, playerOneScore.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 2 - 60, 100), Color.White);
            spriteBatch.DrawString(font, playerTwoScore.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 2 + 20, 100), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Checks for ball collision with walls, paddle and out of boundary
        /// </summary>
        private void CheckBallCollision()
        {
            // Wall bouncing
            if (ball.Rectangle.Intersects(topWall.Rectangle))
            {
                ball.Position.Y = topWall.Rectangle.Bottom;
                ball.Velocity.Y *= -1;
            }
            if (ball.Rectangle.Intersects(bottomWall.Rectangle))
            {
                ball.Position.Y = bottomWall.Rectangle.Y - ballSize;
                ball.Velocity.Y *= -1;
            }
                
            // Bounces the ball at an angle depending on its relative position to the center of the paddle
            // Collisions to the extremities of the paddle means the ball bounces at a sharper angle
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

            // Resets paddles and ball and increments the appropriate score
            if (ball.Position.X < -ball.Rectangle.Width - 100)
            {
                playerTwoScore++;
                SetInStartPosition();
            }
            if (ball.Position.X > GraphicsDevice.Viewport.Width + 100)
            {
                playerOneScore++;
                SetInStartPosition();
            }
                
        }

        /// <summary>
        /// Reset ball and paddles
        /// </summary>
        private void SetInStartPosition()
        {
            // Reset paddles to vertical center
            playerOne.Position.Y = (GraphicsDevice.Viewport.Height - paddleHeight) / 2;
            playerTwo.Position.Y = (GraphicsDevice.Viewport.Height - paddleHeight) / 2;

            // Reset ball to center-center and give initial speed
            ball.Position = new Vector2((GraphicsDevice.Viewport.Width - ballSize) / 2, (GraphicsDevice.Viewport.Height - ballSize) / 2);
            ball.Velocity = new Vector2((float)speedComponent, (float)-speedComponent);
        }

        /// <summary>
        /// Restrict paddle from going past wall
        /// </summary>
        private void CheckPaddleWallCollision()
        {
            // Player one wall collisions
            if (playerOne.Rectangle.Intersects(topWall.Rectangle))
                playerOne.Position.Y = topWall.Rectangle.Bottom;
            if (playerOne.Rectangle.Intersects(bottomWall.Rectangle))
                playerOne.Position.Y = bottomWall.BoundingBox.Y - playerOne.Height;

            // Player two wall collisions
            if (playerTwo.Rectangle.Intersects(topWall.Rectangle))
                playerTwo.Position.Y = topWall.Rectangle.Bottom;
            if (playerTwo.Rectangle.Intersects(bottomWall.Rectangle))
                playerTwo.Position.Y = bottomWall.BoundingBox.Y - playerTwo.Height;

        }

        /// <summary>
        /// Returns the hypotenuse where the legs are both equal to speedComponent
        /// </summary>
        private float Speed
        {
            get
            {
                return (float) Math.Sqrt(2 * speedComponent * speedComponent);
            }
        }

    }
}
