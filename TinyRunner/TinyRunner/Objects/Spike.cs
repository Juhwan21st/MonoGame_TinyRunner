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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D9;

namespace TinyRunner.Objects
{
    /// <summary>
    /// Represents a spike obstacle in the game.
    /// </summary>
    public class Spike : DrawableGameComponent
    {
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;

        // Reference to the game instance
        private Game g;

        /// <summary>
        /// Initializes a new instance of the Spike class.
        /// </summary>
        /// <param name="game">The Game object.</param>
        /// <param name="sb">The SpriteBatch object used for drawing sprites.</param>
        /// <param name="tex">The texture representing the spike.</param>
        /// <param name="speed">The speed of the spike</param>
        public Spike(Game game, SpriteBatch sb,
            Texture2D tex, Vector2 speed) : base(game)
        {
            this.g = game;
            this.sb = sb;
            this.tex = tex;
            this.position = new Vector2(Shared.stage.X, Shared.stage.Y - tex.Height - 60);
            this.speed = speed;
        }

        /// <summary>
        /// Updates the state of the spike.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            position += speed;
            if (position.X < -tex.Width)
            {
                position.X = Shared.stage.X;
                speed = Vector2.Zero;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the spike on the screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, position, Color.White);
            sb.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Gets the bounding rectangle of the spike for collision detection.
        /// </summary>
        /// <returns>A rectangle that bounds the spike.</returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}