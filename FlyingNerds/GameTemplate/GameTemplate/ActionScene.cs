using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;


namespace FlyingNerds
{
    /// <summary>
    /// Game play scene
    /// </summary>
    public class ActionScene : GameScene
    {
        // Get the city and the sky background
        public MainBackground Background;
        // Level two Scene
        public LevelTwoScene levelTwoScene;
        public NextLevelScene nextLevelScene;

        // Get images of the character
        private Texture2D[] jordan = {
            Shared.Game.Content.Load<Texture2D>("images/mainNerd1"),
            Shared.Game.Content.Load<Texture2D>("images/mainNerd2")
        };

        // Get images of obstacles
        private Texture2D[] obstacleTextures =
        {
            Shared.Game.Content.Load<Texture2D>("images/obstacle1"),
            Shared.Game.Content.Load<Texture2D>("images/obstacle2"),
            Shared.Game.Content.Load<Texture2D>("images/obstacle3"),
            Shared.Game.Content.Load<Texture2D>("images/obstacle4"),
            Shared.Game.Content.Load<Texture2D>("images/obstacle5")
        };

       

        // List of obstacles in the screen
        public List<Obstacle> obstacles = new List<Obstacle>();

        public SoundEffect gameOverSong;
        public SoundEffect nextLevelSong;

        // Load Font
        SpriteFont hilight = Shared.Game.Content.Load<SpriteFont>("fonts/hilightFont");

        // Animation settings
        int joranFrameIndex = 0;
        int frameDelay = 12;
        int frameDelayCounter = 0;

        // Controls the creating of Obstacles
        int obstacleDelay = 90;
        int obstacleDelayCounter = 0;

        // Get random y coord for obstacle spawning
        Random random = new Random();

        // Track play time
        public int playTime = 0;

        // Detect if the character touched an obstacle
        public bool dead = false;
        


        // Position of the character
        public Vector2 jordanPosition = new Vector2(Shared.Stage.X / 8, Shared.Stage.Y / 2);

        /// <summary>
        /// Loads the main background parallax
        /// </summary>
        /// <param name="game"></param>
        public ActionScene(Microsoft.Xna.Framework.Game game) : base(game)
        {
            // Creat the background object
            Background = new MainBackground(game, spriteBatch); 
            this.Components.Add(Background);

            GameOverScene gameOverScene = new GameOverScene(Game);

            gameOverSong = Shared.Game.Content.Load<SoundEffect>("music/dead");
            nextLevelSong = Shared.Game.Content.Load<SoundEffect>("music/wow");
        }


        /// <summary>
        /// Update the game screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Create obstacle timer
            if (obstacleDelayCounter >= obstacleDelay)
            {
                // Select a random texture
                int obstIndex = random.Next(0, obstacleTextures.Length);

                // Create obstacle
                Obstacle obstacle = new Obstacle(Game, this, obstacleTextures[random.Next(0, obstacleTextures.Length)], obstacles);
                this.Components.Add(obstacle);
                obstacles.Add(obstacle);

                // Reset the timer
                obstacleDelayCounter = 0;
            }
            else
                // Increase the timer
                obstacleDelayCounter++;

            // Animation
            if (frameDelayCounter >= frameDelay)
            {
                // Show the first image
                if (joranFrameIndex == 1)
                    joranFrameIndex = 0;
                // Show the second image
                else
                    joranFrameIndex = 1;
                // Reset the timer
                frameDelayCounter = 0;
            }
            else
                // Increase the animation timer
                frameDelayCounter++;

            // Get keyboard state
            KeyboardState keyboardState = Keyboard.GetState();

            // When up is pressed change y coordinate of the character
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                jordanPosition.Y -= 5;
            }
            // When down is pressed change y coordinate of the character
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                jordanPosition.Y += 5;
            }
            // When left is pressed change x coordinate of the character
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                jordanPosition.X -= 5;
            }
            // When right is pressed change x coordinate of the character
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                jordanPosition.X += 5;
            }

            // Set bounds of character
            if (jordanPosition.X < 0)
            {
                jordanPosition.X = 0;
            }
            if (jordanPosition.Y < 0)
            {
                jordanPosition.Y = 0;
            }
            if (jordanPosition.X > Shared.Stage.X - jordan[joranFrameIndex].Width)
            {
                jordanPosition.X = Shared.Stage.X - jordan[joranFrameIndex].Width;
            }
            if (jordanPosition.Y > Shared.Stage.Y - jordan[joranFrameIndex].Height)
            {
                jordanPosition.Y = Shared.Stage.Y - jordan[joranFrameIndex].Height;
            }

            // Set rectangle for the character
            Rectangle jordanRect = new Rectangle((int)jordanPosition.X, (int)jordanPosition.Y, jordan[joranFrameIndex].Width, jordan[joranFrameIndex].Height);

         
            // For every obstacle
            foreach (var obt in obstacles)
            {

                // Check if collide with the character
                if (obt.obstRect.Intersects(jordanRect))
                {
                    // Set dead as true
                    dead = true;
                    MediaPlayer.Pause();
                    gameOverSong.Play();
                    break;
                }
              
            }
            
            // Check if the player is dead
            if (!dead)
                // Count the play time
                playTime++;
            else
            {
                // Show the game over screen
                this.Hide();
                Shared.Game.gameOverScene.Show();
                ResetScene();

                obstacles.Clear();
            }

             if (playTime / 60 / 2 >= 15)
             {
                this.Hide();
                Shared.Game.nextLevelScene.Show();
                ResetScene();

                obstacles.Clear();
                MediaPlayer.Pause();
                nextLevelSong.Play();

            }


            base.Update(gameTime);
        }


        /// <summary>
        /// Display images for playing the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            // Main character
            spriteBatch.Draw(jordan[joranFrameIndex], jordanPosition, Color.White);

            // Level
            spriteBatch.DrawString(hilight, "Level 1",
                new Vector2(10, 0), Color.Black);

            // Score 
            spriteBatch.DrawString(hilight, $"{Convert.ToString(playTime / 60 / 2)} / 15", 
                new Vector2(
                    Shared.Stage.X / 2 - hilight.MeasureString(Convert.ToString(playTime / 60 / 2)).X,
                    0
                ), Color.Black);

            spriteBatch.End();

        }

        /// <summary>
        /// Reset the game
        /// </summary>
        public void ResetScene()
        {

            // Reset score
            Shared.Game.actionScene.playTime = 0;

            // Reset Position
            Shared.Game.actionScene.jordanPosition = new Vector2(Shared.Stage.X / 8, Shared.Stage.Y / 2);

            // Remove Obstacles
            // If the player is dead, remove all the obstacles
            foreach (Obstacle obst in this.obstacles)
            {
                obst.Enabled = false;
                obst.Visible = false;

                this.Components.Remove(obst);
            }
            obstacles.Clear();

            Shared.Game.actionScene.dead = false;
        }

    }
}
