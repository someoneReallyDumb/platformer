using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace platformer.Classes
{
    public class SpiderBullet : Bullet
    {
        private int x;
        private int y;
        public SpiderBullet(int x, int y, float speedX, float speedY) : base()
        {
            this.x = x;
            this.y = y;
            this.speedX = speedX;
            this.speedY = speedY;
        }
        public override void Update(int widthScreen, int heightScreen)
        {
            base.Update(widthScreen, heightScreen);
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("spiderBall");
            destinationRectangle = new Rectangle(x, y, texture.Width, texture.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, DestinationRectangle, Color.White);
        }
    }
}
