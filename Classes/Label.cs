using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace platformer.Classes
{
    public class Label
    {
        private Vector2 position;
        private SpriteFont spriteFont;
        private string text;
        private Color color;
        public Color Color
        {
            set { color = value; }
        }
        public Vector2 SizeText
        {
            get { return spriteFont.MeasureString(text); }
        }
        public Vector2 Position
        {
            set { position = value; }
        }
        public string Text
        {
            get => text;
            set => text = value;
        }
        public Label(Vector2 position, string text, Color color)
        {
            this.position = position;
            this.text = text;
            this.color = color;
            spriteFont = null;
        }
        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("GameFont");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, text, position, color);
        }
    }
}
