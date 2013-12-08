using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Testbed.Physics;
using Microsoft.Xna.Framework.Input;

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
        Vector2I gravity = new Vector2I(0, 1);
        Vector2I velocity;

        public override void Initialize()
        {
            testLine = new Line(600, 300, 100, 400);
            AABB = new AABB(new Vector2I(10, 10), new Vector2I(110, 110));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            mousePos = Input.GetMousePos(Matrix.Identity);
            
            dot = testLine.DotFromPoint(mousePos);
            
            int moveSpeed = 4;
            if (Input.IsKeyDown(Keys.A))
            {
                AABB.Move(new Vector2I(-1 * moveSpeed, 0));
            }
            if (Input.IsKeyDown(Keys.D))
            {
                AABB.Move(new Vector2I(1 * moveSpeed, 0));
            }
            if (Input.IsKeyDown(Keys.W))
            {
                AABB.Move(new Vector2I(0, -1 * moveSpeed));
            }
            if (Input.IsKeyDown(Keys.S))
            {
                AABB.Move(new Vector2I(0, 1 * moveSpeed));
            }
            if (Input.IsKeyPressed(Keys.Space))
            {
                velocity = new Vector2I(0, -20);
            }
            //Console.WriteLine(Input.GetLeftStick(PlayerIndex.One));
            velocity += gravity;
            AABB.Move(velocity);
            Collisions();
            //Vector2 start
            base.Update(gameTime);
        }

        public void Collisions()
        {
            while (testLine.DotFromPoint(AABB.BottomRight) < 0)
            {
                AABB.Move(new Vector2I(0, -1));
            }
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
