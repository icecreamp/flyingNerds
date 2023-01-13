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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace FlyingNerds
{
    public class NextLevelScene : GameScene
    {

        public MainBackground Background;

        // Get images of gameover screen
        private Texture2D NextLevelScreen = Shared.Game.Content.Load<Texture2D>("images/levelChange");
        private Texture2D next = Shared.Game.Content.Load<Texture2D>("images/next");

        // Get mouse state
        MouseState oldMouseState = Mouse.GetState();

        // Set positions of the button
        Vector2 nextPosition = new Vector2(Shared.Stage.X - 400, Shared.Stage.Y - 100);

        public NextLevelScene(Microsoft.Xna.Framework.Game game) : base(game)
        {
            game.Components.Add(this);

            // Creat the background object
            Background = new MainBackground(game, spriteBatch);
            this.Components.Add(Background);
        }

        public override void Update(GameTime gameTime)
        {
            // Get mouse state
            MouseState mouseState = Mouse.GetState();
            // Coordinates of the mouse
            Point mousePoint = new Point(mouseState.X, mouseState.Y);
            // Set positions          
            Rectangle nextRec = new Rectangle((int)nextPosition.X, (int)nextPosition.Y, next.Width, next.Height);

            // Next level button
            if (nextRec.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Pressed)
            {

                // Hide opened screens
                Shared.Game.HideAllScenes();
                // Go to the next level
                Shared.Game.levelTwoScene.Show();
                // Reset the game play 
                Shared.Game.actionScene.ResetScene();

                // Play the game song
                MediaPlayer.Pause();
                MediaPlayer.Play(Shared.Game.playSong);
                return;
            }

            oldMouseState = mouseState;

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Shared.spriteBatch.Begin();
            
            // Next level screen
            Shared.spriteBatch.Draw(NextLevelScreen, new Vector2(0, 0), Color.White);

            // Button
            Shared.spriteBatch.Draw(next, new Vector2((int)nextPosition.X, (int)nextPosition.Y), Color.White);

            Shared.spriteBatch.End();
        }


    }
}
