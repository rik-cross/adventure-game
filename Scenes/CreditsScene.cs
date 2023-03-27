﻿using AdventureGame.Engine;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;

using System;
using System.Collections.Generic;
using S = System.Diagnostics.Debug;


namespace AdventureGame
{
    public class CreditsScene : Engine.Scene
    {
        Engine.Text _title;

        List<Engine.Text> creditsList = new List<Text>();
        List<string> creditsText;

        public void UnloadCreditsScene()
        {
            EngineGlobals.sceneManager.RemoveScene(this, applyTransition: true);
        }

        public CreditsScene()
        {

            // title text
            _title = new Engine.Text(
                caption: "Credits",
                font: Theme.FontSubtitle,
                colour: Theme.TextColorTertiary,
                anchor: Anchor.TopCenter,
                padding: new Padding(top: 100)
            );

            creditsText = new List<string>() {
                "Writers", "Rik Cross and Alex Parry", "rik-cross.github.com/adventure-game",
                "Graphics",  "Sunnyside World by danieldiggle", "danieldiggle.itch.io",
                "Sound", "name", "url"
            };
            int padding = 200;

            for(int i=0; i<creditsText.Count; i++)
            {
                creditsList.Add(
                    new Engine.Text(
                        caption: creditsText[i],
                        font: Theme.FontSecondary,
                        colour: Theme.TextColorSecondary,
                        anchor: Anchor.TopCenter,
                        padding: new Padding(top: padding)
                    )
                );
                padding += 30;
                if (i % 3 == 2)
                    padding += 30;
            }

            UIMenu.AddUIElement(
                new UIButton(
                    position: new Vector2((Globals.ScreenWidth / 2) - 60, 700),
                    size: new Vector2(120, 45),
                    text: "Back",
                    textColour: Color.White,
                    outlineColour: Color.White,
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    func: UnloadCreditsScene
                )
            );


        }

        public override void OnEnter()
        {
            
        }
        public override void OnExit()
        {

        }
        public override void Input(GameTime gameTime)
        {

        }
        public override void Update(GameTime gameTime)
        {


        }

        public override void Draw(GameTime gameTime)
        {
            _title.Draw();
            foreach (Engine.Text t in creditsList)
                t.Draw();
        }

    }

}