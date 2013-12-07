using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Testbed
{
    class Test
    {
        public string Name;
        public string ContentPath;
        public string Description;

        protected ContentManager Content;

        public Game Game;
        protected GraphicsDevice Device;

        protected SpriteFont Font;

        public Test(string Name, string Description, Game game)
        {
            Game = game;
            this.Name = Name;
            this.Description = Description;
        }

        public Test()
        {

        }

        public virtual void Initialize()
        {
            Content = new ContentManager(Game.Services, "Content\\" + ContentPath);
            Device = Game.GraphicsDevice;
        }

        public virtual void LoadContent()
        {
            Font = Game1.Instance.Content.Load<SpriteFont>("Font");
        }

        public virtual void Unload()
        {
            Content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, PrimitiveBatch primBatch)
        {

        }
    }
}
