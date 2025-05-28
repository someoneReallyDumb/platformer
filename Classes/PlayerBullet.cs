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
    public class PlayerBullet : Bullet
    {
        private bool isLeft;
        private Texture2D textureRight;
        private Texture2D textureLeft;
        public bool IsLeft
        {
            get { return isLeft; }
            set { isLeft = value; }
        }
        public PlayerBullet() : base()
        {
            speedX = 15;
            speedY = 0;
        }
        public override void Update(int widthScreen, int heightScreen)
        {
            if (isLeft)
            {
                texture = textureLeft;
                destinationRectangle.X -= speedX;
            }
            else
            {
                texture = textureRight;
                destinationRectangle.X += speedX;
            }
        }
        public void LoadContent(ContentManager content)
        {
            textureRight = content.Load<Texture2D>("bullet");
            textureLeft = content.Load<Texture2D>("bullet_left");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
