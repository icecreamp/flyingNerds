using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace FlyingNerds
{
    public class AboutScene : GameScene
    {
        public MainBackground Background;

        // Load Font
        SpriteFont regular = Shared.Game.Content.Load<SpriteFont>("fonts/regularFont");
        SpriteFont hilight = Shared.Game.Content.Load<SpriteFont>("fonts/hilightFont");

        // Load image
        public Texture2D goBack = Shared.Game.Content.Load<Texture2D>("images/goback");

        // Set texts
        string intro = "Hyunjin Kim\nYusuf Hafeji\n\n2022-12-07";

        /// <summary>
        /// Set the parallax background
        /// </summary>
        /// <param name="game"></param>
        public AboutScene(Microsoft.Xna.Framework.Game game) : base(game)
        {

            // Set background
            Background = new MainBackground(game, spriteBatch);
            this.Components.Add(Background);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
         
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            // Logo
            spriteBatch.Draw(Shared.Game.startScene.logo , new Vector2(Shared.Stage.X / 2 - 100, 40), Color.White);
            // Names
            spriteBatch.DrawString(regular, intro, new Vector2(Shared.Stage.X / 2 - 120, 200), Color.Black);
            // Information
            spriteBatch.Draw(goBack, new Vector2(Shared.Stage.X - goBack.Width - 10, Shared.Stage.Y - goBack.Height - 15), Color.White);
            spriteBatch.End();
        }
    }
}
