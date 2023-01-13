using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.IO;
using System.Linq;

namespace FlyingNerds
{

    public class HighScoreScene : GameScene
    {

        public ActionScene actionScene;

        // Load the background
        public MainBackground Background;

        SpriteFont regular = Shared.Game.Content.Load<SpriteFont>("fonts/regularFont");

        /// <summary>
        /// Creates Parallax for help screen
        /// </summary>
        /// <param name="game"></param>
        public HighScoreScene(Microsoft.Xna.Framework.Game game) : base(game)
        {
            // Set background
            Background = new MainBackground(game, spriteBatch);
            this.Components.Add(Background);
        }

        public override void Update(GameTime gameTime)
        {
           DisplayHighScore();

            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Shared.spriteBatch.Begin();
            Shared.spriteBatch.DrawString(regular , DisplayHighScore(), new Vector2(200, 100), Color.Black);
            Shared.spriteBatch.End();

        }

        /// <summary>
        /// Return a string of the best score
        /// </summary>
        /// <returns></returns>
        public string DisplayHighScore()
        {
            // initilaize variables
            string fileName = "highScore.txt";
            string rank = "";
            string score = "";

            //// When the file exists
            //if (File.Exists(fileName))
            //{
            //    // Read the file
            //    using (StreamReader _reader = new StreamReader(fileName))
            //    {
            //        // When the file is null
            //        if(_reader.ReadLine() == null)
            //        {
            //            rank = $"* Level2 The Best Score *\n\n0";
            //        }
            //        else
            //        {

            //        score = _reader.ReadLine();
            //        rank = $"* Level2 The Best Score *\n\n{score}";

            //        }
                   
            //    }
            //}
            //else
            //{
            //    rank = $"* Level2 The Best Score *\n\n0";
            //}
            rank = $"* Level2 The Best Score *\n\n0";
            return rank;
        }

    }


}
