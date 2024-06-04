/*
 * Course: PROG2370-23F-Sec1
 * Programmed by : Juhwan Seo [8819123]
 * Revision history:
 *      1-Dec-2023: Project created
 *      5-Dec-2023: Designed forms
 *      10-Dec-2023: Debugging complete
 *      11-Dec-2023: Project complete
 */

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRunner.Scenes
{
    /// <summary>
    /// Represents a menu component that can be used in various scenes.
    /// Allows navigation through menu items using keyboard input.
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch sb;
        private SpriteFont regularFont, hilightFont;

        private List<string> menuItems;
        public int SelectedIndex { get; set; }
        private Vector2 position;
        private Color regularColor = Color.Black;
        private Color hilightColor = Color.Red;
        private KeyboardState oldState;

        /// <summary>
        /// Initializes a new instance of the MenuComponent class.
        /// </summary>
        /// <param name="game">The Game object.</param>
        /// <param name="sb">The SpriteBatch object used for drawing sprites.</param>
        /// <param name="regularFont">The font used for non-selected menu items.</param>
        /// <param name="hilightFont">The font used for the selected menu item.</param>
        /// <param name="menus">The array of menu items.</param>
        public MenuComponent(Game game, SpriteBatch sb,
            SpriteFont regularFont, SpriteFont hilightFont,
            string[] menus) : base(game)
        {
            this.sb = sb;
            this.regularFont = regularFont;
            this.hilightFont = hilightFont;
            menuItems = menus.ToList();
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
        }

        /// <summary>
        /// Updates the menu component, handling user input for menu navigation.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                SelectedIndex++;
                if (SelectedIndex == menuItems.Count)
                {
                    SelectedIndex = 0;
                }
            }

            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = menuItems.Count - 1;
                }
            }

            oldState = ks;

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the menu component on the screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 temPos = position;
            sb.Begin();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (SelectedIndex == i)
                {
                    sb.DrawString(hilightFont, menuItems[i], temPos, hilightColor);
                    temPos.Y += hilightFont.LineSpacing;
                }
                else
                {
                    sb.DrawString(regularFont, menuItems[i], temPos, regularColor);
                    temPos.Y += regularFont.LineSpacing;
                }
            }
            sb.End();

            base.Draw(gameTime);
        }
    }
}
