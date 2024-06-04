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

namespace TinyRunner.Logics
{
    /// <summary>
    /// Creates a scrolling background effect for the game.
    /// </summary>
    internal class ScrollingBackground : DrawableGameComponent
    {
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position1, position2;
        private Vector2 speed;

        /// <summary>
        /// Initializes a new instance of the ScrollingBackground class.
        /// </summary>
        /// <param name="game">The Game object.</param>
        /// <param name="sb">The SpriteBatch object used for drawing sprites.</param>
        /// <param name="tex">The texture for the background.</param>
        /// <param name="position">The initial position of the background.</param>
        /// <param name="speed">The speed at which the background scrolls.</param>
        public ScrollingBackground(Game game, SpriteBatch sb, Texture2D tex, Vector2 position, Vector2 speed) : base(game)
        {
            this.sb = sb;
            this.tex = tex;
            position1 = position;
            position2 = new Vector2(position1.X + tex.Width, position1.Y);
            this.speed = speed;
        }

        /// <summary>
        /// Updates the scrolling position of the background.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            position1 -= speed;
            position2 -= speed;

            if (position1.X < -tex.Width)
            {
                position1.X = position2.X + tex.Width;
            }

            if (position2.X < -tex.Width)
            {
                position2.X = position1.X + tex.Width;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the scrolling background on the screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, position1, Color.White);
            sb.Draw(tex, position2, Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
