﻿using System;
using System.Collections.Generic;
using System.Text;

using AdventureGame.Engine;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            //if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            if (EngineGlobals.inputManager.IsPressed(Globals.backInput))
            {
                EngineGlobals.sceneManager.PopScene();
            }
            if (EngineGlobals.inputManager.IsPressed(Globals.forwardInput))
            {
                EngineGlobals.sceneManager.PushScene(new GameScene());
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Globals.spriteBatch.FillRectangle(
                new Rectangle(0, 0, Globals.WIDTH, Globals.HEIGHT), Color.Black
            );
            Globals.spriteBatch.DrawString(Globals.font, "Main menu", new Vector2(50, 50), Color.White);
        }

    }

}
