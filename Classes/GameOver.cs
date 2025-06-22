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
        private Label timeLabel;
        private int widthScreen;
        private int heightScreen;
        private bool victory;
        public bool Victory
        {
            set { victory = value; }
        }
        public GameOver(int widthScreen, int heightScreen)
        {
            label = new Label(new Vector2(250, 200), "", Color.White);
                //time
            timeLabel = new Label(new Vector2(250, 220), "", Color.Black);
            lblInstructions = new Label(new Vector2(250, 240),
                "Press Enter to continue", Color.DarkGreen);
            this.widthScreen = widthScreen;
            this.heightScreen = heightScreen;
        }
        public void LoadContent(ContentManager content)
        {
            label.LoadContent(content);
            lblInstructions.LoadContent(content);
            timeLabel.LoadContent(content);
            label.Position = new Vector2(widthScreen / 2 + label.SizeText.X / 2,
                heightScreen / 2 - label.SizeText.Y / 2 - 20);
            timeLabel.Position = new Vector2(widthScreen / 2 + label.SizeText.X / 2,
                heightScreen / 2 - label.SizeText.Y / 2);
            lblInstructions.Position = new Vector2(widthScreen / 2 + label.SizeText.X / 2,
                heightScreen / 2 - label.SizeText.Y / 2 + 20);
        }
        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (victory)
            {
                label.Text = "YOU WIN!";
                label.Color = Color.DarkGreen;
            }
            else
            {
                label.Text = "YOU DIED";
                label.Color = Color.Red;
            }
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                Game1.gameMode = GameMode.Menu;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            label.Draw(spriteBatch);
            timeLabel.Draw(spriteBatch);
            lblInstructions.Draw(spriteBatch);
        }
        public void TimeLabel(int min, int sec)
        {
            timeLabel.Text = "Time: " + min / 10 + min % 10 + ":" + sec / 10 + sec % 10;
        }
    }
}
