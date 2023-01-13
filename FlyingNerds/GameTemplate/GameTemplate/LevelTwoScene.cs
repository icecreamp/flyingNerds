using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace FlyingNerds
{
    /// <summary>
    /// Game play scene
    /// </summary>
    public class LevelTwoScene : GameScene
    {
        // Get the city and the sky background
        public MainBackground Background;
        Game game;

        // Level one Sene
        public ActionScene actionScene;
        // Level two Scene
        public LevelTwoScene levelTwoScene;
        public NextLevelScene nextLevelScene;

        // Get images of the character
        private Texture2D[] jordan = {
            Shared.Game.Content.Load<Texture2D>("images/mainNerd1"),
            Shared.Game.Content.Load<Texture2D>("images/mainNerd2")
        };


        // Get images of the enemy
        private Texture2D[] enemy = {
            Shared.Game.Content.Load<Texture2D>("images/enemy1"),
            Shared.Game.Content.Load<Texture2D>("images/enemy2")
        };

        // Get images of obstacles
        private Texture2D[] obstacleTextures =
        {
            Shared.Game.Content.Load<Texture2D>("images/obstacle2"),
            Shared.Game.Content.Load<Texture2D>("images/obstacle3"),
            Shared.Game.Content.Load<Texture2D>("images/obstacle5")
        };

        // List of obstacles in the screen
        public List<Obstacle> obstacles = new List<Obstacle>();
        public List<ObstacleLevelTwo> newObstacles = new List<ObstacleLevelTwo>();

        public SoundEffect gameOverSong;
        public SoundEffect nextLevelSong;

        // Load Font
        SpriteFont hilight = Shared.Game.Content.Load<SpriteFont>("fonts/hilightFont");

        // Animation settings
        int jordanFrameIndex = 0;
        int frameDelay = 12;
        int frameDelayCounter = 0;

        // Enemy animation settings
        int enemyFrameIndex = 0;
        int enemyFrameDelay = 10;
        int enemyFrameDelayCounter = 0;

        // Change enemy position
        bool goToRight = true;
        bool goToleft = false;

        // Controls the creating of obstacles
        int obstacleDelay = 140;
        int obstacleDelayCounter = 0;

        // Controls the creating of new obstacles
        int newObstacleDelay = 180;
        int newObstacleDelayCounter = 60;

        // Get random y coord for obstacle spawning
        Random random = new Random();

        // Track play time
        public int playTime = 0;

        // Detect if the character touched an obstacle
        public bool dead = false;


        // Position of the character
        public Vector2 jordanPosition = new Vector2(Shared.Stage.X / 8, Shared.Stage.Y / 2);
        public Vector2 enemyPosition = new Vector2(0, 28);


        /// <summary>
        /// Loads the main background parallax
        /// </summary>
        /// <param name="game"></param>
        public LevelTwoScene(Microsoft.Xna.Framework.Game game) : base(game)
        {
            // Creat the background object
            Background = new MainBackground(game, spriteBatch);
            this.Components.Add(Background);

            GameOverScene gameOverScene = new GameOverScene(Game);

            gameOverSong = Shared.Game.Content.Load<SoundEffect>("music/dead");
            // nextLevelSong = Shared.Game.Content.Load<SoundEffect>("music/wow");
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


            // Create new obstacle timer
            if (newObstacleDelayCounter >= newObstacleDelay)
            {
                // Create obstacle
                ObstacleLevelTwo levelTwoEnemy = new ObstacleLevelTwo(Game, this, enemyPosition);
                newObstacles.Add(levelTwoEnemy);

                // Reset the timer
                newObstacleDelayCounter = 0;
            }
            else
                // Increase the timer
                newObstacleDelayCounter++;


            // Jordan animation
            if (frameDelayCounter >= frameDelay)
            {
                // Show the first image
                if (jordanFrameIndex == 1)
                    jordanFrameIndex = 0;
                // Show the second image
                else
                    jordanFrameIndex = 1;
                // Reset the timer
                frameDelayCounter = 0;
            }
            else
                // Increase the animation timer
                frameDelayCounter++;

            // Enemy animation
            if (enemyFrameDelayCounter >= enemyFrameDelay)
            {
                // Show the first image
                if (enemyFrameIndex == 1)
                    enemyFrameIndex = 0;
                // Show the second image
                else
                    enemyFrameIndex = 1;
                // Reset the timer
                enemyFrameDelayCounter = 0;
            }
            else
                // Increase the animation timer
                enemyFrameDelayCounter++;

            // Move the enemy to right
            if (goToRight)
            {
                enemyPosition.X += 1;

                if (enemyPosition.X >= Shared.Stage.X - enemy[enemyFrameIndex].Width)
                {
                    enemyPosition.X = Shared.Stage.X - enemy[enemyFrameIndex].Width;
                    goToRight = false;
                    goToleft = true;
                }

            }

            // Move the enemy to left
            if (goToleft)
            {
                enemyPosition.X -= 1;

                if (enemyPosition.X <= 0)
                {
                    enemyPosition.X = 0;
                    goToleft = false;
                    goToRight = true;

                }
            }


            // Set rectangle for the character
            Rectangle enemyRect = new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, enemy[enemyFrameIndex].Width, enemy[enemyFrameIndex].Height);

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
            if (jordanPosition.X > Shared.Stage.X - jordan[jordanFrameIndex].Width)
            {
                jordanPosition.X = Shared.Stage.X - jordan[jordanFrameIndex].Width;
            }
            if (jordanPosition.Y > Shared.Stage.Y - jordan[jordanFrameIndex].Height)
            {
                jordanPosition.Y = Shared.Stage.Y - jordan[jordanFrameIndex].Height;
            }

            // Set rectangle for the character
            Rectangle jordanRect = new Rectangle((int)jordanPosition.X, (int)jordanPosition.Y, jordan[jordanFrameIndex].Width, jordan[jordanFrameIndex].Height);


            // For every obstacle
            foreach (var obt in obstacles)
            {

                // Check if collide with the character
                if (obt.obstRect.Intersects(jordanRect) || enemyRect.Intersects(jordanRect))
                {
                    // Set dead as true
                    dead = true;
                    MediaPlayer.Pause();
                    gameOverSong.Play();
                    break;
                }

            }

            foreach (var obt in newObstacles)
            {
                // Check if collide with the character
                if (obt.poopRect.Intersects(jordanRect))
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
                GetHighScore(playTime);
                this.Hide();
                Shared.Game.gameOverScene.Show();
                ResetScene();
                obstacles.Clear();
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

            // Enemy
            spriteBatch.Draw(enemy[enemyFrameIndex], enemyPosition, Color.White);

            // Main character
            spriteBatch.Draw(jordan[jordanFrameIndex], jordanPosition, Color.White);
            // Level
            spriteBatch.DrawString(hilight, "Level 2",
                new Vector2(10, 0), Color.Black);

            // Score 
            spriteBatch.DrawString(hilight, $"{Convert.ToString(playTime / 60 / 2)}",
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
            Shared.Game.levelTwoScene.playTime = 0;

            // Reset Position
            Shared.Game.levelTwoScene.jordanPosition = new Vector2(Shared.Stage.X / 8, Shared.Stage.Y / 2);
            Shared.Game.levelTwoScene.enemyPosition = new Vector2(0, 28);

            // Remove Obstacles
            // If the player is dead, remove all the obstacles
            foreach (Obstacle obst in this.obstacles)
            {
                obst.Enabled = false;
                obst.Visible = false;

                this.Components.Remove(obst);
            }
            obstacles.Clear();

            foreach (var obst in newObstacles)
            {
                obst.Enabled = false;
                obst.Visible = false;

                this.Components.Remove(obst);
            }
            newObstacles.Clear();

            Shared.Game.levelTwoScene.dead = false;
        }


        /// <summary>
        /// Write high scores
        /// </summary>
        /// <param name="playTime"></param>
        public void GetHighScore(int playTime)
        {
            // Initilaize variables
            string prevScore = "";
            int score = playTime / 60 / 2;
            // Set file name
            string fileName = "highScore.txt";

            using (FileStream Connection = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamReader _reader = new StreamReader(Connection))
                {
                    using (StreamWriter _writer = new StreamWriter(Connection))
                    {
                      
                        //// Get the prebious best score
                        //prevScore = _reader.ReadLine();

                        //if(score >= int.Parse(prevScore))
                        //{
                        //    score = int.Parse(prevScore);
                        //    _writer.WriteLine(score);
                        //}

                    }
                }
            }

        }
    }


        }
