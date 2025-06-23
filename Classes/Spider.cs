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
        private Texture2D bulletTexture;
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
        private bool shot = false;
        private bool isAlive = true;
        private bool isHurt = false;

        private List<SpiderBullet> spiderBullets;
        public int Health { get; set; }
        public Rectangle HitBox
        {
            get { return hitbox; }
        }
        public bool IsAlive
        {
            get { return isAlive; }
        }
        public List<SpiderBullet> SpiderBullets
        {
            get { return spiderBullets; }
        }
        public bool IsHurt
        {
            get => isHurt;
            set => isHurt = value;
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
        public void Update(ContentManager content, SpriteBatch spriteBatch)
        {
            hitbox = new Rectangle((int)position.X + texture.Width / 4,
                (int)position.Y + texture.Height / 4, texture.Width - texture.Width / 4,
                texture.Height - texture.Height / 4);
            if (spiderBullets != null)
            {
                foreach (SpiderBullet bullet in spiderBullets)
                {
                    bullet.Update(widthScreen, heightScreen);
                }
            }
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
                shot = true;
            }
            
            if (!isMoving && stopTimer == stopMaxTime / 2)
            {
                if (shot)
                {
                    Shoot(content, spriteBatch);
                }
                shot = false;
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
            if (spiderBullets != null)
            {
                for (int i = 0; i < spiderBullets.Count; i++)
                {
                    if (spiderBullets[i].IsAlive == false)
                    {
                        spiderBullets.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("spider");
            bulletTexture = content.Load<Texture2D>("spiderBall");
            if (spiderBullets != null)
            {
                foreach (SpiderBullet bullet in spiderBullets)
                {
                    bullet.LoadContent(content);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            if (spiderBullets != null)
            {
                foreach (SpiderBullet bullet in spiderBullets)
                {
                    bullet.Draw(spriteBatch);
                }
            }
        }
        public void Shoot(ContentManager content, SpriteBatch spriteBatch)
        {
            SpiderBullet b1 = new SpiderBullet((int)position.X + texture.Width / 2 - 
                bulletTexture.Width / 2, (int)position.Y + texture.Height - 
                bulletTexture.Height, 0, 6);
            SpiderBullet b2 = new SpiderBullet((int)position.X + texture.Width / 2 - 
                bulletTexture.Width / 2, (int)position.Y + texture.Height -
                bulletTexture.Height, 3, 6);
            SpiderBullet b3 = new SpiderBullet((int)position.X + texture.Width / 2 - 
                bulletTexture.Width / 2, (int)position.Y + texture.Height -
                bulletTexture.Height, -3, 6);
            SpiderBullet b4 = new SpiderBullet((int)position.X + texture.Width / 2 -
                bulletTexture.Width / 2, (int)position.Y + texture.Height -
                bulletTexture.Height, 6, 6);
            SpiderBullet b5 = new SpiderBullet((int)position.X + texture.Width / 2 -
                bulletTexture.Width / 2, (int)position.Y + texture.Height -
                bulletTexture.Height, -6, 6);
            spiderBullets = [b1, b2, b3, b4, b5];
            if (spiderBullets != null)
            {
                foreach (SpiderBullet bullet in spiderBullets)
                {
                    bullet.LoadContent(content);
                }
            }
        }
        public void Damage()
        {
            Health--;
            if (Health <= 0)
            {
                isAlive = false;
            }
        }
        public void Reset()
        {
            position = new Vector2(widthScreen / 2, 5);
            hitbox = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            moveTimer = 0;
            stopTimer = 0;
            isMoving = true;
            shot = false;
            isAlive = true;
            Health = 5;
            if (spiderBullets != null)
            {
                spiderBullets.Clear();
            }
        }
    }
}
