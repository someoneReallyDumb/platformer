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
    public class Face
    {
        public Vector2 position;
        private Texture2D texture;
        private bool isLeft;
        #region textures
        private Texture2D defaultFace;
        private Texture2D defaultFaceLeft;
        private Texture2D shootFace;
        private Texture2D shootFaceLeft;
        #endregion
        public bool IsShooting { get; set; }
        public bool IsLeft 
        {
            set { isLeft = value; }
        }

        public Face(bool isLeft)
        {
            this.isLeft = isLeft;
        }
        public void LoadContent(ContentManager content)
        {
            defaultFace = content.Load<Texture2D>("default_face");
            texture = defaultFace;
            defaultFaceLeft = content.Load<Texture2D>("default_face_left");
            shootFace = content.Load<Texture2D>("shoot_face");
            shootFaceLeft = content.Load<Texture2D>("shoot_face_left");
        }
        public void Update()
        {
            TextureChange();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
        public void TextureChange()
        {
            if (!isLeft)
            {
                if (!IsShooting)
                {
                    texture = defaultFace;
                }
                else
                {
                    texture = shootFace;
                }
            }
            else
            {
                if (!IsShooting)
                {
                    texture = defaultFaceLeft;
                }
                else
                {
                    texture = shootFaceLeft;
                }
            }
        }
    }
}
