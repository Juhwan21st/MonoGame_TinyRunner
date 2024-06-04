/*
 * Course: PROG2370-23F-Sec1
 * Programmed by : Juhwan Seo [8819123]
 * Revision history:
 *      1-Dec-2023: Project created
 *      5-Dec-2023: Designed forms
 *      10-Dec-2023: Debugging complete
 *      11-Dec-2023: Project complete
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRunner.Scenes
{
    /// <summary>
    /// Represents the Help scene in the game.
    /// Displays information about how to play the game,
    /// including required key presses and/or mouse clicks.
    /// </summary>
    public class HelpScene : GameScene
    {
        private Texture2D tex;
        private SpriteBatch sb;

        /// <summary>
        /// Initializes a new instance of the HelpScene class.
        /// </summary>
        /// <param name="game">The Game object.</param>
        public HelpScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            sb = g._spriteBatch;
            tex = game.Content.Load<Texture2D>("images/HelpPage_Background");
        }

        /// <summary>
        /// Draws the Help scene on the screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, Vector2.Zero, Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
