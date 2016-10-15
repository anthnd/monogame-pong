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

        /// <summary>
        /// Rectangle for objects without an imported texture
        /// </summary>
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

        /// <summary>
        /// Rectangle for objects with an imported texture
        /// </summary>
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

        /// <summary>
        /// Create a new GameObject with an imported texture
        /// </summary>
        /// <param name="texture">Texture to use for the object. A Texture2D Object</param>
        /// <param name="position">Initial position. A Vector2 Object</param>
        public GameObject(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
        }

        /// <summary>
        /// Create a new GameObject with an imported texture
        /// </summary>
        /// <param name="texture">Texture to use for the object. A Texture2D Object</param>
        /// <param name="position">Initial position. A Vector2 Object</param>
        /// <param name="velocity">Initial velocity. A Vector2 Object</param>
        public GameObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this.Texture = texture;
            this.Position = position;
            this.Velocity = velocity;
        }

        /// <summary>
        /// Create a new GameObject without an imported texture
        /// </summary>
        /// <param name="texture">Texture to use for the object. A Texture2D Object</param>
        /// <param name="position">Initial position. A Vector2 Object</param>
        /// <param name="width">Width of the new GameObject</param>
        /// <param name="height">Height of the new GameObject</param>
        public GameObject(Texture2D texture, Vector2 position, int width, int height)
        {
            this.Texture = texture;
            this.Position = position;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Create a new GameObject without an imported texture
        /// </summary>
        /// <param name="texture">Texture to use for the object. A Texture2D Object</param>
        /// <param name="position">Initial position. A Vector2 Object</param>
        /// <param name="velocity">Initial velocity. A Vector2 Object</param>
        /// <param name="width">Width of the new GameObject</param>
        /// <param name="height">Height of the new GameObject</param>
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
