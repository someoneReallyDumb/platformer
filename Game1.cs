using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using platformer.Classes;
using System;
using System.Collections.Generic;

namespace platformer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player player;
        private Platform platform;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            platform = new Platform(390, 440);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            platform.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            CheckCollision();
            player.Update(
                    _graphics.PreferredBackBufferWidth,
                    _graphics.PreferredBackBufferHeight,
                    Content, gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Thistle);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            player.Draw(_spriteBatch);
            platform.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public void CheckCollision()
        {
            if (player.UpCollision.Intersects(platform.Location))
            {
                player.position.Y = platform.Location.Y - platform.Width;
            }
            if (player.DownCollision.Intersects(platform.Location))
            {
                player.position.Y = platform.Location.Y;
            }
        }
    }
}
