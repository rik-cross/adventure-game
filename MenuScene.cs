﻿using System;
using System.Collections.Generic;
using System.Text;

using AdventureGame.Engine;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;

namespace AdventureGame
{
    public class MenuScene : Engine.Scene
    {

        public override void Init()
        {
            
        }

        public override void LoadContent()
        {
            Init();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                EngineGlobals.sceneManager.PopScene();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                EngineGlobals.sceneManager.PushScene(new GameScene());
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Globals.spriteBatch.FillRectangle(
                new Rectangle(0, 0, 800, 640), Color.Black
            );
            Globals.spriteBatch.DrawString(Globals.font, "Main menu", new Vector2(50, 50), Color.White);
            Globals.spriteBatch.DrawString(Globals.fontSmall, "[enter] play  //  [esc] quit", new Vector2(10, 450), Color.White);
        }

    }

}