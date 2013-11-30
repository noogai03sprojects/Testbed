using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Testbed
{
    class PixelZoom : Test
    {
        public static Test Create()
        {
            PixelZoom test = new PixelZoom();
            test.Name = "Pixel Zoom";
            test.Description = "Blocky pixel zooming with shaders";
            test.ContentPath = "PixelZoom";

            return test;
        }

        //Effect effect;
        Texture2D balrog;
        RenderTarget2D target;

        public override void LoadContent()
        {
            //effect = Content.Load<Effect>("PixelZoom");
            balrog = Content.Load<Texture2D>("BalrogStandingSmile");
            target = new RenderTarget2D(Device, Device.Viewport.Width/2, Device.Viewport.Height/2);

            base.LoadContent();
        }

        public override void Draw(SpriteBatch spriteBatch, PrimitiveBatch primBatch)
        {
            Device.SetRenderTarget(target);
            spriteBatch.Begin();

            spriteBatch.Draw(balrog, new Vector2(10, 10), null, Color.White, 0, Vector2.Zero, 1, 0, 0);
            spriteBatch.Draw(balrog, new Vector2(10, 80), null, Color.White, 0, Vector2.Zero, 1, 0, 0);

            spriteBatch.End();

            Device.SetRenderTarget(null);
            spriteBatch.Begin(0, null, SamplerState.PointClamp, null, null);

            spriteBatch.Draw(target, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 2, 0, 0);

            spriteBatch.End();

            base.Draw(spriteBatch, primBatch);
        }


    }
}
