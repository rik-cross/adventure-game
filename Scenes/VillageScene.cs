﻿using AdventureGame.Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using S = System.Diagnostics.Debug;

namespace AdventureGame
{
    public class VillageScene : Scene
    {
        public VillageScene()
        {
            EngineGlobals.DEBUG = true;
        }

        public override void LoadContent()
        {
            // add map
            AddMap("Maps/Map_Village");

            // add camera
            AddCamera("main");
            GetCameraByName("main").SetZoom(4.0f, instant: true);

            //
            // add NPCs
            //
            Engine.Entity blacksmithEntity = NPCEntity2.Create(225, 150, "spr_hammering_strip23", "Blacksmith-M06-thumb", idTag: "blacksmith");
            blacksmithEntity.GetComponent<TriggerComponent>().onCollide = SceneTriggers.BlacksmithDialogue;
            AddEntity(blacksmithEntity);

            //
            // add objects
            //
            AddEntity(TreeEntity.Create(40, 40));

            //
            // add buildings
            //
            // Todo - remove size
            AddEntity(PlayerHouseEntity.Create("player_house_01", 423, 111, 66, 66));

            AddEntity(BuildingEntity2.Create("blacksmith_01", 150, 50));

            List<string> keys = new List<string>() { "door_closed", "door_open" };
            AddEntity(BuildingEntity2.Create("shop_01", 300, 200, keys));

            //  cave entrance
            Engine.Entity caveEntranceEntity = EngineGlobals.entityManager.CreateEntity();
            caveEntranceEntity.AddComponent(new Engine.TransformComponent(new Vector2(496, 18), new Vector2(32, 6)));
            Engine.TriggerComponent tc = caveEntranceEntity.AddComponent<Engine.TriggerComponent>(
                new Engine.TriggerComponent(new Vector2(32, 16))
            );
            tc.onCollisionEnter = (Entity thisEntity, Entity otherEntity, float distance) => {
                if (otherEntity.IsLocalPlayer())
                {
                    EngineGlobals.sceneManager.SetActiveScene<CaveScene>();
                    EngineGlobals.sceneManager.SetPlayerScene<CaveScene>(new Vector2(395, 430));
                }
            };
            AddEntity(caveEntranceEntity);

            // Options for adding buildings:
            // 1. Single sprite (default key idle?)
            //      file, position, size (optional)
            // 2. Sprite sheet with multiple keys
            //      file, position, size (optional), Dictionary key/x,y


        }

        public override void OnEnter()
        {
            // Add the player and minimap cameras
            //AddCamera("minimap");
        }

        public override void Input(GameTime gameTime)
        {
            if (EngineGlobals.inputManager.IsPressed(Globals.backInput))
                EngineGlobals.sceneManager.RemoveScene(this);

            if (EngineGlobals.inputManager.IsPressed(Globals.pauseInput))
            {
                EngineGlobals.sceneManager.SetActiveScene<PauseScene>(
                    applyTransition: false, unloadCurrentScene: false);
            }

            if (EngineGlobals.inputManager.IsPressed(Globals.inventoryInput))
            {
                EngineGlobals.sceneManager.SetActiveScene<InventoryScene2>(
                    applyTransition: false, unloadCurrentScene: false);
            }

            if (EngineGlobals.inputManager.IsPressed(Globals.devToolsInput))
            {
                EngineGlobals.sceneManager.SetActiveScene<DevToolsScene>(
                    applyTransition: false, unloadCurrentScene: false);
            }
            
        }

        public override void Update(GameTime gameTime)
        {
            Utilities.SetBuildingAlpha(EntityList);
            //S.WriteLine(EngineGlobals.entityManager.GetLocalPlayer().State);
        }

        public override void Draw(GameTime gameTime)
        {

        }

    }

}