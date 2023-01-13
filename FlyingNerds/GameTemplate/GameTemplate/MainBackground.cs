using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyingNerds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace FlyingNerds
{
    /// <summary>
    /// Draw a parallax background
    /// </summary>
    public class MainBackground : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Rectangle srcRect;
        public Vector2 pos1, pos2;
        private Vector2 speed;
       
        // Load images
        private Texture2D sky = Shared.Game.Content.Load<Texture2D>("images/sky");
        private Texture2D city = Shared.Game.Content.Load<Texture2D>("images/mainBackground2");

        public MainBackground(Microsoft.Xna.Framework.Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.srcRect = new Rectangle(0, 0, (int)Shared.Stage.X, (int)Shared.Stage.Y); ;
            this.pos1 = new Vector2(0, Shared.Stage.Y - srcRect.Height);
            this.pos2 = new Vector2(pos1.X + srcRect.Width, pos1.Y);
            this.speed = new Vector2(1, 0); 
        }

        public override void Update(GameTime gameTime)
        {
            // Make the backgrounds go to right to left
            pos1 -= speed;
            pos2 -= speed;

            // Reset the X coordination of the images when they go more than the game screen
            if (pos1.X < -srcRect.Width)
                pos1.X = pos2.X + srcRect.Width;
            if (pos2.X < -srcRect.Width)
                pos2.X = pos1.X + srcRect.Width;


            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();   

            // Draw two sky images to make it look like constantly moving
            spriteBatch.Draw(sky, pos1, srcRect, Color.White);
            spriteBatch.Draw(sky, pos2, srcRect, Color.White);

            // Draw image of the city
            spriteBatch.Draw(city, Vector2.Zero, srcRect, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}