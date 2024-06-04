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
    /// Represents the About scene in the game.
    /// Displays the developers' information.
    /// </summary>
    public class AboutScene : GameScene
    {
        private Texture2D tex;
        private SpriteBatch sb;

        /// <summary>
        /// Initializes a new instance of the AboutScene class.
        /// </summary>
        /// <param name="game">The Game object.</param>
        public AboutScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            sb = g._spriteBatch;
            tex = game.Content.Load<Texture2D>("images/AboutPage_PurpleBoard");
        }

        /// <summary>
        /// Draws the About scene on the screen.
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
