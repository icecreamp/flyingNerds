using FlyingNerds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace FlyingNerds
{
    public class ObstacleLevelTwo : DrawableGameComponent
    {

        // Image of the obstacle
        private Texture2D poop = Shared.Game.Content.Load<Texture2D>("images/poop");

        // Set the position
        private Vector2 poopPosition;

        // Rectangle of obstacle
        public Rectangle poopRect;

        LevelTwoScene levelTwoScene;

        public ObstacleLevelTwo(Microsoft.Xna.Framework.Game game, GameScene scene, Vector2 poopPosition) : base(game)
        {
            this.poopPosition = poopPosition;
            scene.Components.Add(this);
        }

        /// <summary>
        /// Update the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
         
            if ((int)poopPosition.X != Shared.Stage.Y)
            {
                poopPosition.Y += 1;
            }
            else
            {
                levelTwoScene.Components.Remove(this);
            }

            poopRect = new Rectangle((int)poopPosition.X, (int)poopPosition.Y, poop.Width, poop.Height);


            base.Update(gameTime);
        }

        /// <summary>
        /// Draw obstacles in the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Shared.spriteBatch.Begin();

            Shared.spriteBatch.Draw(poop, poopPosition, Color.White);

            Shared.spriteBatch.End();

        }
    }
}
