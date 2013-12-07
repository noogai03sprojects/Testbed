using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Testbed
{
    class ScrollingText : Test
    {
        public static Test Create()
        {
            ScrollingText test = new ScrollingText();
            test.Name = "Scrolling text";
            test.ContentPath = "ScrollingText";
            test.Description = "Zelda-style scrolling text.";

            return test;
        }




        new SpriteFont Font;

        int position, length;

        string text = "Hello this is a test for scrolling text like in Legend of Zelda #yolo";
        string toPrint;

        Timer timer;

        public override void Initialize()
        {
            length = text.Length;
            position = 0;
            toPrint = "";

            timer = new Timer(15);            
            timer.Elapsed += Tick;
            timer.Start();
            base.Initialize();
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
            if (position < length)
            {
                toPrint += text[position];
                position++;
            }
            else
            {
                toPrint = "";
                position = 0;
            }
        }

        public override void LoadContent()
        {
            Font = Content.Load<SpriteFont>("Font");
            base.LoadContent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Space))
            {
                if (timer.Interval != 5)
                    timer.Interval = 5;
            }
            else
            {
                if (timer.Interval != 20)
                    timer.Interval = 20;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, PrimitiveBatch primBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(Font, toPrint, new Vector2(10, 10), Color.White);

            spriteBatch.End();
            base.Draw(spriteBatch, primBatch);
        }
    }
}
