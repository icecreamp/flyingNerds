using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameTemplate
{
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Game1 g;
        private Bat bat;
        public ActionScene(Game game) : base(game)
        {
            g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            //instantiate all game components

            Texture2D batTex = game.Content.Load<Texture2D>("images/Bat");
            Vector2 batPos = new Vector2(Shared.stage.X / 2 - batTex.Width/2,
                Shared.stage.Y - batTex.Height);
            Vector2 batSpeed = new Vector2(4, 0);

            bat = new Bat(game, spriteBatch, batPos, batSpeed, batTex) ;
            this.Components.Add(bat);

        }
    }
}
