using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.MediaFoundation;
using System.Windows.Forms;

namespace FlyingNerds
{
    /// <summary>
    /// Start scene
    /// </summary>
    public class StartScene : GameScene
    {
        public MenuComponent Menu { get; set; }
        
        // Menu items
        string[] menuItems = { "Start",
            "Help", "High Score", "About", "Exit" };

        // Set background
        public MainBackground Background;

        // Index for animation
        int aliFrameIndex = 0;

        // Speed of the animation
        int frameDelay = 13;

        // Make the animation repeat
        int frameDelayCounter = 0;

        // Load images
        public Texture2D logo = Shared.Game.Content.Load<Texture2D>("images/logo");
        private Texture2D thomas = Shared.Game.Content.Load<Texture2D>("images/thomas");
        private Texture2D[] aliFrame =
       {
            Shared.Game.Content.Load<Texture2D>("images/ali1"),
            Shared.Game.Content.Load<Texture2D>("images/ali2")
        };

        /// <summary>
        /// Create a parallax background and menu in start scene
        /// </summary>
        /// <param name="game"></param>
        public StartScene(Microsoft.Xna.Framework.Game game) : base(game)
        {
            // Load fonts
            SpriteFont regular = game.Content.Load<SpriteFont>("fonts/regularFont");
            SpriteFont highlight = game.Content.Load<SpriteFont>("fonts/highlightFont");

            // Set background
            Background = new MainBackground(game, spriteBatch);
            this.Components.Add(Background);

            Menu = new MenuComponent(game, spriteBatch, regular, highlight, menuItems);
            this.Components.Add(Menu);

           
        }

        public override void Update(GameTime gameTime)
        {

            // Animation of Ali
            if (frameDelayCounter >= frameDelay)
            {
                // Show the first image
                if (aliFrameIndex == 1)
                    aliFrameIndex = 0;
                // Show the second image
                else
                    aliFrameIndex = 1;
                // Reset the timer
                frameDelayCounter = 0;
            }
            else
                // Increase the animation timer
                frameDelayCounter++;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            // Thomas
            spriteBatch.Draw(thomas, new Vector2(Shared.Stage.X - 300, Shared.Stage.Y - thomas.Height), Color.White);

            // Ali animation
            spriteBatch.Draw(aliFrame[aliFrameIndex], new Vector2(Shared.Stage.X - 700, Shared.Stage.Y - 180), Color.White);

            // Logo
            spriteBatch.Draw(logo, new Vector2(30, 0), Color.White);

            spriteBatch.End();
        
        }
    }
}
