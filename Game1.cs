using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using platformer.Classes;
//using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;

namespace platformer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;

        private Player player;
        private Background background;
        private HUD hud;
        private MainMenu mainMenu;
        private PauseMenu pauseMenu;
        private Target target;
        private List<PlayerBullet> playerBullets;
        #region Platforms
        private Platform p1;
        private Platform p2;
        private Platform p3;
        private Platform p4;
        private Platform p5;
        private Platform p6;
        private Platform p7;
        private Platform p8;
        private Platform p9;
        private Platform p10;
        private Platform[] platforms;
        #endregion
        public static GameMode gameMode = GameMode.Menu;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            //platform = new Platform(390, 400);
            background = new Background();
            mainMenu = new MainMenu(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            pauseMenu = new PauseMenu(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            hud = new HUD();
            target = new Target(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            playerBullets = new List<PlayerBullet>();

            #region Platforms
            p1 = new Platform(210, 400);
            p2 = new Platform(488, 400);
            p3 = new Platform(89, 300);
            p4 = new Platform(349, 300);
            p5 = new Platform(609, 300);
            p6 = new Platform(210, 200);
            p7 = new Platform(488, 200);
            p8 = new Platform(89, 100);
            p9 = new Platform(349, 100);
            p10 = new Platform(609, 100);

            platforms = [p1, p2, p3, p4, p5, p6, p7, p8, p9, p10];
            #endregion

            mainMenu.OnPlayingStarted += OnPlayingStarted;
            pauseMenu.OnPlayingResumed += OnPlayingResumed;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            background.LoadContent(Content);
            mainMenu.LoadContent(Content);
            pauseMenu.LoadContent(Content);
            target.LoadContent(Content);
            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].LoadContent(Content);
            }
            foreach (PlayerBullet bullet in playerBullets)
            {
                bullet.LoadContent(Content);
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameMode)
            {
                case GameMode.Menu:
                    mainMenu.Update();
                    break;
                case GameMode.Pause:
                    pauseMenu.Update();
                    break;
                case GameMode.Playing:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                        Exit();
                    CheckCollision();
                    player.Update(
                            graphics.PreferredBackBufferWidth,
                            graphics.PreferredBackBufferHeight,
                            Content, gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameMode = GameMode.Pause;
                        //MediaPlayer.Play(_menuSong);
                    }
                    break;
                case GameMode.GameOver:
                    break;
                case GameMode.Exit:
                    Exit();
                    break;
            }
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Thistle);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            switch (gameMode)
            {
                case GameMode.Menu:
                    background.Draw(_spriteBatch);
                    mainMenu.Draw(_spriteBatch);
                    break;
                case GameMode.Pause:
                    background.Draw(_spriteBatch);
                    pauseMenu.Draw(_spriteBatch);
                    break;
                case GameMode.Playing:
                    background.Draw(_spriteBatch);
                    player.Draw(_spriteBatch);
                    for (int i = 0; i < platforms.Length; i++)
                    {
                        platforms[i].Draw(_spriteBatch);
                    }
                    target.Draw(_spriteBatch);
                    //hud.Draw(_spriteBatch);
                    break;
                case GameMode.GameOver:
                    background.Draw(_spriteBatch);
                    //gameOver.Draw(_spriteBatch);
                    break;
                case GameMode.Exit:
                    break;
                default:
                    break;
            }
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public void CheckCollision()
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                if (player.UpCollision.Intersects(platforms[i].Location))
                {
                    player.position.Y = platforms[i].Location.Y + platforms[i].Height;
                }

                if (player.DownCollision.Intersects(platforms[i].Location))
                {
                    player.IsJumping = false;
                    player.IsFalling = false;
                    player.ClearJump();
                }

                if (player.LeftCollision.Intersects(platforms[i].Location))
                {
                    player.position.X = platforms[i].Location.X + platforms[i].Location.Width;
                }

                if (player.RightCollision.Intersects(platforms[i].Location))
                {
                    player.position.X = platforms[i].Location.X - player.Width;
                }
                if(target.HitBox.Intersects(platforms[i].Location))
                {
                    target.ChangePosition();
                }

            }
            foreach (Bullet bullet in playerBullets)
            {
                if (bullet.DestinationRectangle.Intersects(target.HitBox))
                {
                    target.ChangePosition();
                    bullet.IsAlive = false;
                }
            }
        }
        private void OnPlayingStarted()
        {
            gameMode = GameMode.Playing;
            //MediaPlayer.Play(_gameSong);
            //Reset();
        }
        private void OnPlayingResumed()
        {
            gameMode = GameMode.Playing;
            //MediaPlayer.Play(_gameSong);
        }
    }
}
