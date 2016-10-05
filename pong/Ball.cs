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
    class Ball
    {
        private Texture2D texture;
        private Vector2 position;

        private int speed;

        public Ball(Vector2 initialPos)
        {
            position = initialPos;
        }
    }
}
