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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TinyRunner.Objects;
using static System.Net.Mime.MediaTypeNames;
using TinyRunner.Logics;

namespace TinyRunner.Scenes
{
    /// <summary>
    /// Represents the main gameplay scene(level).
    /// </summary>
    public class ActionScene : GameScene
    {
        // for runner character
        private Runner runner;
        private const int FRAMES = 12;

        // for spikes
        private List<Spike> spikes;
        private double lastSpikeSpawnTime = 0;
		private Random random = new Random();
		private double nextSpikeSpawnTime;
		private Texture2D spikeTexture;

		// for fruits
		private List<Fruit> fruits;
		private double lastFruitSpawnTime = 0;
		private Random randomF = new Random();
		private double nextFruitSpawnTime;
		private Texture2D fruitTexture;
        private int numberOfCollection = 0;
        private int collectionPoints = 10000;

		private double currentTime;

        // collision logic
        private CollisionManager collisionManager;

		// collection logic
		private CollectionManager collectionManager;

		// for score
		private TimeSpan startTime;
        private int score;
        private SpriteFont scoreFont;
        private string scoreText;
        private SpriteBatch sb;
        private Vector2 position;

        // game status
        private bool gameOver;

        private Game1 g;

        /// <summary>
        /// Initializes a new instance of the ActionScene class.
        /// </summary>
        /// <param name="game">The Game object.</param>
        public ActionScene(Game game) : base(game)
        {
            g = (Game1)game;
            sb = g._spriteBatch;

            Texture2D skyTex = game.Content.Load<Texture2D>("images/ActionPage_Sky");
            Vector2 skySpeed = new Vector2(1, 0);

            Texture2D groundTex = game.Content.Load<Texture2D>("images/ActionPage_Ground");
            Vector2 groundSpeed = new Vector2(3, 0);

            generateBackgroundImages(skyTex, skySpeed);

            generateBackgroundImages(groundTex, groundSpeed);


            Texture2D runnerTex = game.Content.Load<Texture2D>("images/Run (64x64)");
            Vector2 runnerPosition = new Vector2(Shared.stage.X / 2 - runnerTex.Width / FRAMES / 2, Shared.stage.Y - runnerTex.Height - 60);
            Vector2 runnerSpeed = new Vector2(5, 0);
            runner = new Runner(game, g._spriteBatch, runnerTex, runnerPosition, 3);
            Components.Add(runner);

            spikes = new List<Spike>();
            
            collisionManager = new CollisionManager(game, runner, spikes);
            Components.Add(collisionManager);
            
            spikeTexture = g.Content.Load<Texture2D>("images/Spike");

			fruits = new List<Fruit>();

			collectionManager = new CollectionManager(game, runner, fruits);
			Components.Add(collectionManager);
			fruitTexture = g.Content.Load<Texture2D>("images/Apple");

			startTime = TimeSpan.Zero;
            score = 0;
            scoreFont = game.Content.Load<SpriteFont>("fonts/RegularFont");

        }

        /// <summary>
        /// Updates the game state, including spawning spikes/fruits and tracking score.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (!gameOver)
            {
                currentTime = gameTime.TotalGameTime.TotalSeconds;
				if (currentTime >= nextSpikeSpawnTime)
				{
					Vector2 spikeSpeed = new Vector2(-5, 0);
					Spike newSpike = new Spike(g, g._spriteBatch, spikeTexture, spikeSpeed);
					spikes.Add(newSpike);
					Components.Add(newSpike);

					// calculate the next spawn time using a random interval
					nextSpikeSpawnTime = currentTime + random.Next(1, 4);
				}

				if (currentTime >= nextFruitSpawnTime)
				{
					Vector2 fruitSpeed = new Vector2(-7, 0);
					Fruit newFruit = new Fruit(g, g._spriteBatch, fruitTexture, fruitSpeed, 4);
					fruits.Add(newFruit);
					Components.Add(newFruit);

					// calculate the next spawn time using a random interval
					nextFruitSpawnTime = currentTime + randomF.Next(3, 7);
				}

				if (startTime == TimeSpan.Zero)
                {
                    startTime = gameTime.TotalGameTime;
                }
                else
                {
                    score = (int)(gameTime.TotalGameTime - startTime).TotalMilliseconds + numberOfCollection * collectionPoints;
                }

                scoreText = $"Score: {score}";

                if (collisionManager.isCollided)
                {
                    gameOver = true;
                    g.EndActionSceneGame(score);
                }
				if (collectionManager.isCollided)
				{
					//Components.Remove(newFruit);
					numberOfCollection++;
                    collectionManager.isCollided = false;
				}
			}

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game state to the screen, including the score.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            sb.Begin();
            position = new Vector2(10, 10);
            sb.DrawString(scoreFont, scoreText, position, Color.Purple);
            sb.End();
        }

        /// <summary>
        /// Generates scrolling background images for the scene.
        /// </summary>
        /// <param name="backgroundTex">The texture for the background.</param>
        /// <param name="backgroundSpeed">The speed at which the background scrolls.</param>
        private void generateBackgroundImages(Texture2D backgroundTex, Vector2 backgroundSpeed)
        {
            Rectangle srcRect = new Rectangle(0, 0, backgroundTex.Width, backgroundTex.Height);
            Vector2 pos = new Vector2(0, Shared.stage.Y - srcRect.Height);

            ScrollingBackground sb1 = new ScrollingBackground(g, g._spriteBatch, backgroundTex, pos, backgroundSpeed);

            this.Components.Add(sb1);
        }
    }
}
