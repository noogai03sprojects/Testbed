using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Testbed
{
    struct TestEntry
    {
        
        public CreateTestDelegate CreateFunc;

        public TestEntry(CreateTestDelegate func)
        {            
            CreateFunc = func;
        }
    }

    delegate Test CreateTestDelegate();

    class Testbed
    {

        SpriteFont Font;
        //public List<TestEntry> TestEntries
        public TestEntry[] TestEntries;   

        protected Test CurrentTest = null;

        public Game Game;

        bool bDrawData = true;

        public Testbed(Game game)
        {
            //TestEntries = new List<TestEntry>();
            TestEntries = new TestEntry[] {
                new TestEntry (new CreateTestDelegate(_1v1Test.Create)),
                new TestEntry (new CreateTestDelegate(PixelZoom.Create)),
                new TestEntry (new CreateTestDelegate(ScrollingText.Create)),
                new TestEntry (LineResolution.Create)
            };
            Game = game;
        }

        public void AddTest(TestEntry test)
        {
            //TestEntries.Add(test);
        }

        public void LoadContent(ContentManager content)
        {
            Font = content.Load<SpriteFont>("Font");
        }

        public void StartTest(int index)
        {            
            if (CurrentTest!= null)
                CurrentTest.Unload();

            CurrentTest = TestEntries[index].CreateFunc();
            CurrentTest.Game = Game;
            CurrentTest.Initialize();
            CurrentTest.LoadContent();
        }

        public void NextTest()
        {

        }

        public void LastTest()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (CurrentTest != null)
            {
                CurrentTest.Update(gameTime);
            }
            if (Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.F1))
                bDrawData = !bDrawData;
        }

        public void DrawTestList(SpriteBatch spriteBatch)
        {

        }

        public void Draw(SpriteBatch spriteBatch, PrimitiveBatch primBatch)
        {          

            if (CurrentTest != null)
                CurrentTest.Draw(spriteBatch, primBatch);

            if (bDrawData)
            {

                string text = "Name: " + CurrentTest.Name + "\nDescription: " + CurrentTest.Description;
                Vector2 size = Font.MeasureString(text);
                Rectangle rect = new Rectangle(7, 2, (int)size.X, (int)size.Y);

                spriteBatch.Begin();
                spriteBatch.Draw(Art.Pixel, rect, Color.Gray * 0.7f);
                spriteBatch.DrawString(Font, text, new Vector2(7, 2), Color.Black);
                spriteBatch.End();
            }
        }
    }
}
