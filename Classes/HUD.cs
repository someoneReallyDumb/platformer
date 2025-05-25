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
    public class HUD
    {
        private PlayerHealth health;
        public HUD()
        {
            //Vector2 position = new Vector2(20, 20);
            health = new PlayerHealth(new Vector2(20, 20), 10, 200, 15);
        }
        public void LoadContent(GraphicsDevice graphics, ContentManager content)
        {
            health.LoadContent(content);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            health.Draw(spriteBatch);
        }
        public void OnPlayerTakeDamage(int health)
        {
            this.health.NumParts = health;
        }
        public void Reset()
        {
            health.NumParts = 3;
        }
    }
}
