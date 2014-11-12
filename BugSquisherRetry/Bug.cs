using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BugSquisherRetry
{
    enum BugState
    {
        MOVING,
        CHASING,
        WAITING,
        DEADASHELL
    }

    class Bug : Sprite
    {
        public Vector2 Target;
        public BugState State;

        private Random rand = new Random((int)DateTime.UtcNow.Ticks);

        private float bugTimer = 0f;
        private float bugTimerMax = 150f;

        public Bug(
            Vector2 location,
            Texture2D texture,
            Rectangle initialFrame,
            Vector2 velocity)
            : base(location, texture, initialFrame, velocity)
        {
            Target = Vector2.Zero;
            State = BugState.MOVING;

            System.Threading.Thread.Sleep(1);
        }

        public override void Update(GameTime gameTime)
        {
            

            if (State == BugState.MOVING)
            {
                this.TintColor = Color.White;
                bugTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (bugTimer > bugTimerMax)
                {
                    bugTimer = (int)rand.Next(0, (int)bugTimerMax/2);

                    Vector2 target = new Vector2(this.Center.X + 100, this.Center.Y + rand.Next(-50, 50));
                    Vector2 velocity = target - Center;
                    velocity.Normalize();
                    velocity *= 100;

                    this.Velocity = velocity;
                    Rotation = (float)Math.Atan2(velocity.Y, Velocity.X);
                }
            }

            if (State == BugState.WAITING)
            {
                Vector2 velocity = this.Velocity;
                velocity.Normalize();
                velocity *= 10;

                this.TintColor = Color.Red;

                this.Velocity = velocity;
            }

            if (State == BugState.DEADASHELL)
            {
                Vector2 velocity = this.Velocity;
                velocity.Normalize();
                velocity *= 0;


                this.Velocity = velocity;
            }

            
            if (Target != Vector2.Zero)
            {
                // Follow the target
                //Vector2 target = new Vector2(10, 100);
                Vector2 velocity = Target - Center;
                velocity.Normalize();
                velocity *= 100;
                this.Velocity = velocity;

                Rotation = (float)Math.Atan2(velocity.Y, Velocity.X);
                
                

            }
            
            if (Target == Vector2.Zero)
            {
                //velocity.Normalize();
               // Velocity*=new Vector2(100, rnd.Next(-100,100));
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        
    }
}
