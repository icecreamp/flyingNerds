/* flyingNerds.cs
 * 
 * Revision History 
 * Hyunjin Kim, Yusuf Hafeji 2010.12.3: Created 
 * Hyunjin Kim, Yusuf Hafeji 2022.12.9: Added code
 * Hyunjin Kim, Yusuf Hafeji 2022.12.11: Comments added
 */


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;
using System.IO;
using System.Linq;

namespace FlyingNerds
{

    /// <summary>
    /// Set up basic settings
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        // Declare variables
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        // Scenes
        public StartScene startScene;
        private HelpScene helpScene;
        private AboutScene aboutScene;
        public ActionScene actionScene;
        public GameOverScene gameOverScene;
        public HighScoreScene highScoreScene;
        public LevelTwoScene levelTwoScene;
        public NextLevelScene nextLevelScene;

        // Default background
        public MainBackground background;

        // Songs
        public Song mainSong;
        public Song playSong;

        // Name of the file
        public string fileName = "highScore.txt";


        /// <summary>
        /// Initializes game with visible mouse
        /// </summary>
        public Game()
        {

            _graphics = new GraphicsDeviceManager(this);
            Shared.Game = this;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Load songs
            mainSong = this.Content.Load<Song>("music/startSceneMusic");
            playSong = this.Content.Load<Song>("music/gamePlayMusic");
 
        }

        /// <summary>
        /// A method that hides all opened scenes
        /// </summary>
        public void HideAllScenes()
        {
            // Hide the current scene
            foreach (GameScene scene in this.Components)
                scene.Hide();
        }


        /// <summary>
        /// Get the width and the height of the game screen
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.Stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        /// <summary>
        /// A method that loads scenes
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Shared.spriteBatch = _spriteBatch;

            // Main Menu scene
            startScene = new StartScene(this);
            this.Components.Add(startScene);
            CreateFile();
            startScene.Show();

            // Play scene
            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);

            // Help scene
            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            // High score scene
            highScoreScene = new HighScoreScene(this);
            this.Components.Add(highScoreScene);

            // About scene
            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            // Game over scene
            gameOverScene = new GameOverScene(this);

            // Level Two 
            levelTwoScene = new LevelTwoScene(this);
            this.Components.Add(levelTwoScene);
            // Next level scene
            nextLevelScene = new NextLevelScene(this);

            // Play the maing song
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(mainSong);

        }


        protected override void Update(GameTime gameTime)
        {

            // Get which scene was selected
            int selectedIndex = 0;

            // Get keyboard state
            KeyboardState ks = Keyboard.GetState();

            // When the user is in the start scene
            if (startScene.Enabled)
            {
                // Open the selected menu
                selectedIndex = startScene.Menu.selectedIndex;
                if (ks.IsKeyDown(Keys.Enter) || ks.IsKeyDown(Keys.Space))
                {
                    // Close if there is an opened scene
                    HideAllScenes();
                    switch (selectedIndex)
                    {
                        // Show the game play scene
                        case 0:
                            actionScene.Show();
                            MediaPlayer.Play(playSong);
                           
                            break;

                        // Show help scene
                        case 1:
                            helpScene.Show();
                            break;

                        // Show high score scene
                        case 2:
                            highScoreScene.Show();
                            break;

                        // Show about scene
                        case 3:
                            aboutScene.Show();
                            break;

                        // Exit the game
                        case 4:
                            Exit();
                            break;
                    }
                }

            }
            else
            {
                // When user clicked ESC key
                if (ks.IsKeyDown(Keys.Escape))
                {
                    // Play the main song
                    if (actionScene.Enabled || levelTwoScene.Enabled)
                    {
                        MediaPlayer.Play(mainSong);
                    }
                    // Hide opened scenes
                    HideAllScenes();

                    // Reset the game scenes
                    actionScene.ResetScene();
                    levelTwoScene.ResetScene();
                    // Go back to the main menu
                    startScene.Show();

                }
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// Create a file
        /// </summary>
        public void CreateFile()
        {
            string fileName = "highScore.txt";

            using (FileStream Connection = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamReader _reader = new StreamReader(Connection))
                {
                    using (StreamWriter _writer = new StreamWriter(Connection))
                    {
                        // Create a file if it doesn't exist
                        if (!File.Exists(fileName))
                        {
                            File.Create(fileName);
                        }

                        if (_reader.ReadLine() == null)
                        {
                            // Initialize the scores
                            _writer.WriteLine(0.ToString());
                        }
                    }
                }
            }

       
        }

    }
}