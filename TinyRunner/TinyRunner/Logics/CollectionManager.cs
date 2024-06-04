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
    /// Manages collecting detection between the runner and fruit.
    /// </summary>
    internal class CollectionManager : GameComponent
    {
        private Runner runner;
        private List<Fruit> fruits;
        private SoundEffect eatSound;
		public bool isCollided { get; set; }

		/// <summary>
		/// Initializes a new instance of the CollectionManager class.
		/// </summary>
		/// <param name="game">The Game object.</param>
		/// <param name="runner">The Runner object to check for collecting.</param>
		/// <param name="fruits">The list of Fruit objects to check for collisions.</param>
		public CollectionManager(Game game, Runner runner, List<Fruit> fruits) : base(game)
        {
            this.runner = runner;
            this.fruits = fruits;
			isCollided = false;

			// Sound effect for collecting
			eatSound = game.Content.Load<SoundEffect>("sounds/Collect");
        }

        /// <summary>
        /// Updates the collection state of the game.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
			Rectangle runnerRect = runner.getBounds();
			for (int i = fruits.Count - 1; i >= 0; i--)
			{
				Rectangle fruitRect = fruits[i].getBounds();
				if (runnerRect.Intersects(fruitRect))
				{
					eatSound.Play();
					isCollided = true;
					// Remove the fruit from the list and game components
					fruits[i].Visible = false;
					Game.Components.Remove(fruits[i]);
					fruits.RemoveAt(i);
				}
			}

			base.Update(gameTime);
        }
    }
}
