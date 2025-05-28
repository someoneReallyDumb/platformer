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
    public abstract class Bullet
    {
        protected int speedX;
        protected int speedY;
        protected Rectangle destinationRectangle;
        protected Texture2D texture;
        protected bool isAlive;
        public bool IsAlive 
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
            
        public Vector2 Position
        {
            get 
            { 
                return new Vector2(destinationRectangle.X, destinationRectangle.Y);
            } 
            set 
            { 
                destinationRectangle.X = (int)value.X;
                destinationRectangle.Y = (int)value.Y;
            }
        }
        public Bullet()
        {
            texture = null;
            isAlive = true;
            destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, 0, 0);
        }
        public virtual void Update(int widthScreen, int heightScreen)
        {

            destinationRectangle.X += speedX;
            destinationRectangle.Y += speedY;
            if (destinationRectangle.X < 0)
            {
                isAlive = false;
            }
            if (destinationRectangle.Y < 0)
            {
                isAlive = false;
            }
            if (destinationRectangle.X >= widthScreen)
            {
                isAlive = false;
            }
            if (destinationRectangle.Y >= heightScreen)
            {
                isAlive = false;
            }
        }
    }
}
