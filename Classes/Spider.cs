using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
namespace platformer.Classes
{
    public class Spider
    {
        private Texture2D texture;
        private Vector2 position;
        private float speedX;
        private float speedY;
        private Rectangle hitbox;
        private int widthScreen;
        private int heightScreen;
        private int moveTimer = 0;
        private int moveMaxTime = 150;
        private int stopTimer = 0;
        private int stopMaxTime = 50;
        private bool isMoving = true;
        public int Health { get; set; }
        public Rectangle HitBox
        {
            get { return hitbox; }
        }
        public Spider(int widthScreen, int heightScreen)
        {
            Health = 5;
            speedX = 5; 
            speedY = 5;
            position = new Vector2(widthScreen / 2, 5);
            hitbox = new Rectangle((int)position.X, (int)position.Y, 0,0);
            this.widthScreen = widthScreen;
            this.heightScreen = heightScreen;
        }
        public void Update()
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (isMoving && moveTimer < moveMaxTime)
            {
                moveTimer++;
                position.X += speedX;
                position.Y += speedY;
                stopTimer = 0;
            }
            else
            {
                moveTimer = 0;
                isMoving = false;
            }
            if (!isMoving && stopTimer < stopMaxTime)
            {
                stopTimer++;
                moveTimer = 0;
            }
            else
            {
                isMoving = true;
            }
            if (position.X < 0)
            {
                speedX = -speedX;
            }
            if (position.Y < 0)
            {
                speedY = -speedY;
            }
            if (position.X >= widthScreen - texture.Width)
            {
                speedX = -speedX;
            }
            if (position.Y >= heightScreen - texture.Height)
            {
                speedY = -speedY;
            }
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("spider");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, Color.White);
        }
    }
}
