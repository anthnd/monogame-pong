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

        public static int paddleHeight = 90;
        public static int paddleWidth = 10;
        private int speed = 6;
        

        /// <summary>
        /// Make a new paddle
        /// </summary>
        /// <param name="initialPos">Requires a coordinate pair (X,Y)</param>
        public Paddle(Vector2 initialPos) {
            position = initialPos;
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

        public void Initialize(GraphicsDevice gd)
        {
            texture = new Texture2D(gd, Paddle.paddleWidth, Paddle.paddleHeight);
            Color[] colorData = new Color[Paddle.paddleWidth * Paddle.paddleHeight];
            for (int i = 0; i < (Paddle.paddleWidth * Paddle.paddleHeight); i++)
                colorData[i] = Color.White;

            texture.SetData<Color>(colorData);
        }

        public void Update(KeyboardState state, GraphicsDevice gd) {
            if (state.IsKeyDown(Keys.Down))
            {
                if (position.Y < (gd.Viewport.Height-paddleHeight))
                    position.Y += speed;
            }
            else if (state.IsKeyDown(Keys.Up))
            {
                if (position.Y > 0)
                    position.Y -= speed;
            }
            Pong.OldState = state;
        }
    }
}
