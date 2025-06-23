using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using platformer;
using SharpDX.MediaFoundation;
namespace platformer.Classes
{
    public class MainMenu : Menu
    {
        public event Action OnPlayingStarted;
        public MainMenu(int widthScreen, int heightScreen) : base(widthScreen, heightScreen)
        {
            buttonList.Add(new Label(new Vector2(0, 0), "Play", Color.Black));
            buttonList.Add(new Label(new Vector2(0, 40), "Exit", Color.Black));
        }
        public override void PressEnter()
        {
            if (selected == 0)
            {
                if (OnPlayingStarted != null)
                {
                    OnPlayingStarted();
                }
            }
            else if (selected == 1)
            {
                Game1.gameMode = GameMode.Exit;
            }
        }
    }
}
