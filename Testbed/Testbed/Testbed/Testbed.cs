using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        //public List<TestEntry> TestEntries
        public TestEntry[] TestEntries;
       


        protected Test CurrentTest = null;

        public Game Game;

        public Testbed(Game game)
        {
            //TestEntries = new List<TestEntry>();
            TestEntries = new TestEntry[] {
                new TestEntry (new CreateTestDelegate(_1v1Test.Create)),
                new TestEntry (new CreateTestDelegate(PixelZoom.Create))
            };
            Game = game;
        }

        public void AddTest(TestEntry test)
        {
            //TestEntries.Add(test);
        }

        public void StartTest(int index)
        {
            CurrentTest = TestEntries[index].CreateFunc();
            CurrentTest.Game = Game;
            CurrentTest.Initialize();
            CurrentTest.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (CurrentTest != null)
            {
                CurrentTest.Update(gameTime);
            }
        }

        public void DrawTestList(SpriteBatch spriteBatch)
        {

        }

        public void Draw(SpriteBatch spriteBatch, PrimitiveBatch primBatch)
        {
            if (CurrentTest != null)
                CurrentTest.Draw(spriteBatch, primBatch);
        }
    }
}
