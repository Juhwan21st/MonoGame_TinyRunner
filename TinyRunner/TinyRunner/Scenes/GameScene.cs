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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRunner.Scenes
{
    /// <summary>
    /// Represents a base class for all game scenes.
    /// Provides methods to show or hide the scene and manage its components.
    /// </summary>
    public abstract class GameScene : DrawableGameComponent
    {
        /// <summary>
        /// Gets or sets the list of components in the scene.
        /// </summary>
        public List<GameComponent> Components { get; set; }

        /// <summary>
        /// Hides the scene and disables updates and drawing.
        /// </summary>
        public virtual void hide()
        {
            Visible = false;
            Enabled = false;
        }

        /// <summary>
        /// Shows the scene and enables updates and drawing.
        /// </summary>
        public virtual void show()
        {
            Enabled = true;
            Visible = true;
        }

        /// <summary>
        /// Initializes a new instance of the GameScene class.
        /// </summary>
        /// <param name="game">The Game object.</param>
        protected GameScene(Game game) : base(game)
        {
            Components = new List<GameComponent>();
            hide();
        }

        /// <summary>
        /// Updates the game scene and all its enabled components.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game scene and all its visible components.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }
    }
}
