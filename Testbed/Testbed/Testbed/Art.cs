using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Testbed
{
    static class Art
    {
        public static Texture2D Pixel;

        static Art()
        {
            
        }

        public static void LoadContent(ContentManager content, Game game)
        {
            Color[] data = new Color[1];
            data[0] = Color.White;

            Pixel = new Texture2D(game.GraphicsDevice, 1, 1);
            Pixel.SetData<Color>(data);
        }
    }
}
