﻿using System;
using System.Collections.Generic;
using System.Text;

using AdventureGame.Engine;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended;

using S = System.Diagnostics.Debug;

namespace AdventureGame
{
    public class PauseScene : Engine.Scene
    {
        private Engine.Text _title;
        public override void Init()
        {
            DrawSceneBelow = true;
            backgroundColour = Color.Black * 0.5f;

            

            // title text
            _title = new Engine.Text(
                caption: "Game Paused",
                font: Theme.FontSubtitle,
                colour: Theme.TextColorTertiary,
                anchor: Anchor.TopCenter,
                padding: new Padding(top: 250),
                outline: true,
                outlineThickness: 6,
                outlineColour: Color.Black
            );

            UIMenu.AddUIElement(
                new UIButton(
                    position: new Vector2((Globals.ScreenWidth / 2) - 70, Globals.ScreenHeight / 2 - 25),
                    size: new Vector2(140, 45),
                    text: "Back",
                    textColour: Color.White,
                    outlineColour: Color.White,
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    func: (UIButton button) => {
                        EngineGlobals.sceneManager.ChangeToSceneBelow();
                    }
                )
            );

            UIMenu.AddUIElement(
                new UIButton(
                    position: new Vector2((Globals.ScreenWidth / 2) - 70, Globals.ScreenHeight / 2 + 25),
                    size: new Vector2(140, 45),
                    text: "Quit",
                    textColour: Color.White,
                    outlineColour: Color.White,
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    func: (UIButton button) => {
                        EngineGlobals.sceneManager.UnloadAllScenes();
                    }
                )
            );
        }

        public override void OnEnter()
        {
            EngineGlobals.soundManager.Volume /= 3;
        }
        public override void OnExit()
        {
            EngineGlobals.soundManager.Volume *= 3;
        }
        public override void Input(GameTime gameTime)
        {
            //if (EngineGlobals.inputManager.IsPressed(Globals.pauseInput))
            //    EngineGlobals.sceneManager.RemoveScene(this, applyTransition: false);
        }
        public override void Update(GameTime gameTime)
        {


        }

        public override void Draw(GameTime gameTime)
        {
            _title.Draw();
        }

    }

}
