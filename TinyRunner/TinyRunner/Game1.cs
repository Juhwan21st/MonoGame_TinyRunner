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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TinyRunner.Scenes;

namespace TinyRunner
{
    /// <summary>
    /// This is the main class for this game.
    /// It manages game scenes and musics.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        //declare all scenes
        private StartScene startScene;
        private ActionScene actionScene;
        private HelpScene helpScene;
        private HighScoreScene highScoreScene;
        private AboutScene aboutScene;
        private EndingScene endingScene;

        // declare all background musics
        private Song startSceneBackgroundMusic;
        private Song backgroundMusic;
        private Song currentMusic;

        private Texture2D backgroundTitleTex;

        /// <summary>
        /// Constructor for the Game1 class.
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load all of the content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundTitleTex = this.Content.Load<Texture2D>("images/StartPage_Title");

            //instantiate all scenes here
            startScene = new StartScene(this);
            this.Components.Add(startScene);

            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            highScoreScene = new HighScoreScene(this);
            this.Components.Add(highScoreScene);

            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            endingScene = new EndingScene(this);
            this.Components.Add(endingScene);


            //make ONLY startscene active
            startScene.show();

            // start scene background music
            startSceneBackgroundMusic = this.Content.Load<Song>("sounds/StartSceneBackgroundMusic");
            MediaPlayer.IsRepeating = true;

            // background music
            backgroundMusic = this.Content.Load<Song>("sounds/BackgroundMusic");
        }

        /// <summary>
        /// Allows the game to run its logic
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    actionScene.show();
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    helpScene.show();
                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    highScoreScene.LoadHighScores();
					highScoreScene.show();
                }
                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    aboutScene.show();
                }
                else if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            if (actionScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    actionScene.hide();
                    startScene.show();

                }
            }
            if (helpScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    helpScene.hide();
                    startScene.show();
                }
            }
            if (highScoreScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    highScoreScene.hide();
                    startScene.show();
                }
            }
            if (aboutScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    aboutScene.hide();
                    startScene.show();
                }
            }
            if (aboutScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    aboutScene.hide();
                    startScene.show();
                }
            }
            if (endingScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
            }

            UpdateMusic();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTitleTex, Vector2.Zero, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Updates the background music based on the current scene.
        /// </summary>
        private void UpdateMusic()
        {
            if (startScene.Enabled && currentMusic != startSceneBackgroundMusic)
            {
                MediaPlayer.Stop();
                currentMusic = startSceneBackgroundMusic;
                MediaPlayer.Play(currentMusic);
            }
            else if (actionScene.Enabled && currentMusic != backgroundMusic)
            {
                MediaPlayer.Stop();
                currentMusic = backgroundMusic;
                MediaPlayer.Play(currentMusic);
            }
        }

        /// <summary>
        /// Ends the action scene game and transitions to the ending scene.
        /// </summary>
        /// <param name="score">The final score achieved by the player.</param>
        public void EndActionSceneGame(int score)
        {
            Components.Remove(actionScene);
            endingScene.SetFinalScore(score);
            endingScene.show();
            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);
        }

		/// <summary>
		/// Handles the click of the back button in the ending scene.
		/// </summary>
		public void ClickBackButton()
        {
            endingScene.hide();
			Components.Remove(endingScene);
			startScene.show();
			endingScene = new EndingScene(this);
			this.Components.Add(endingScene);
		}
    }
}