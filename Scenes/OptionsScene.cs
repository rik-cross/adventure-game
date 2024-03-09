﻿using AdventureGame.Engine;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using S = System.Diagnostics.Debug;


namespace AdventureGame
{
    public class OptionsScene : Engine.Scene
    {
        Engine.Text _title;

        public void UnloadOptionsScene(UIButton button)
        {
            //EngineGlobals.sceneManager.RemoveScene(this);
            //EngineGlobals.sceneManager.StartSceneTransition(new FadeSceneTransition(
            //        new List<Scene>() { }, numScenesToUnload: 1
            //    ));
            EngineGlobals.sceneManager.StartSceneTransition<
                FadeSceneTransition2, MenuScene>();
        }

        public void SetMute(UIButton button)
        {
            EngineGlobals.soundManager.Mute = !EngineGlobals.soundManager.Mute;
            if (EngineGlobals.soundManager.Mute)
                button.text = "Unmute";
            else
                button.text = "Mute";
            button.Init();
        }

        public void UpdateMusicVolume(UISlider button)
        {

            button.HandleInput();
            if (EngineGlobals.soundManager.Mute)
            {
                button.active = false;
                //S.WriteLine("fff");
            }
            else
            {
                button.active = true;
                //S.WriteLine("ttt");
            }

            button.currentValue = EngineGlobals.soundManager._targetVolume;

        }

        public void UpdateSFXVolume(UISlider button)
        {

            button.HandleInput();
            if (EngineGlobals.soundManager.Mute)
            {
                button.active = false;
                //S.WriteLine("fff");
            }
            else
            {
                button.active = true;
                //S.WriteLine("ttt");
            }

            button.currentValue = EngineGlobals.soundManager.SFXVolume;

        }


        public OptionsScene()
        {

            backgroundColour = Color.DarkSlateGray;
            UISlider.drawMethod = UICustomisations.DrawSlider;

            // title text
            _title = new Engine.Text(
                caption: "Options",
                font: Theme.FontSubtitle,
                colour: Theme.TextColorTertiary,
                anchor: Anchor.TopCenter,
                padding: new Padding(top: 100),
                outline: true,
                outlineThickness: 6,
                outlineColour: Color.Black
            );


            float screenMiddle = Globals.ScreenHeight / 2;

            string t = "Controller";
            if (EngineGlobals.entityManager.GetLocalPlayer().GetComponent<InputComponent>().input == Engine.Inputs.controller)
            {
                t = "Keyboard";
            }

            UIMenu.AddUIElement(
                new UIButton(
                    position: new Vector2((Globals.ScreenWidth / 2) - 70, screenMiddle - 100),
                    size: new Vector2(140, 45),
                    text: t,
                    textColour: Color.White,
                    outlineColour: Color.White,
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    func: UICustomisations.SetControls,
                    active: EngineGlobals.inputManager.IsControllerConnected()
                )
            );

            double m = 1;
            if (EngineGlobals.soundManager.Mute)
                m = 0;
            
            UIMenu.AddUIElement(
                new UIButton(
                    position: new Vector2((Globals.ScreenWidth / 2) - 70, screenMiddle - 50),
                    size: new Vector2(140, 45),
                    text: "Mute",
                    textColour: Color.White,
                    outlineColour: Color.White,
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    func: SetMute
                )
            );
            
            UIMenu.AddUIElement(
                new UISlider(
                    position: new Vector2((Globals.ScreenWidth / 2) - 70, screenMiddle),
                    size: new Vector2(140, 45),
                    text: "Music",
                    textColour: Color.White,
                    outlineColour: Color.White,
                    onColour: new Color(99,199,77,255),
                    offColour: new Color(228,59,68,255),
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    buttonSpecificUpdateMethod: UpdateMusicVolume,
                    func: UICustomisations.SetMusicVolume,
                    currentValue: EngineGlobals.soundManager.Volume
                )
            );

            UIMenu.AddUIElement(
                new UISlider(
                    position: new Vector2((Globals.ScreenWidth / 2) - 70, screenMiddle + 50),
                    size: new Vector2(140, 45),
                    text: "SFX",
                    textColour: Color.White,
                    outlineColour: Color.White,
                    onColour: new Color(99, 199, 77, 255),
                    offColour: new Color(228, 59, 68, 255),
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    buttonSpecificUpdateMethod: UpdateSFXVolume,
                    func: UICustomisations.SetSFXVolume,
                    currentValue: EngineGlobals.soundManager.SFXVolume
                )
            );

            string tt;
            if (EngineGlobals.fullscreen)
                tt = "Windowed";
            else
                tt = "Fullscreen";
            
            UIMenu.AddUIElement(
                new UIButton(
                    position: new Vector2((Globals.ScreenWidth / 2) - 70, screenMiddle + 100),
                    size: new Vector2(140, 45),
                    text: tt,
                    textColour: Color.White,
                    outlineColour: Color.White,
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    func: (UIButton button) => {
                        EngineGlobals.fullscreen = !EngineGlobals.fullscreen;
                        if (EngineGlobals.fullscreen)
                            button.text = "Windowed";
                        else
                            button.text = "Fullscreen";
                        Globals.graphics.IsFullScreen = EngineGlobals.fullscreen; 
                        Globals.graphics.ApplyChanges();
                    }
                )
            );

            UIMenu.AddUIElement(
                new UIButton(
                    position: new Vector2((Globals.ScreenWidth / 2) - 70, screenMiddle + 150),
                    size: new Vector2(140, 45),
                    text: "Back",
                    textColour: Color.White,
                    outlineColour: Color.White,
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    func: UnloadOptionsScene
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
        }

    }

}
