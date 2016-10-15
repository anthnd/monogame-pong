using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace pong
{
    class GameObject
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public int Width;
        public int Height;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    Width,
                    Height
                );
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    Texture.Width,
                    Texture.Height
                );
            }
        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
        }

        public GameObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this.Texture = texture;
            this.Position = position;
            this.Velocity = velocity;
        }

        public GameObject(Texture2D texture, Vector2 position, int width, int height)
        {
            this.Texture = texture;
            this.Position = position;
            this.Width = width;
            this.Height = height;
        }

        public GameObject(Texture2D texture, Vector2 position, Vector2 velocity, int width, int height)
        {
            this.Texture = texture;
            this.Position = position;
            this.Velocity = velocity;
            this.Width = width;
            this.Height = height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

    }
}
