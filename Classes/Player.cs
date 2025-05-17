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
    public class Player
    {
        public Vector2 position;
        private Texture2D texture;
        private Texture2D defaultTextureRight;
        private Texture2D defaultTextureLeft;
        private float speed = 5;
        private float fallSpeed = 5;
        private float jumpSpeed = 10;
        private float jumpHeight = 200;
        private float startHeight = 0;
        //private double time = 0.0d;
        //private double duration = 400.0d;
        //private double jumpCount = 0;
        private bool isLeft = false;
        //private bool jumped = false;
        private int textureRunNum = 0;
        private Texture2D[] texturesRunRight = new Texture2D[4];
        private Texture2D[] texturesRunLeft = new Texture2D[4];
        private Texture2D[] texturesJumpRight = new Texture2D[4];
        private Texture2D[] texturesJumpLeft = new Texture2D[4];
        private int animSpeed = 7;
        private Rectangle upCollision;
        private Rectangle downCollision;
        private Rectangle leftCollision;
        private Rectangle rightCollision;

        public Rectangle UpCollision
        {
            get { return upCollision; }
        }
        public Rectangle DownCollision
        {
            get { return downCollision; }
        }
        public Rectangle LeftCollision
        {
            get { return leftCollision; }
        }
        public Rectangle RightCollision
        {
            get { return rightCollision; }
        }
        //public Vector2 Position { get; set; }
        public Player()
        {
            position = new Vector2(0,0);
            texture = defaultTextureRight;
            upCollision = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            downCollision = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            rightCollision = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            leftCollision = new Rectangle((int)position.X, (int)position.Y, 0, 0);
        }
        public void Update(int widthScreen, int heightScreen, ContentManager content,
            GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (isLeft)
            { texture = defaultTextureLeft; }
            else { texture = defaultTextureRight; }
            #region Movement
            if (keyboard.IsKeyDown(Keys.S))
            {
                if (isLeft)
                { texture = texturesJumpLeft[0]; }
                else { texture = texturesJumpRight[0]; }
            }
            if (keyboard.IsKeyDown(Keys.Space))
            {
                //time += gameTime.ElapsedGameTime.TotalMilliseconds
                    //- duration * jumpCount;  //надо с этим что-то сделать
                //if (time <= duration)
                //{
                    //texture = texturesJumpRight[1];
                //}
                //if (time > duration)
                //{
                Jump(heightScreen);
                //}
            }
            if (textureRunNum > animSpeed * 3)
            {
                textureRunNum = 0;
            }
            if (keyboard.IsKeyDown(Keys.A) && !keyboard.IsKeyDown(Keys.D) &&
                !keyboard.IsKeyDown(Keys.S))
            {
                position.X -= speed;
                isLeft = true;
                if (texture != null)
                {
                    if (position.Y == heightScreen - texture.Height)   //поменять
                    {
                        texture = texturesRunLeft[textureRunNum / animSpeed];
                        textureRunNum++;
                    }
                }    
            }
            if (textureRunNum > animSpeed*3)
            {
                textureRunNum = 0;
            }
            if (keyboard.IsKeyDown(Keys.D) && !keyboard.IsKeyDown(Keys.A) &&
                !keyboard.IsKeyDown(Keys.S))
            {
                position.X += speed;
                isLeft = false;
                if (texture != null)
                {
                    if (position.Y == heightScreen - texture.Height)   //поменять
                    {
                        texture = texturesRunRight[textureRunNum / animSpeed];
                        textureRunNum++;
                    }
                }       
            }
            position.Y += fallSpeed;
            #endregion
            #region Bounds
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            if (position.X > widthScreen - texture.Width)
            {
                position.X = widthScreen - texture.Width;
            }
            if (position.Y > heightScreen - texture.Height)
            {
                position.Y = heightScreen - texture.Height;
            }
            #endregion
            upCollision = new Rectangle((int)position.X, (int)position.Y,
                                       texture.Width, 20);
            downCollision = new Rectangle((int)position.X, (int)position.Y,
                                       texture.Width, 20);
        }
        private void Jump(float heightScreen)
        {
           
            if (startHeight <= jumpHeight)
            {
                if (isLeft)
                { texture = texturesJumpLeft[2]; }
                else
                { texture = texturesJumpRight[2]; }
                    position.Y -= jumpSpeed;
                startHeight += jumpSpeed;
            }
            else
            {
                if (isLeft)
                { texture = texturesJumpLeft[3]; }
                else
                { texture = texturesJumpRight[3]; }
                if (position.Y == heightScreen - texture.Height)  //!!!
                {
                    startHeight = 0;
                    //jumpCount++;
                }  
            }
        }
        public void LoadContent(ContentManager content)
        {
            defaultTextureRight = content.Load<Texture2D>("default");
            defaultTextureLeft = content.Load<Texture2D>("default_left");
            LoadRunTextures(content);
        }
        public void LoadRunTextures(ContentManager content)
        {
            texturesRunRight[0] = content.Load<Texture2D>("run1");
            texturesRunLeft[0] = content.Load<Texture2D>("run1_left");
            texturesRunRight[1] = content.Load<Texture2D>("run2");
            texturesRunLeft[1] = content.Load<Texture2D>("run2_left");
            texturesRunRight[2] = content.Load<Texture2D>("run3");
            texturesRunLeft[2] = content.Load<Texture2D>("run3_left");
            texturesRunRight[3] = content.Load<Texture2D>("run4");
            texturesRunLeft[3] = content.Load<Texture2D>("run4_left");
            texturesJumpRight[0] = content.Load<Texture2D>("jump1");
            texturesJumpLeft[0] = content.Load<Texture2D>("jump1_left");
            texturesJumpRight[1] = content.Load<Texture2D>("jump2");
            texturesJumpLeft[1] = content.Load<Texture2D>("jump2_left");
            texturesJumpRight[2] = content.Load<Texture2D>("jump3");
            texturesJumpLeft[2] = content.Load<Texture2D>("jump3_left");
            texturesJumpRight[3] = content.Load<Texture2D>("jump4");
            texturesJumpLeft[3] = content.Load<Texture2D>("jump4_left");
        }
        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
