using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameTemplate
{
    public class Bat : DrawableGameComponent
    {
        public SpriteBatch spriteBatch { get; set; }
        public Vector2 position { get; set; }
        public Vector2 speed { get; set; }
        public Texture2D tex { get; set; }
        public Bat(Game game, SpriteBatch spriteBatch, Vector2 position, Vector2 speed, Texture2D tex) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.speed = speed;
            this.tex = tex;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            //big mistake -- never write the following line
            //KeyboardState ks = new KeyboardState();
            //-------------------------------------------

            if (ks.IsKeyDown(Keys.Left))
            {
                position -= speed;
                if (position.X < 0)
                {
                    position = new Vector2(0, position.Y);
                }
            }
             
            if (ks.IsKeyDown(Keys.Right))
            {
                position += speed;
                if (position.X > Shared.stage.X - tex.Width)
                {
                    position = new Vector2(Shared.stage.X - tex.Width, position.Y);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //v2
            spriteBatch.Draw(tex,position, Color.White); 
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
