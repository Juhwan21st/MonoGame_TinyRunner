/*
 * Course: PROG2370-23F-Sec1
 * Programmed by : Juhwan Seo [8819123]
 * Revision history:
 *      1-Dec-2023: Project created
 *      5-Dec-2023: Designed forms
 *      10-Dec-2023: Debugging complete
 *      11-Dec-2023: Project complete
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TinyRunner.Scenes
{
    /// <summary>
    /// Represents the start scene of the game.
    /// This scene includes the main menu of the game.
    /// </summary>
    public class StartScene : GameScene
    {
        private MenuComponent menu;

        /// <summary>
        /// Gets or sets the MenuComponent.
        /// </summary>
        public MenuComponent Menu { get => menu; set => menu = value; }

        /// <summary>
        /// Initializes a new instance of the StartScene class.
        /// </summary>
        /// <param name="game">The Game object.</param>
        public StartScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            string[] menuItems = { "Start game", "Help", "High Score", "About", "Quit" };

            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/RegularFont");
            SpriteFont hilightFont = g.Content.Load<SpriteFont>("fonts/HilightFont");

            Menu = new MenuComponent(game, g._spriteBatch, regularFont, hilightFont, menuItems);
            Components.Add(Menu);
        }
    }
}
