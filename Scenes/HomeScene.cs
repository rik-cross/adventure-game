﻿
using AdventureGame.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

using S = System.Diagnostics.Debug;

namespace AdventureGame
{

    public class HomeScene : Scene
    {

        public HomeScene()
        {

            lightLevel = 0.3f;

            // add map
            AddMap("home");

            //
            // add entities
            //
            
            // player entity
            //AddEntity(EngineGlobals.entityManager.GetEntityByTag("player"));
            // trigger
            AddEntity(EngineGlobals.entityManager.GetEntityById("h"));
            // light
            AddEntity(EngineGlobals.entityManager.GetEntityById("homeLight1"));
            // light switch
            AddEntity(EngineGlobals.entityManager.GetEntityById("lightSwitch1"));

            //
            // add cameras
            //

            // player camera
            Engine.Camera playerCamera = new Engine.Camera(
                name: "main",
                size: new Vector2(Globals.ScreenWidth, Globals.ScreenHeight),
                zoom: Globals.globalZoomLevel,
                backgroundColour: Color.DarkSlateBlue,
                trackedEntity: EngineGlobals.entityManager.GetLocalPlayer()
            );
            AddCamera(playerCamera);

            // minimap camera
            Engine.Camera minimapCamera = new Engine.Camera(
                name: "minimap",
                screenPosition: new Vector2(Globals.ScreenWidth - 320, Globals.ScreenHeight - 320),
                size: new Vector2(300, 300),
                followPercentage: 1.0f,
                zoom: 0.5f,
                backgroundColour: Color.DarkSlateBlue,
                borderColour: Color.Black,
                borderThickness: 2,
                trackedEntity: EngineGlobals.entityManager.GetLocalPlayer()

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