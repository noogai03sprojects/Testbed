using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Testbed.Physics;

namespace Testbed
{
    class LineResolution : Test
    {
        public static Test Create()
        {
            LineResolution test = new LineResolution();
            test.Name = "Line resolution";
            test.Description = "Collision handling using lines instead of AABBs";
            test.ContentPath = "LineResolution";
            return test;
        }

        Line testLine;
        Vector2 mousePos;
        float dot;

        AABB AABB;

        public override void Initialize()
        {
            testLine = new Line(400, 100, 100, 400);
            AABB = new AABB(new Vector2I(10, 10), new Vector2I(110, 110));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            mousePos = Input.GetMousePos(Matrix.Identity);
            //Vector2 normal = testLine.StartToEnd.Normal;
            //normal.Normalize();
            //Vector2 distance = (Vector2)testLine.Start - mousePos;
            //distance.Normalize();
            //dot = Vector2.Dot(normal, distance);
            dot = testLine.DotFromPoint(mousePos);

            //Vector2 start
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, PrimitiveBatch primBatch)
        {
            primBatch.DrawLine(testLine, Color.Black);
            //primBatch.DrawLine(mousePos, (Vector2)testLine.Start, Color.Black);
            //primBatch.DrawLine((Vector2)testLine.Start, (Vector2)(testLine.Start) + testLine.StartToEnd.Normal, Color.Black);

            spriteBatch.Begin();

            spriteBatch.DrawString(Font, "Gradient: " + testLine.Gradient 
                + "\nDot: " + dot, new Vector2(10, 400), Color.Black);

            spriteBatch.Draw(Art.Pixel, AABB.ToRectangle(), Color.Red * 0.5f);

            spriteBatch.Draw(Art.Pixel, mousePos, null,  Color.Black, 0, Vector2.Zero, 3, 0, 0);

            spriteBatch.End();
            base.Draw(spriteBatch, primBatch);
        }
    }
}
