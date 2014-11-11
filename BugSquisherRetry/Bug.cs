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
    class Bug : Sprite
    {
        public Vector2 Target;

        public Bug(
            Vector2 location,
            Texture2D texture,
            Rectangle initialFrame,
            Vector2 velocity)
            : base(location, texture, initialFrame, velocity)
        {
            Target = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            if (Target != Vector2.Zero)
            {
                // Follow the target
                Vector2 target = new Vector2(10, 100);
                Vector2 velocity = target - Center;
                velocity.Normalize();
                velocity *= 100;
                Rotation = (float)Math.Atan2(velocity.Y, Velocity.X);
                
                

            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        
    }
}
