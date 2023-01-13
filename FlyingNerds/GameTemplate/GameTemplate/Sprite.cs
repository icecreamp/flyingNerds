using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlyingNerds
{
    public class Sprite : DrawableGameComponent
    {
        public SpriteBatch SpriteBatch { get; set; }
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Speed { get; set; }

        public Sprite(Microsoft.Xna.Framework.Game game,
            SpriteBatch spriteBatch,
            Vector2 position,
            Texture2D texture,
            Vector2 speed) : base(game)
        {
            SpriteBatch = spriteBatch;
            Position = position;
            Texture = texture;
            Speed = speed;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(Texture, Position, Color.White);

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Gets the perimeter of the Sprite as a Rectangle
        /// </summary>
        /// <returns>A Rectangle of the sprite boundary</returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
    }
}
