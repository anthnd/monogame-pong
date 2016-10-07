using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace pong
{
    /// <summary>
    /// Ball class
    /// </summary>
    class Ball
    {
        private Texture2D texture;
        private Rectangle rectangle;
        private Vector2 position;
        private Vector2 velocity;
        private int counter = 0;

        public static int size = 8;
        private int speed = 4;

        /// <summary>
        /// Make a new Ball Object
        /// </summary>
        /// <param name="initialPos">Initial ball position in coordinate pair form (x,y)</param>
        public Ball(Vector2 initialPos)
        {
            Position = initialPos;
            rectangle.Location = new Point((int)initialPos.X, (int)initialPos.Y);

            // Find a random direction for the ball to start going in and set the velocity accordingly
            Random rand = new Random();
            double angle = rand.NextDouble();
            float rad = MathHelper.Lerp(0, (float)Math.PI /** 2*/, (float)angle);
            velocity = new Vector2((float) (speed*Math.Cos(rad)), (float) (speed*Math.Sin(rad)));
        }

        /// <summary>
        /// Colors the ball
        /// </summary>
        /// <param name="gd">GraphicsDevice Object</param>
        public void Initialize(GraphicsDevice gd)
        { 
            texture = new Texture2D(gd, size, size);
            Color[] colorData = new Color[size * size];
            for (int i = 0; i < (size * size); i++)
                colorData[i] = Color.White;

            texture.SetData<Color>(colorData);
        }

        /// <summary>
        /// Updates the ball's position and direction
        /// </summary>
        /// <param name="gd">GraphicsDevice Object</param>
        public void Update(GraphicsDevice gd)
        {
            position.X += (int)velocity.X;
            position.Y += (int)velocity.Y;
            rectangle.Offset((int)velocity.X, (int)velocity.Y);
            BounceOffTopBottom();
            if (counter % 20 == 0)
            {
                Console.WriteLine("Ball: " + rectangle.Location);
            }
        }

        public void BounceHorizontal()
        {
            velocity.X *= -1;
        }

        public void BounceVertical()
        {
            velocity.Y *= -1;
        }

        /// <summary>
        /// Bounces the ball off the top and bottom window borders
        /// </summary>
        private void BounceOffTopBottom()
        {
            if (position.Y <= 0 || position.Y+size >= Pong.WinHeight)
            {
                BounceVertical();
            }
        }

        public static int Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return texture;
            }

            set
            {
                texture = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }

            set
            {
                rectangle = value;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                velocity = value;
            }
        }
    }
}
