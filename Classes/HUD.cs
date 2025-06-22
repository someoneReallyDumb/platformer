using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace platformer.Classes
{
    public class HUD
    {
        private PlayerHealth health;
        private int updateCount;
        private int seconds;
        private int minutes;
        private Label secondsLabel;
        private Label minutesLabel;
        public int Seconds
        {
            get => seconds;
        }
        public int Minutes
        {
            get => minutes;
        }
        public HUD()
        {
            //Vector2 position = new Vector2(20, 20);
            health = new PlayerHealth(new Vector2(12, 15));
            secondsLabel = new Label(new Vector2(37, 45), "", Color.White);
            minutesLabel = new Label(new Vector2(18, 45), "", Color.White);
        }
        public void LoadContent(GraphicsDevice graphics, ContentManager content)
        {
            health.LoadContent(content);

            secondsLabel.LoadContent(content);
            minutesLabel.LoadContent(content);
        }
        public void Update()
        {
            if (minutes != 99)
            {
                updateCount++;
                if (updateCount >=60)
                {
                    seconds++;
                    updateCount = 0;
                }
                if (seconds >= 60)
                {
                    minutes++;
                    seconds = 0;
                }
            }
            if (seconds <= 9)
            {
                secondsLabel.Text = ":0" + seconds;
            }
            else
            {
                secondsLabel.Text = ":" + seconds;
            }
            if (minutes == 0)
            {
                minutesLabel.Text = "00";
            }
            else if (minutes <= 9)
            {
                minutesLabel.Text = "0" + minutes;
            }
            else
            {
                minutesLabel.Text = minutes.ToString();
            } 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            health.Draw(spriteBatch);
            if (secondsLabel != null)
            {
                secondsLabel.Draw(spriteBatch);
            }
            if (minutesLabel != null)
            {
                minutesLabel.Draw(spriteBatch);
            }
        }
        public void OnPlayerTakeDamage(int health)
        {
            this.health.NumParts = health;
        }
        public void Reset(int h)
        {
            health.NumParts = h;
            updateCount = 0;
            seconds = 0;
            minutes = 0;
        }
    }
}
