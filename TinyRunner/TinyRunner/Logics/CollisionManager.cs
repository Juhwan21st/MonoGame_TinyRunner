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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyRunner.Objects;

namespace TinyRunner.Logics
{
    /// <summary>
    /// Manages collision detection between the runner and spikes.
    /// </summary>
    internal class CollisionManager : GameComponent
    {
        private Runner runner;
        private List<Spike> spikes;
        private SoundEffect hitSound;
        public bool isCollided = false;

        /// <summary>
        /// Initializes a new instance of the CollisionManager class.
        /// </summary>
        /// <param name="game">The Game object.</param>
        /// <param name="runner">The Runner object to check for collisions.</param>
        /// <param name="spikes">The list of Spike objects to check for collisions.</param>
        public CollisionManager(Game game, Runner runner, List<Spike> spikes) : base(game)
        {
            this.runner = runner;
            this.spikes = spikes;

            // Sound effect for collision
            hitSound = game.Content.Load<SoundEffect>("sounds/Hit");
        }

        /// <summary>
        /// Updates the collision state of the game.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            Rectangle runnerRect = runner.getBounds();
            foreach (var spike in spikes)
            {
                Rectangle spikeRect = spike.getBounds();
                if (runnerRect.Intersects(spikeRect))
                {
                    hitSound.Play();
                    isCollided = true;
                }
            }

            base.Update(gameTime);
        }
    }
}
