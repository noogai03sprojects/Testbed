using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Testbed
{
    class _1v1Test : Test
    {
        Texture2D _1v1;

        public override void LoadContent()
        {
            _1v1 = Content.Load<Texture2D>("1v1");
            base.LoadContent();
        }

        public override void Draw(SpriteBatch spriteBatch, PrimitiveBatch primBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_1v1, new Vector2(10, 10), Color.White);

            spriteBatch.End();
            base.Draw(spriteBatch, primBatch);
        }

        public static Test Create()
        {
            _1v1Test test = new _1v1Test();
            test.Name = "1v1 me irl";
            test.Description = "nope";
            test.ContentPath = "BasicTest";

            return test;
        }
    }
}
