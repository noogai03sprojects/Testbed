using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Diagnostics;

namespace Testbed
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PrimitiveBatch primBatch;

        Testbed Testbed;

        bool bSelectingTest = false;

        int currentTest = 0;

        KeyboardState oldState;

        string prefsPath = "prefs";

        public static Game1 Instance;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Instance = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Art.LoadContent(Content, this);
            Testbed = new Testbed(this);

            oldState = Keyboard.GetState();

            //Testbed.AddTest(new TestEntry
            //DirectoryInfo info = new DirectoryInfo(Environment.CurrentDirectory);
            //if (info.
            
            //this.OnExiting()
            //Environment.
            //base.Exiting += Exiting;
            base.Initialize();
        }        
        
        protected override void OnExiting(object sender, EventArgs args)
        {
            using (StreamWriter writer = new StreamWriter(prefsPath, false))
            {
                writer.Write(currentTest);
            }
            Debug.WriteLine("unload");
            base.OnExiting(sender, args);
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            primBatch = new PrimitiveBatch(GraphicsDevice);

            Testbed.LoadContent(Content);

            if (File.Exists(prefsPath))
            {
                using (StreamReader reader = new StreamReader(prefsPath))
                {
                    int test = Convert.ToInt32(reader.ReadToEnd());
                    Testbed.StartTest(test);
                    currentTest = test;
                }
            }
            else
                Testbed.StartTest(0);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            
        }        
       
        //override 
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState newState = Keyboard.GetState();
            if (Input.IsKeyPressed(Keys.Left))
            {
                if (currentTest > 0)
                currentTest--;
                Testbed.StartTest(currentTest);
            }
            if (Input.IsKeyPressed(Keys.Right))
            {
                if (currentTest < Testbed.TestEntries.Length - 1)
                    currentTest++;
                Testbed.StartTest(currentTest);
            }

            Testbed.Update(gameTime);

            // TODO: Add your update logic here

            oldState = newState;


            Input.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            if (bSelectingTest)
                Testbed.DrawTestList(spriteBatch);
            else
                Testbed.Draw(spriteBatch, primBatch);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        
    }
}
