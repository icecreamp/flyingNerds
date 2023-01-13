using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace FlyingNerds
{
    /// <summary>
    /// Help scene
    /// </summary>
    public class HelpScene : GameScene
    {
        // Load the image 
        private Texture2D help = Shared.Game.Content.Load<Texture2D>("images/help");

        // Load the background
        public MainBackground Background;

        /// <summary>
        /// Creates Parallax for help screen
        /// </summary>
        /// <param name="game"></param>
        public HelpScene(Microsoft.Xna.Framework.Game game) : base(game)
        { 
            // Set background
            Background = new MainBackground(game, spriteBatch);
            this.Components.Add(Background);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            // Image of the game guide
            spriteBatch.Draw(help, new Vector2(0 ,0), Color.White);          
            spriteBatch.End();
        }
    }
}
