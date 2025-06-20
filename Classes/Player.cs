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
    public class Player
    {
        public Vector2 position;
        private Texture2D texture;
        private Texture2D defaultTextureRight;
        private Texture2D defaultTextureLeft;
        private int health = 5;
        private int maxHealth = 5;
        private int timer = 0;
        private int maxTime = 5;
        private float speed = 7;
        private float fallSpeed = 10;
        private float jumpSpeed = 7;
        private float jumpHeight = 100;
        private float startHeight = 0;
        private int bulletTimer = 15;
        private int bulletMaxTime = 15;
        private int immuneTime = 0;
        private int immuneMaxTime = 30;
        //private double time = 0.0d;
        //private double duration = 400.0d;
        //private double jumpCount = 0;
        private static bool isLeft = false;
        //private bool jumped = false;
        private int textureRunNum = 0;
        private bool isAlive = true;
        private bool isHurt = false;

        public event Action<int> TakeDamage;

        private Texture2D[] texturesRunRight = new Texture2D[4];
        private Texture2D[] texturesRunLeft = new Texture2D[4];
        private Texture2D[] texturesJumpRight = new Texture2D[4];
        private Texture2D[] texturesJumpLeft = new Texture2D[4];

        List<PlayerBullet> playerBullets = new List<PlayerBullet>();

        private int animSpeed = 7;
        private Rectangle upCollision;
        private Rectangle downCollision;
        private Rectangle leftCollision;
        private Rectangle rightCollision;

        Face face = new Face(isLeft);
        public int Height
        {
            get { return texture.Height; }
        }
        public int Width
        {
            get { return texture.Width; }
        }
        public int Health
        {
            get => health;
        }
        public int MaxHealth
        {
            get => maxHealth;
        }
        public float JumpSpeed
        {
            get { return jumpSpeed; }
        }
        public bool IsJumping
        {
            get;
            set;
        }
        public bool IsFalling
        {
            get;
            set;
        }
        public bool IsHurt
        {
            get => isHurt;
            set => isHurt = value;
        }
        public bool IsLeft
        {
            get { return isLeft; }
        }
        public bool IsAlive
        {
            get { return isAlive; }
        }
        public List<PlayerBullet> PlayerBullets
        {
            get { return playerBullets; }  
        }
        #region collisions
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
        #endregion
        //public Vector2 Position { get; set; }
        public Player()
        {
            position = new Vector2(0,1000);
            texture = defaultTextureRight;
            IsFalling = true;
            upCollision = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            downCollision = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            rightCollision = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            leftCollision = new Rectangle((int)position.X, (int)position.Y, 0, 0);
        }
        public void Update(int widthScreen, int heightScreen, ContentManager content,
            GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            face.Update();
            face.IsLeft = isLeft;
            face.position = new Vector2(position.X, position.Y);
            if (isHurt)
            {
                immuneTime++;
                face.Suffer();
                if (immuneTime >= immuneMaxTime)
                {
                    isHurt = false;
                    immuneTime = 0;
                    face.TextureChange();
                }
            }
            if (IsFalling)
            {
                position.Y += fallSpeed;
            }
            if (isLeft)
            { texture = defaultTextureLeft; }
            else { texture = defaultTextureRight; }
            #region Movement
            if (keyboard.IsKeyDown(Keys.Space) || keyboard.IsKeyDown(Keys.W))
            {
                if (!IsFalling)
                {
                    Jump(heightScreen);
                }
            }
            else
            {
                IsJumping = false;
                IsFalling = true;
                startHeight = heightScreen;
            }
            if (textureRunNum > animSpeed * 3)
            {
                textureRunNum = 0;
            }
            if (keyboard.IsKeyDown(Keys.A) && !keyboard.IsKeyDown(Keys.D))
            {
                position.X -= speed;
                isLeft = true;
                if (texture != null && !IsJumping)
                {
                    texture = texturesRunLeft[textureRunNum / animSpeed];
                    textureRunNum++;
                }    
            }
            if (textureRunNum > animSpeed*3)
            {
                textureRunNum = 0;
            }
            if (keyboard.IsKeyDown(Keys.D) && !keyboard.IsKeyDown(Keys.A))
            {
                position.X += speed;
                isLeft = false;
                if (texture != null && !IsJumping)
                {
                    texture = texturesRunRight[textureRunNum / animSpeed];
                    textureRunNum++;
                }       
            }
            if (keyboard.IsKeyDown(Keys.S) && !keyboard.IsKeyDown(Keys.D) &&
                !keyboard.IsKeyDown(Keys.A) && !keyboard.IsKeyDown(Keys.W) &&
                !keyboard.IsKeyDown(Keys.Space))
            {
                if (!isLeft)
                {
                    texture = texturesJumpRight[0];
                }
                else
                {
                    texture = texturesJumpLeft[0];
                }
            }
            #endregion
            if (keyboard.IsKeyDown(Keys.RightShift) || keyboard.IsKeyDown(Keys.LeftShift))
            {
                face.IsShooting = true;
            }
            else
            {
                if (face.IsShooting)
                {
                    timer++;
                }
                if (timer >= maxTime)
                {
                    timer = 0;
                    face.IsShooting = false;
                }
            }
            #region Bounds
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            if (position.X >= widthScreen - texture.Width)
            {
                position.X = widthScreen - texture.Width;
            }
            if (position.Y >= heightScreen - texture.Height)
            {
                position.Y = heightScreen - texture.Height;
                IsFalling = false;
            }
            #endregion
            upCollision = new Rectangle((int)position.X, 
                (int)position.Y - 5, texture.Width, 10);
            downCollision = new Rectangle((int)position.X,
                (int)position.Y + texture.Height - 5, texture.Width, 10);
            leftCollision = new Rectangle((int)position.X,
                (int)position.Y + 10, 10, texture.Height - 20);
            rightCollision = new Rectangle((int)position.X + texture.Width,
                (int)position.Y + 10, 10, texture.Height - 20);
            face.position = new Vector2(position.X, position.Y);
            if (keyboard.IsKeyDown(Keys.RightShift) || keyboard.IsKeyDown(Keys.LeftShift))
            {
                bulletTimer++;
                if (bulletTimer >= bulletMaxTime)
                {
                    PlayerBullet playerBullet = new PlayerBullet();
                    playerBullet.LoadContent(content);
                    if (!isLeft)
                    {
                        playerBullet.DestinationRectangle = new Rectangle((int)position.X + 35,
                            (int)position.Y + 10, playerBullet.Width, playerBullet.Height);
                    }
                    else
                    {
                        playerBullet.DestinationRectangle = new Rectangle((int)position.X - 22,
                            (int)position.Y + 10, playerBullet.Width, playerBullet.Height);
                    }
                    playerBullet.IsLeft = isLeft;
                    
                    playerBullets.Add(playerBullet);
                    bulletTimer = 0;
                }
            }
            foreach (PlayerBullet bullet in playerBullets)
            {
                bullet.Update(widthScreen, heightScreen);
            }
            for (int i = 0; i < playerBullets.Count; i++)
            {
                if (playerBullets[i].IsAlive == false)
                {
                    playerBullets.RemoveAt(i);
                    i--;
                }
            }
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
                IsJumping = true;
            }
            else
            {
                IsJumping = false;
                if (isLeft)
                { texture = texturesJumpLeft[3]; }
                else
                { texture = texturesJumpRight[3]; }
                if (position.Y >= heightScreen - texture.Height)  //!!!
                {
                    startHeight = 0;
                    //jumpCount++;
                }
                IsFalling = true;
            }
        }
        public void ClearJump()
        {
            startHeight = 0;
            IsJumping = false;
        }
        public void LoadContent(ContentManager content)
        {
            defaultTextureRight = content.Load<Texture2D>("default");
            texture = content.Load<Texture2D>("default");
            defaultTextureLeft = content.Load<Texture2D>("default_left");
            LoadRunTextures(content);
            face.LoadContent(content);
            foreach (PlayerBullet bullet in playerBullets)
            {
                bullet.LoadContent(content);
            }
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
            face.Draw(spriteBatch);
            foreach (PlayerBullet bullet in playerBullets)
            {
                bullet.Draw(spriteBatch);
            }
        }
        public void Damage()
        {
            health--;
            isHurt = true;
            if (TakeDamage != null)
                TakeDamage(health);
        }
        public void Reset()
        {
            position = new Vector2(0, 1000);
            texture = defaultTextureRight;
            playerBullets.Clear();
            timer = 0;
            bulletTimer = 15;
            isLeft = false;
            textureRunNum = 0;
            startHeight = 0;
            isAlive = true;
            health = maxHealth;
            face.Reset();
        }
    }
}
