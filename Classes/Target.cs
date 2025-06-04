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
    public class Target
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle hitBox;
        private int heightScreen;
        private int widthScreen;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Rectangle HitBox
        {
            get { return hitBox; }
            //set { hitBox = value; }
        }
        public Target(int widthScreen, int heightScreen)
        {
            texture = null;
            position = Vector2.Zero;
            hitBox = new Rectangle();
            this.heightScreen = heightScreen;
            this.widthScreen = widthScreen;
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("target");
            ChangePosition();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitBox, Color.White);
        }
        public void ChangePosition()
        {
            Random random = new Random();
            position.X = random.Next(widthScreen - texture.Width);
            position.Y = random.Next(heightScreen - texture.Height);
            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
    }
}
