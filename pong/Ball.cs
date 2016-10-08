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

        private float maxBounceAngle = (float) (75 * (Math.PI/180));
        public static int size = 22;
        private int speed = 4;

        /// <summary>
        /// Make a new Ball Object
        /// </summary>
        /// <param name="initialPos">Initial ball position in coordinate pair form (x,y)</param>
        public Ball(Vector2 initialPos)
        {
            Position = initialPos;
            rectangle = new Rectangle((int)initialPos.X, (int)initialPos.Y, size, size);

            // Find a random direction for the ball to start going in and set the velocity accordingly
            Random rand = new Random();
            double angle = rand.NextDouble();
            float rad = MathHelper.Lerp(0, (float)Math.PI * 2, (float)angle);
            velocity = new Vector2((float) (speed*Math.Cos(rad)), (float) (speed*Math.Sin(rad)));
        }

        /// <summary>
        /// Colors the ball
        /// </summary>
        /// <param name="gd">GraphicsDevice Object</param>
        public void Initialize(GraphicsDevice gd)
        { 
            //texture = new Texture2D(gd, size, size);
            //Color[] colorData = new Color[size * size];
            //for (int i = 0; i < (size * size); i++)
            //    colorData[i] = Color.White;

            //texture.SetData<Color>(colorData);
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
            if (counter % 100 == 0)
            {
               Console.WriteLine("Ball speed: " + Velocity.ToString());
            }
        }

        public void BounceOffPaddle(Paddle pdl)
        {
           
            float ratio = (pdl.Rectangle.Center.Y - rectangle.Center.Y) / Paddle.height;
            Console.WriteLine(pdl.Rectangle.Center.Y + ", " + rectangle.Center.Y);
            float bounceAngle = ratio * maxBounceAngle;
            Console.WriteLine(ratio + ", " + bounceAngle);
            if (velocity.X > 0)
            {
                velocity.X = (float) Math.Cos(Math.PI - bounceAngle);
                velocity.Y = (float)Math.Sin(Math.PI - bounceAngle);
            } else
            {
                velocity.X = (float)Math.Cos(bounceAngle);
                velocity.Y = (float)Math.Sin(bounceAngle);
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

        public void printLocation()
        {
            Console.WriteLine(rectangle.Location);
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
