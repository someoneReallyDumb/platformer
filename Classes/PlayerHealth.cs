using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PlayerHealth
    {
        private Texture2D texture;
        private Vector2 position;
        private int widthPart;
        private int numParts;
        private int height;
        public int NumParts
        {
            get => numParts;
            set => numParts = value;
        }
        public Rectangle DestinationRectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y,
                widthPart * numParts, height);
            }
        }
        public PlayerHealth(Vector2 position, int numParts, int width, int height)
        {
            this.position = position;
            this.numParts = numParts;
            this.height = height;
            this.widthPart = width / numParts;
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("heart");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, DestinationRectangle, Color.White);
        }
    }
}
