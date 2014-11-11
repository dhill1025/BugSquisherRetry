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

namespace BugSquisherRetry
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background, spritesheet;

        Sprite hand;

        List<Bug> bugs = new List<Bug>();

        Random rnd = new Random();

        int bugsNum = 60;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("background");
            spritesheet = Content.Load<Texture2D>("spritesheet");
            hand = new Sprite(new Vector2(0,0), spritesheet, new Rectangle(135, 200, 44, 44), new Vector2(0, 0));
            

                for (int i = 0; i < bugsNum; i++)
                {
                    int bugX = rnd.Next(0, 3);
                    int bugY = rnd.Next(0, 2);
                    Bug bug = new Bug(new Vector2(rnd.Next(-450, -65), rnd.Next(65, 450)), spritesheet, new Rectangle(64 * bugX, 64 * bugY, 64, 64), new Vector2(rnd.Next(40, 150), rnd.Next(-40, 40)));

                    bugs.Add(bug);
                }
                
            }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
            // TODO: Unload any non ContentManager content here
        }

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

            MouseState ms = Mouse.GetState();

            for (int i = 0; i < 60; i++)
            {
                if (hand.IsBoxColliding(bugs[i].BoundingBoxRect) && ms.LeftButton == ButtonState.Pressed)
                {
                    bugs[i].frames[0] = new Rectangle(0, 143, 130, 100);
                    bugs[i].Velocity = new Vector2(0, 0);
                }
            }


            Vector2 target = Vector2.Zero;

            

            if (ms.LeftButton == ButtonState.Pressed)
            {
                target = new Vector2(ms.X, ms.Y);
            }

            hand.Location = new Vector2(ms.X -25, ms.Y-25);

            // TODO: Add your update logic here
            for (int i = 0; i < bugs.Count; i++)
            {
                bugs[i].Target = target;
                bugs[i].Update(gameTime);
                //bugs[i].Velocity*=new Vector2(100, rnd.Next(-100,100));
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            MouseState ms = Mouse.GetState();

            spriteBatch.Begin();

            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            
            
            hand.Draw(spriteBatch);

            for (int i = 0; i < bugs.Count; i++)
            {
                bugs[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
