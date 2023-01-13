using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace FlyingNerds
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;

        // Load fonts
        private SpriteFont regularFont, highlightFont;
        private Color regularColor = Color.Black;

        // Items in the start scene menu
        private List<string> menuItems;

        // Index of selected menu item
        public int selectedIndex { get; set; }
        private Vector2 position;

        // Previously clicked key 
        private KeyboardState prevState;

        /// <summary>
        /// Set fonts of the menu items
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="regularFont"></param>
        /// <param name="highlightFont"></param>
        /// <param name="menus"></param>
        public MenuComponent(Microsoft.Xna.Framework.Game game,
            SpriteBatch spriteBatch, 
            SpriteFont regularFont, 
            SpriteFont highlightFont, 
            string[] menus ) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.highlightFont = highlightFont;

            menuItems = menus.ToList<string>();

            // Position of the menu
            position = new Vector2(Shared.Stage.X / 3,
                Shared.Stage.Y / 3);
        }

        public override void Update(GameTime gameTime)
        {
            // Get index of the menu items when users go down 
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && prevState.IsKeyUp(Keys.Down) ||
                ks.IsKeyDown(Keys.S) && prevState.IsKeyUp(Keys.S))
            {
                selectedIndex++;
                if(selectedIndex == menuItems.Count)
                    // Reset the index
                    selectedIndex = 0;
            }
            // Get index of the menu items when users go up
            if (ks.IsKeyDown(Keys.Up) && prevState.IsKeyUp(Keys.Up) ||
                ks.IsKeyDown(Keys.W) && prevState.IsKeyUp(Keys.W))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                    // Go to the last item
                    selectedIndex = menuItems.Count - 1;
            }

            prevState = ks;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            // Display menu items
            Vector2 tempPos = position;
            for (int i = 0; i < menuItems.Count; i++)
            {
                if(selectedIndex == i)
                {
                    spriteBatch.DrawString(highlightFont, menuItems[i], tempPos, regularColor);
                    tempPos.Y += highlightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }          

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
