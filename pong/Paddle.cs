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
    /// A paddle in a Pong game
    /// </summary>
    class Paddle
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle;

        public static int height = 90;
        public static int width = 10;
        private int speed = 6;
        

        /// <summary>
        /// Make a new paddle
        /// </summary>
        /// <param name="initialPos">Requires a coordinate pair (X,Y)</param>
        public Paddle(Vector2 initialPos) {
            position = initialPos;
            rectangle = new Rectangle((int)initialPos.X, (int)initialPos.Y, width, height);
        }

        public void Initialize(GraphicsDevice gd)
        {
            texture = new Texture2D(gd, Paddle.width, Paddle.height);
            Color[] colorData = new Color[Paddle.width * Paddle.height];
            for (int i = 0; i < (Paddle.width * Paddle.height); i++)
                colorData[i] = Color.White;

            texture.SetData<Color>(colorData);
        }

        public void Update(KeyboardState state, GraphicsDevice gd) {
            if (state.IsKeyDown(Keys.Down))
            {
                if (position.Y < (gd.Viewport.Height - height))
                {
                    rectangle.Offset(0, speed);
                    position.Y += speed;
                }
            }
            else if (state.IsKeyDown(Keys.Up))
            {
                if (position.Y > 0)
                {
                    rectangle.Offset(0, speed * -1);
                    position.Y -= speed;
                }  
            }
            Console.WriteLine("Paddle: " + rectangle.Location);
            Pong.OldState = state;
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
    }
}
