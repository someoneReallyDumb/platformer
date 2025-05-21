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
    public class Platform
    {
        public Vector2 position;
        private int height;
        private int width;
        private Texture2D texture;
        private Rectangle location;

        public Rectangle Location
        {
            get { return location; }
            set { location = value; }
        }
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public Platform( int x, int y) 
        {
            position = new Vector2(x, y);
            texture = null;
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("platform");
            width = texture.Width;
            height = texture.Height;

            location = new Rectangle((int)position.X, (int)position.Y, 
                texture.Width, texture.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
       
    }
}
