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
    public class PlayerSelectScene : Engine.Scene
    {
        private Engine.Text _title;

        private Engine.Camera camera;

        public PlayerSelectScene()
        {
            EngineGlobals.DEBUG = false;
            UIButton.drawMethod = UICustomisations.DrawButton;
            UISlider.drawMethod = UICustomisations.DrawSlider;
            backgroundColour = Color.DarkSlateGray;

            //AddMap("Maps/Map_PlayerSelect");

            camera = new Engine.Camera(
                    name: "main",
                    size: new Vector2(Globals.ScreenWidth, Globals.ScreenHeight),
                    zoom: 6.0f
                );

            camera.SetWorldPosition(new Vector2(0, 0), instant: true);

            //S.WriteLine(camera.worldPosition);
            //S.WriteLine(EngineGlobals.entityManager.GetLocalPlayer().GetComponent<Engine.TransformComponent>().Position);

            //camera.zoomIncrement = 0.005f;
            //camera.SetZoom(3.0f);
            //AddCamera(n);
            CameraList.Add(camera);
            
            LightLevel = 1.0f;

            // title text
            _title = new Engine.Text(
                caption: "Player Select",
                font: Theme.FontSubtitle,
                colour: Theme.TextColorTertiary,
                anchor: Anchor.TopCenter,
                padding: new Padding(top: 100),
                outline: true,
                outlineThickness: 6,
                outlineColour: Color.Black
            );

            float screenMiddle = Globals.ScreenHeight / 2;

            UIMenu.AddUIElement(
                new UISlider(
                    position: new Vector2((Globals.ScreenWidth / 2) - 60, screenMiddle + 150),
                    size: new Vector2(120, 45),
                    text: "Player",
                    textColour: Color.White,
                    outlineColour: Color.White,
                    onColour: new Color(194, 133, 105, 255),
                    offColour: new Color(194, 133, 105, 255),
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    buttonSpecificUpdateMethod: (UISlider slider) => {
                        slider.HandleInput();
                        slider.text = Globals.characherNames[Globals.playerIndex];
                        slider.Init();
                    },
                    func: (UISlider slider, double currentValue) =>
                    {
                        Globals.playerIndex = (int)currentValue;
                        PlayerEntity.AddSprites();
                    },
                    minValue: 0,
                    maxValue: 5,
                    stepValue: 1,
                    currentValue: Globals.playerIndex
                )
            );

            UIMenu.AddUIElement(
                new UIButton(
                    position: new Vector2((Globals.ScreenWidth / 2) - 60, screenMiddle + 200),
                    size: new Vector2(120, 45),
                    text: "Back",
                    textColour: Color.White,
                    outlineColour: Color.White,
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    func: (UIButton button) => { EngineGlobals.sceneManager.RemoveScene(this); }
                )
            );

        }

        public override void OnEnter()
        {
            EngineGlobals.DEBUG = false;
            EngineGlobals.entityManager.GetLocalPlayer().GetComponent<Engine.InputComponent>().inputControllerStack.Clear();
            EngineGlobals.entityManager.GetLocalPlayer().State = "idle_right";
        }
        public override void OnExit()
        {
            EngineGlobals.entityManager.GetLocalPlayer().GetComponent<Engine.InputComponent>().inputControllerStack.Push(PlayerEntity.PlayerInputController);
        }
        public override void Input(GameTime gameTime)
        {
            // todo -- remove this
            if (EngineGlobals.inputManager.IsPressed(Globals.backInput))
                EngineGlobals.sceneManager.RemoveScene(this);
            //    UnloadMenuScene(null);
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