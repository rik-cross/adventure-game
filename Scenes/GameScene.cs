﻿using System.Collections.Generic;
using AdventureGame.Engine;
using Microsoft.Xna.Framework;

using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace AdventureGame
{

    public class GameScene : Scene
    {

        public GameScene()
        {
            // Get scene data from the JSON file
            string projectSourcePath = ProjectSourcePath.Value;
            var jsonString = File.ReadAllText(projectSourcePath + "Data/village.json");
            //var jsonData = (JObject)JsonConvert.DeserializeObject(jsonString);
            var jsonData = JObject.Parse(jsonString);

            // Add the map
            AddMap(jsonData["Map"]["Filename"].ToString());

            // Create an Item entity for each item in the JSON file
            var items = jsonData["Items"];
            foreach (var item in items)
            {
                Console.WriteLine($"X:{item["X"]} Y:{item["Y"]} Filename:{item["Filename"]}" +
                    $" Collectable:{item["Collectable"]}");
                // .Value<int>()  .ToObject<int[]>()

                // Convert to a list if not null using the null-conditional operator
                List<string> collectable = item["$.Collectable"]?.Values<string>().ToList();

                try
                {
                    AddEntity(ItemEntity.Create((int)item["X"], (int)item["Y"],
                        (string)item["Filename"], collectable));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception adding item entity: {ex}");
                }
            }

            // Create an Enemy entity for each enemy in the JSON file
            var enemies = jsonData["Enemies"];
            foreach (var enemy in enemies)
            {
                Console.WriteLine($"X:{enemy["X"]} Y:{enemy["Y"]} Filename:{enemy["Filename"]}");

                try
                {
                    AddEntity(EnemyEntity.Create((int)enemy["X"], (int)enemy["Y"],
                        (string)enemy["Filename"]));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception adding enemy entity: {ex}");
                }
            }

            // add map
            //AddMap("startZone");
            //AddMap("village.tmx");
            //AddMap("village");

            //
            // add entities
            //

            // home entity
            AddEntity(EngineGlobals.entityManager.GetEntityByName("home"));
            // enemy entity
            //AddEntity(EngineGlobals.entityManager.GetEntityByName("enemy1"));
            // light entity
            AddEntity(EngineGlobals.entityManager.GetEntityByName("light1"));
            // map trigger
            AddEntity(EngineGlobals.entityManager.GetEntityByName("m"));

            // item entities
            //HashSet<string> playerCollectable = new HashSet<string>() { "player" };
            //AddEntity(ItemEntity.Create(30, 170, "Items/W_Sword003", playerCollectable));
            //AddEntity(ItemEntity.Create(100, 250, "Items/I_Boulder01"));

            //AddEntity(EngineGlobals.entityManager.GetEntityByTag("m"));
            //AddEntity(EngineGlobals.entityManager.GetAllEntitiesByTag("item"));

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