﻿using System.Collections.Generic;

using AdventureGame.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

using S = System.Diagnostics.Debug;

namespace AdventureGame
{

    public class BeachScene : Scene
    {

        public BeachScene()
        {
            // add map
            AddMap("beach");

            //
            // add entities
            //
            
            // trigger
            AddEntity(EngineGlobals.entityManager.GetEntityByName("b"));

            //
            // add cameras
            //

            // player camera
            Engine.Camera playerCamera = new Engine.Camera(
                name: "main",
                size: new Vector2(Globals.WIDTH, Globals.HEIGHT),
                zoom: Globals.globalZoomLevel,
                backgroundColour: Color.DarkSlateBlue,
                trackedEntity: EngineGlobals.entityManager.GetEntityByName("player1")
            );
            AddCamera(playerCamera);

            // minimap camera
            Engine.Camera minimapCamera = new Engine.Camera(
                name: "minimap",
                screenPosition: new Vector2(Globals.WIDTH - 320, Globals.HEIGHT - 320),
                size: new Vector2(300, 300),
                followPercentage: 1.0f,
                zoom: 0.5f,
                backgroundColour: Color.DarkSlateBlue,
                borderColour: Color.Black,
                borderThickness: 2,
                trackedEntity: EngineGlobals.entityManager.GetEntityByName("player1")

            );
            AddCamera(minimapCamera);

        }

        public override void Update(GameTime gameTime)
        {
            // update scene time and set light level
            // commented out DayNightCycle for testing
            //DayNightCycle.Update(gameTime);
            //lightLevel = DayNightCycle.GetLightLevel();

            if (EngineGlobals.inputManager.IsPressed(Globals.backInput) && EngineGlobals.sceneManager.transition == null)
            {
                EngineGlobals.sceneManager.transition = new FadeSceneTransition(null);
            }
            if (EngineGlobals.inputManager.IsPressed(Globals.pauseInput))
            {
                EngineGlobals.sceneManager.PushScene(new PauseScene());
            }
        }

    }

}