using FlyingNerds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FlyingNerds
{
    public class Obstacle : DrawableGameComponent
    {
        // Get Image and the position
        Texture2D tex;
        private Vector2 obstaclePosition;

        // Create a random number
        Random random = new Random();
        
        // Rectangle of obstacles
        public Rectangle obstRect;

        // List of created obstacles
        List<Obstacle> obstacles;
        ActionScene actionScene;

        /// <summary>
        /// Get value of an obstacle
        /// </summary>
        /// <param name="game"></param>
        /// <param name="tex"></param>
        /// <param name="obstacles"></param>
        public Obstacle(Microsoft.Xna.Framework.Game game, GameScene scene, Texture2D tex, List<Obstacle> obstacles) : base (game)
        {   
            // Set texture
            this.tex = tex;
            this.obstacles = obstacles; 

            // Set position
            obstaclePosition = new Vector2(Shared.Stage.X, random.Next(0, (int)Shared.Stage.Y - tex.Height));
            scene.Components.Add(this);
        }

        /// <summary>
        /// Update the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
           
            // Update position
            obstaclePosition.X -= 1;

            // check if off screen
            if(obstaclePosition.X >= Shared.Stage.X)
            {
                // Remove the obstacles
                obstacles.Remove(this);
                actionScene.Components.Remove(this);
            }

            // Set rectangles of obstacles
            obstRect = new Rectangle((int)obstaclePosition.X, (int)obstaclePosition.Y, tex.Width, tex.Height);

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

            Shared.spriteBatch.Draw(tex, obstaclePosition, Color.White);

            Shared.spriteBatch.End();
            
        }
    }
}
