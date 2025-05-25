using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;

namespace platformer.Classes
{
    public class PauseMenu : Menu
    {
        public event Action OnPlayingResumed;
        public event Action OnSaveGame;
        public PauseMenu(int widthScreen, int heightScreen) : base(widthScreen, heightScreen)
        {
            buttonList.Add(new Label(new Vector2(0, 0), "Resume", Color.Black));
            buttonList.Add(new Label(new Vector2(0, 40), "Exit to menu", Color.Black));
        }
        public override void PressEnter()
        {
            if (selected == 0)
            {
                if (OnPlayingResumed != null)
                    OnPlayingResumed();
            }
            if (selected == 1)
            {
                Game1.gameMode = GameMode.Menu;
            }
        }
    }
}
