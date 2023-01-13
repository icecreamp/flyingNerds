using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FlyingNerds
{
    public abstract class GameScene : DrawableGameComponent
    {
        public List<GameComponent> Components { get; set; }

        protected SpriteBatch spriteBatch;
        protected Game g;

        protected GameScene(Microsoft.Xna.Framework.Game game) : base(game)
        {
            g = (Game)game;
            this.spriteBatch = g._spriteBatch;

            Components = new List<GameComponent>();
            Hide();
        }

        public virtual void Show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        public virtual void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        public override void Update(GameTime gameTime)
        {
            foreach(GameComponent component in Components)
                if(component.Enabled)
                    component.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (DrawableGameComponent component in Components)
                if (component.Visible)
                    component.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
