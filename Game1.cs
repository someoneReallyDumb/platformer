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
        private Platform platform;
        private Background background;
        private HUD hud;
        private MainMenu mainMenu;
        private PauseMenu pauseMenu;
        private List<PlayerBullet> playerBullets;
        //private Face face;
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
            platform = new Platform(390, 400);
            background = new Background();
            mainMenu = new MainMenu(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            pauseMenu = new PauseMenu(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            hud = new HUD();
            playerBullets = new List<PlayerBullet>();
            //face = new Face(player.IsLeft);

            mainMenu.OnPlayingStarted += OnPlayingStarted;
            pauseMenu.OnPlayingResumed += OnPlayingResumed;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            platform.LoadContent(Content);
            background.LoadContent(Content);
            mainMenu.LoadContent(Content);
            pauseMenu.LoadContent(Content);
            foreach (PlayerBullet bullet in playerBullets)
            {
                bullet.LoadContent(Content);
            }
            //face.LoadContent(Content);
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
                    platform.Draw(_spriteBatch);
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
            if (player.UpCollision.Intersects(platform.Location))
            {
                player.position.Y = platform.Location.Y + platform.Height;
            }

            if (player.DownCollision.Intersects(platform.Location))
            {
                //player.position.Y -= player.JumpSpeed;
                player.IsJumping = false;
                player.IsFalling = false;
                player.ClearJump();
            }

            if (player.LeftCollision.Intersects(platform.Location))
            {
                player.position.X = platform.Location.X + platform.Location.Width;
            }

            if (player.RightCollision.Intersects(platform.Location))
            {
                player.position.X = platform.Location.X - player.Width;
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
