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
    public abstract class Menu
    {
        protected List<Label> buttonList = new List<Label>();
        protected int selected;
        protected int widthScreen;
        protected int heightScreen;
        protected KeyboardState keyboardState;
        protected KeyboardState prevKeyboardState;
        public Menu(int widthScreen, int heightScreen)
        {
            selected = 0;
            this.widthScreen = widthScreen;
            this.heightScreen = heightScreen;
        }
        public void Update()
        {
            keyboardState = Keyboard.GetState();
            if (prevKeyboardState.IsKeyUp(Keys.S) &&
                keyboardState.IsKeyDown(Keys.S))
            {
                selected++;
                if (selected >= buttonList.Count)
                {
                    selected = 0;
                }
            }
            if (prevKeyboardState.IsKeyUp(Keys.W) &&
                keyboardState.IsKeyDown(Keys.W))
            {
                selected--;
                if (selected < 0)
                {
                    selected = buttonList.Count - 1;
                }
            }
            if (prevKeyboardState.IsKeyUp(Keys.Enter) &&
                keyboardState.IsKeyDown(Keys.Enter))
            {
                PressEnter();
            }
            prevKeyboardState = keyboardState;
        }
        public void LoadContent(ContentManager content)
        {
            int width = -100000;
            int height = 0;
            int offset = 0;
            foreach (Label button in buttonList)
            {
                button.LoadContent(content);
                if (button.SizeText.X > width)
                {
                    width = (int)button.SizeText.X;
                }
                height = height + (int)button.SizeText.Y;
            }
            height = height + 20 * (buttonList.Count - 1);
            int x = widthScreen / 2 - width / 2;
            int y = heightScreen / 2 - height / 2;
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].Position = new Vector2(
                    x + (width - buttonList[i].SizeText.X) / 2,
                    y + offset);
                offset += (int)buttonList[i].SizeText.Y + 20;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                Color colorButton;
                if (i == selected)
                {
                    colorButton = Color.Green;
                }
                else
                {
                    colorButton = Color.Black;
                }
                buttonList[i].Color = colorButton;
                buttonList[i].Draw(spriteBatch);
            }
        }
        public abstract void PressEnter();
    }
}
