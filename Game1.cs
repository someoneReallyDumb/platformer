using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using platformer.Classes;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using System.Text.Json;
using System.IO;
using System.Text.Json.Nodes;
using System.Drawing;

namespace platformer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Player player;
        private Background background;
        private HUD hud;
        private MainMenu mainMenu;
        private PauseMenu pauseMenu;
        private GameOver gameOver;
        private Target target;
        private Spider spider; 
        private List<PlayerBullet> playerBullets;
        private List<SpiderBullet> spiderBullets;
        #region Platforms
        private Platform p1;
        private Platform p2;
        private Platform p3;
        private Platform p4;
        private Platform p5;
        private Platform p6;
        private Platform p7;
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
            gameOver = new GameOver(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            hud = new HUD();
            spider = new Spider(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            target = new Target(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            playerBullets = new List<PlayerBullet>();
            spiderBullets = new List<SpiderBullet>();

            #region Platforms
            p1 = new Platform(210, 400);
            p2 = new Platform(488, 400);
            p3 = new Platform(89, 300);
            p4 = new Platform(349, 300);
            p5 = new Platform(609, 300);
            p6 = new Platform(210, 200);
            p7 = new Platform(488, 200);

            platforms = [p1, p2, p3, p4, p5, p6, p7];
            #endregion

            mainMenu.OnPlayingStarted += OnPlayingStarted;
            pauseMenu.OnPlayingResumed += OnPlayingResumed;
            player.TakeDamage += hud.OnPlayerTakeDamage;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            background.LoadContent(Content);
            mainMenu.LoadContent(Content);
            pauseMenu.LoadContent(Content);
            gameOver.LoadContent(Content);
            target.LoadContent(Content);
            spider.LoadContent(Content);
            hud.LoadContent(GraphicsDevice, Content);
            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].LoadContent(Content);
            }
            foreach (PlayerBullet bullet in playerBullets)
            {
                bullet.LoadContent(Content);
            }
            foreach (SpiderBullet bullet in spiderBullets)
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
                    Reset();
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
                    spider.Update(Content, spriteBatch);
                    if (player.Health <= 0)
                    {
                        gameMode = GameMode.GameOver;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameMode = GameMode.Pause;
                        //MediaPlayer.Play(_menuSong);
                    }
                    break;
                case GameMode.GameOver:
                    gameOver.Update();
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
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.LightGreen);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            switch (gameMode)
            {
                case GameMode.Menu:
                    background.Draw(spriteBatch);
                    mainMenu.Draw(spriteBatch);
                    break;
                case GameMode.Pause:
                    background.Draw(spriteBatch);
                    pauseMenu.Draw(spriteBatch);
                    break;
                case GameMode.Playing:
                    background.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    for (int i = 0; i < platforms.Length; i++)
                    {
                        platforms[i].Draw(spriteBatch);
                    }
                    target.Draw(spriteBatch);
                    spider.Draw(spriteBatch);
                    hud.Draw(spriteBatch);
                    //hud.Draw(spriteBatch);
                    break;
                case GameMode.GameOver:
                    background.Draw(spriteBatch);
                    gameOver.Draw(spriteBatch);
                    break;
                case GameMode.Exit:
                    break;
                default:
                    break;
            }
            
            spriteBatch.End();
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
            }
            foreach (Bullet bullet in player.PlayerBullets)
            {
                if (bullet.DestinationRectangle.Intersects(target.HitBox))  //
                {
                    target.ChangePosition();
                    bullet.IsAlive = false;
                }
            }
            if (!player.IsHurt)
            {
                if (spider.SpiderBullets != null)
                {
                    foreach (SpiderBullet spiderBullet in spider.SpiderBullets)
                    {
                        if (spiderBullet.DestinationRectangle.Intersects(player.UpCollision))
                        {
                            spiderBullet.IsAlive = false;
                            player.Damage();
                        }
                    }
                }
                if (spider.HitBox.Intersects(player.UpCollision))
                {
                    player.Damage();
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
        private void Reset()
        {
            player.Reset();
            spider.Reset();
            hud.Reset(player.MaxHealth);
        }
    }
}
