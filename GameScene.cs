﻿
using AdventureGame.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

using System;
using System.Collections.Generic;

namespace AdventureGame
{

    public class GameScene : Scene
    {

        public override void Init()
        {

            // add map
            AddMap("startZone");

            //
            // add entities
            //
            
            // player entity
            AddEntity(EngineGlobals.entityManager.GetEntityByTag("player"));
            // enemy entity
            AddEntity(EngineGlobals.entityManager.GetEntityByTag("enemy"));
            // light entity
            AddEntity(EngineGlobals.entityManager.GetEntityByTag("light"));
            // map trigger
            AddEntity(EngineGlobals.entityManager.GetEntityByTag("m"));

            //
            // add cameras
            //

            // player camera
            Engine.Camera playerCamera = new Engine.Camera("main", 0, 0, 0, 0, Globals.WIDTH, Globals.HEIGHT, Globals.globalZoomLevel, 0, 2);
            playerCamera.trackedEntity = EngineGlobals.entityManager.GetEntityByTag("player");
            AddCamera(playerCamera);

            // minimap camera
            Engine.Camera minimapCamera = new Engine.Camera("minimap", 300, 300, Globals.WIDTH - 320, Globals.HEIGHT - 320, 300, 300, 0.5f, 0, 2);
            minimapCamera.trackedEntity = EngineGlobals.entityManager.GetEntityByTag("player");
            AddCamera(minimapCamera);

        }

        public override void LoadContent()
        {
            Init();
        }

        public override void Update(GameTime gameTime)
        {
            // update scene time and set light level
            // commented out DayNightCycle for testing
            //DayNightCycle.Update(gameTime);
            //lightLevel = DayNightCycle.GetLightLevel();

            if (EngineGlobals.inputManager.IsPressed(Globals.backInput))
            {
                EngineGlobals.sceneManager.PopScene();
            }
            if (EngineGlobals.inputManager.IsPressed(Globals.pauseInput))
            {
                EngineGlobals.sceneManager.PushScene(new PauseScene());
            }
        }

    }

}