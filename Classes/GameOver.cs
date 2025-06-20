using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using platformer.Classes;
using platformer;

namespace platformer.Classes
{
    public class GameOver
    {
        private Label label;
        private Label lblInstructions;
        private int widthScreen;
        private int heightScreen;
        public GameOver(int widthScreen, int heightScreen)
        {
            label = new Label(new Vector2(250, 200), "GAME OVER", Color.Red);
            lblInstructions = new Label(new Vector2(250, 240),
                "Press Enter to continue", Color.DarkGreen);
            this.widthScreen = widthScreen;
            this.heightScreen = heightScreen;
        }
        public void LoadContent(ContentManager content)
        {
            label.LoadContent(content);
            lblInstructions.LoadContent(content);
            label.Position = new Vector2(widthScreen / 2 - label.SizeText.X / 2,
                heightScreen / 2 - label.SizeText.Y / 2 - 20);
            lblInstructions.Position = new Vector2(widthScreen / 2 - label.SizeText.X / 2,
                heightScreen / 2 - label.SizeText.Y / 2 + 20);
        }
        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                Game1.gameMode = GameMode.Menu;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            label.Draw(spriteBatch);
            lblInstructions.Draw(spriteBatch);
        }
    }
}
