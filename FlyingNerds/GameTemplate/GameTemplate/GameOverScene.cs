using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using SharpDX.Direct2D1;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Xna.Framework.Media;

namespace FlyingNerds
{
    public class GameOverScene : GameScene
    {
        // Get images of gameover screen
        private Texture2D gameOverScreen = Shared.Game.Content.Load<Texture2D>("images/gameoverScreen");
        public Texture2D replay = Shared.Game.Content.Load<Texture2D>("images/replay");
        public Texture2D exit = Shared.Game.Content.Load<Texture2D>("images/exit");

        // Get mouse state
        MouseState oldMouseState= Mouse.GetState();

        // Set positions of the buttons
        Vector2 replayPosition = new Vector2(Shared.Stage.X - 700, Shared.Stage.Y - 140);
        Vector2 exitPosition = new Vector2(Shared.Stage.X - 200, Shared.Stage.Y - 140);

        public GameOverScene(Microsoft.Xna.Framework.Game game) : base(game)
        {
            game.Components.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            // Get mouse state
            MouseState mouseState = Mouse.GetState();
            // Coordinates of the mouse
            Point mousePoint = new Point(mouseState.X, mouseState.Y);
            // Set positions
            Rectangle replayRec = new Rectangle((int)replayPosition.X, (int)replayPosition.Y, replay.Width, replay.Height);
            Rectangle exitRec = new Rectangle((int)exitPosition.X, (int)exitPosition.Y, exit.Width, exit.Height); 

            // Replay button
            if (replayRec.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Pressed)
            {
                // Hide opened screens
                Shared.Game.HideAllScenes();
                // Replay the game
                Shared.Game.actionScene.Show();
                // Reset the game play 
                Shared.Game.actionScene.ResetScene();
                // Play the game song
                MediaPlayer.Pause();
                MediaPlayer.Play(Shared.Game.playSong);
                return;
            }

            // Exit button
            if (exitRec.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Pressed)
            {
                // Hide opened screens
                Shared.Game.HideAllScenes();
                // Reset the game
                Shared.Game.actionScene.ResetScene();
                // Go to the start screen
                Shared.Game.startScene.Show();
                // Play the start screen song
                MediaPlayer.Pause();
                MediaPlayer.Play(Shared.Game.mainSong);
                return;
            }

            oldMouseState = mouseState;

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            Shared.spriteBatch.Begin();

            // Game over screen
            Shared.spriteBatch.Draw(gameOverScreen, new Vector2(0, 0), Color.White);
            // Buttons
            Shared.spriteBatch.Draw(replay, new Vector2((int)replayPosition.X, (int)replayPosition.Y), Color.White);
            Shared.spriteBatch.Draw(exit, new Vector2((int)exitPosition.X, (int)exitPosition.Y), Color.White);

            Shared.spriteBatch.End();
        }


    }
}
