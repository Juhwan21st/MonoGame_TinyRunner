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
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyRunner.Objects;
using System.IO;

namespace TinyRunner.Scenes
{
	/// <summary>
	/// Represents the ending scene of the game.
	/// Displays the final score and provides an option to exit the game.
	/// This scene allows the player to enter their name after completing the game
	/// which are then saved along with the final score in a text file.
	/// </summary>
	internal class EndingScene : GameScene
    {
        private Texture2D tex;
        private SpriteBatch sb;
        private SpriteFont font;
        private Vector2 position;
        private Color scoreColor = Color.Purple;

        private int finalScore;
        private string scoreText;

        private Button backButton;

        private Game1 g;

        // variables for the user name
		private string playerName = "";
		private bool nameEntered = false;
		private KeyboardState oldState;

		/// <summary>
		/// Initializes a new instance of the EndingScene class.
		/// </summary>
		/// <param name="game">The Game object.</param>
		public EndingScene(Game game) : base(game)
        {
            this.g = (Game1)game;
            sb = g._spriteBatch;
            tex = game.Content.Load<Texture2D>("images/EndingPage_Background");
            font = game.Content.Load<SpriteFont>("fonts/FinalFont");
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);

            Texture2D buttonTex = game.Content.Load<Texture2D>("images/ArrowButton");
            Vector2 buttonPosition = new Vector2(10, 10);
            int buttonDelay = 10;
            backButton = new Button(game, g._spriteBatch, buttonTex, buttonPosition, buttonDelay);
            Components.Add(backButton);
        }

        /// <summary>
        /// Sets the final score to be displayed in the ending scene.
        /// </summary>
        /// <param name="finalScore">The final score achieved by the player.</param>
        public void SetFinalScore(int score)
        {
            finalScore = score;
            scoreText = $"Final Score: {finalScore}\nTo Exit, press the esc";
        }

		/// <summary>
		/// Saves the player's name and final score to the gameScores.txt file.
		/// </summary>
		private void SaveScore()
		{
			// save user namd and final score into gameScores.txt 
			string filePath = Path.Combine(Environment.CurrentDirectory, "gameScores.txt");
			string userScore= $"{playerName}|{finalScore}\n";
			File.AppendAllText(filePath, userScore);
		}

		/// <summary>
		/// Updates the ending scene, handling user input for name entry and back button interaction.
		/// The name entry allows for a maximum of 6 characters, accepting only uppercase letters.
		/// The back button returns the player to the previous scene.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
        {
            backButton.Update(gameTime);
            MouseState mouseState = Mouse.GetState();
            if (backButton.IsClicked(mouseState))
            {
                g.ClickBackButton();
            }

			KeyboardState ks = Keyboard.GetState();
			if (!nameEntered)
			{
				if (ks.IsKeyDown(Keys.Back) && oldState.IsKeyUp(Keys.Back) && playerName.Length > 0)
				{
					playerName = playerName.Remove(playerName.Length - 1);
				}
				else if (ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter) && playerName.Length > 2)
				{
					nameEntered = true;
					SaveScore();
				}
				else
				{
					Keys[] keys = ks.GetPressedKeys();
					foreach (var key in keys)
					{
						if (playerName.Length < 6 && key >= Keys.A && key <= Keys.Z && oldState.IsKeyUp(key))
						{
							playerName += key.ToString();
						}
					}
				}
			}
			oldState = ks;
			base.Update(gameTime);
        }

		/// <summary>
		/// Draws the ending scene, displaying the final score, player's name input, and back button.
		/// The name input is displayed with a prompt, and the back button is drawn as an arrow.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, Vector2.Zero, Color.White);
            sb.DrawString(font, scoreText, position, scoreColor);
			if (!nameEntered)
			{
				string inputText = $"Enter Your Name: {playerName}";
				sb.DrawString(font, inputText, new Vector2(100, 150), Color.Black);
			}
			sb.End();

            base.Draw(gameTime);
        }
    }
}
