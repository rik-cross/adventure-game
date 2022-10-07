﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AdventureGame.Engine;

using System;

namespace AdventureGame
{
    public class Game1 : Game
    {
        public EntityManager entityManager; // REMOVE? reference from EngineGlobals
        public SceneManager sceneManager;

        public static void doorOnCollisionEnter(Entity thisEntity, Entity otherEntity, float distance)
        {
            if (otherEntity.Tags.HasTag("player"))
            {

                Globals.gameScene.GetCameraByName("main").trackedEntity = null;
                Globals.gameScene.GetCameraByName("minimap").trackedEntity = null;
                Globals.gameScene.RemoveEntity(otherEntity);

                otherEntity.GetComponent<Engine.TransformComponent>().position = new Vector2(525, 900);
                Globals.beachScene.GetCameraByName("main").SetWorldPosition(otherEntity.GetComponent<Engine.TransformComponent>().GetCenter(), instant: true);
                Globals.beachScene.GetCameraByName("minimap").SetWorldPosition(otherEntity.GetComponent<Engine.TransformComponent>().GetCenter(), instant: true);
                Globals.beachScene.AddEntity(otherEntity);
                Globals.beachScene.GetCameraByName("main").trackedEntity = otherEntity;
                Globals.beachScene.GetCameraByName("minimap").trackedEntity = otherEntity;

                EngineGlobals.sceneManager.transition = new FadeSceneTransition(Globals.beachScene, replaceScene: true);
            }
        }

        public static void beachOnCollisionEnter(Entity thisEntity, Entity otherEntity, float distance)
        {
            if (otherEntity.Tags.HasTag("player"))
            {

                Globals.beachScene.GetCameraByName("main").trackedEntity = null;
                Globals.beachScene.GetCameraByName("minimap").trackedEntity = null;
                Globals.beachScene.RemoveEntity(otherEntity);

                otherEntity.GetComponent<Engine.TransformComponent>().position = new Vector2(260, 60);
                Globals.gameScene.GetCameraByName("main").SetWorldPosition(otherEntity.GetComponent<Engine.TransformComponent>().GetCenter(), instant: true);
                Globals.gameScene.GetCameraByName("minimap").SetWorldPosition(otherEntity.GetComponent<Engine.TransformComponent>().GetCenter(), instant: true);
                Globals.gameScene.AddEntity(otherEntity);
                Globals.gameScene.GetCameraByName("main").trackedEntity = otherEntity;
                Globals.gameScene.GetCameraByName("minimap").trackedEntity = otherEntity;

                EngineGlobals.sceneManager.transition = new FadeSceneTransition(Globals.gameScene, replaceScene: true);
            }
        }

        public static void homeOnCollisionEnter(Entity thisEntity, Entity otherEntity, float distance)
        {
            if (otherEntity.Tags.HasTag("player"))
            {

                Globals.homeScene.GetCameraByName("main").trackedEntity = null;
                Globals.homeScene.GetCameraByName("minimap").trackedEntity = null;
                Globals.homeScene.RemoveEntity(otherEntity);

                otherEntity.GetComponent<Engine.TransformComponent>().position = new Vector2(85, 90);
                TransformComponent thisTransform = thisEntity.GetComponent<Engine.TransformComponent>();
                float newX = thisTransform.GetCenter().X;
                //float newX = thisTransform.size.X; // Testing - size is 0!
                float newY = thisTransform.position.Y + thisTransform.size.Y;
                Console.WriteLine($"Building X:{thisTransform.position.X}  New Player X:{newX}");
                Console.WriteLine($"Building Y:{thisTransform.position.Y}  New Player Y:{newY}");
                //otherEntity.GetComponent<Engine.TransformComponent>().position = new Vector2(newX, newY);

                Globals.gameScene.GetCameraByName("main").SetWorldPosition(otherEntity.GetComponent<Engine.TransformComponent>().GetCenter(), instant: true);
                Globals.gameScene.GetCameraByName("minimap").SetWorldPosition(otherEntity.GetComponent<Engine.TransformComponent>().GetCenter(), instant: true);
                Globals.gameScene.AddEntity(otherEntity);
                Globals.gameScene.GetCameraByName("main").trackedEntity = otherEntity;
                Globals.gameScene.GetCameraByName("minimap").trackedEntity = otherEntity;

                EngineGlobals.sceneManager.transition = new FadeSceneTransition(Globals.gameScene, replaceScene: true);

            }
        }


        public Game1()
        {
            Globals.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            Globals.graphics.PreferredBackBufferWidth = Globals.WIDTH;
            Globals.graphics.PreferredBackBufferHeight = Globals.HEIGHT;
            Globals.graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {

            Globals.content = this.Content;
            Globals.graphicsDevice = GraphicsDevice;
            
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            // load the fonts
            Globals.font = Content.Load<SpriteFont>("File");
            Globals.fontSmall = Content.Load<SpriteFont>("small");

            Globals.sceneRenderTarget = new RenderTarget2D(
                Globals.graphicsDevice,
                Globals.graphicsDevice.PresentationParameters.BackBufferWidth,
                Globals.graphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                Globals.graphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24, 0, RenderTargetUsage.PreserveContents
            );

            Globals.lightRenderTarget = new RenderTarget2D(
                Globals.graphicsDevice,
                Globals.graphicsDevice.PresentationParameters.BackBufferWidth,
                Globals.graphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                Globals.graphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24, 0, RenderTargetUsage.PreserveContents
            );

            // Move to another accessible place for the menu and scenes?
            Globals.playerSpriteSheet = new Engine.SpriteSheet(Globals.content.Load<Texture2D>("playerSpriteSheet"), new Vector2(26, 36));

            // Instantiate the managers
            EngineGlobals.inputManager = new InputManager();
            EngineGlobals.componentManager = new ComponentManager();
            EngineGlobals.systemManager = new SystemManager();
            EngineGlobals.entityManager = new EntityManager();
            EngineGlobals.sceneManager = new SceneManager();
            EngineGlobals.playerManager = new PlayerManager();
            EngineGlobals.inventoryManager = new InventoryManager();

            //
            // create entities - MOVE to the required scenes
            //                  what about player, player bag etc?
            //

            // player entity
            //Engine.Entity playerEntity = PlayerEntity.Create(100, 100); // opposites (180, 350)
            string playerId = "player1";
            Engine.Entity playerEntity = PlayerEntity.Create(300, 200, playerId); // opposites (180, 350)


            // Player bag
            /*
            Engine.Entity playerBagEntity = EngineGlobals.entityManager.CreateEntity();
            playerBagEntity.Tags.AddTag("playerBag");
            playerBagEntity.AddComponent(new Engine.InventoryContainerComponent());
            EngineGlobals.playerManager.SetPlayer(playerBagEntity, playerId); // Assign bag to player1
            */
            /*
            // Test - add inventory items
            //string bagContainerId = playerBagEntity.GetComponent<InventoryContainerComponent>().ContainerId;
            string bagContainerId = playerEntity.GetComponent<InventoryContainerComponent>().ContainerId;
            //playerBagEntity.AddComponent(new Engine.InventoryItemComponent("stick", bagContainerId, 5));
            //playerBagEntity.AddComponent(new Engine.InventoryItemComponent("pebble", bagContainerId));
            Engine.Entity inventoryItemEntity1 = EngineGlobals.entityManager.CreateEntity();
            inventoryItemEntity1.AddComponent(new Engine.InventoryItemComponent("stick", bagContainerId, 5));
            Engine.Entity inventoryItemEntity2 = EngineGlobals.entityManager.CreateEntity();
            inventoryItemEntity2.AddComponent(new Engine.InventoryItemComponent("pebble", bagContainerId));

            // Player hotbar
            Engine.Entity playerHotbarEntity = EngineGlobals.entityManager.CreateEntity();
            playerHotbarEntity.Tags.AddTag("playerHotbar");
            playerHotbarEntity.AddComponent(new Engine.InventoryContainerComponent());
            EngineGlobals.playerManager.SetPlayer(playerHotbarEntity, playerId); // Assign hotbar to player1
            // Test - add hotbar items
            string hotbarContainerId = playerHotbarEntity.GetComponent<InventoryContainerComponent>().ContainerId;
            //playerHotbarEntity.AddComponent(new Engine.InventoryItemComponent("pebble", hotbarContainerId));
            Engine.Entity inventoryItemEntity3 = EngineGlobals.entityManager.CreateEntity();
            inventoryItemEntity3.AddComponent(new Engine.InventoryItemComponent("pebble", bagContainerId));


            // Chest test - MOVE to gameScene
            Engine.Entity chestEntity = EngineGlobals.entityManager.CreateEntity();
            chestEntity.Tags.AddTag("chest");
            chestEntity.AddComponent(new Engine.InventoryContainerComponent());
            string chestId = chestEntity.GetComponent<InventoryContainerComponent>().ContainerId;
            // Test - add chest items
            chestEntity.AddComponent(new Engine.InventoryItemComponent("arrowStandard", chestId, 20));
            chestEntity.AddComponent(new Engine.InventoryItemComponent("stick", chestId, 10));
            */

            // home entity
            Engine.Entity homeEntity = HomeEntity.Create(50, 20);
            // home light entity
            Engine.Entity homeLightEntity = HomeLightEntity.Create(150,75);
            // light switch entity
            Engine.Entity lightSwitchEntity = LightSwitchEntity.Create(120, 135);

            // enemy entity
            //Engine.Entity enemyEntity = EnemyEntity.Create(300, 480); // opposites (300, 483)
            // light source entity
            Engine.Entity lightSourceEntity = LightEntity.Create(300, 300);

            // item entity
            //Engine.Entity itemEntity = ItemEntity.

            // Test player movement
            //Engine.IntentionComponent pIntentionComponent = playerEntity.GetComponent<Engine.IntentionComponent>();
            //pIntentionComponent.up = true;
            //pIntentionComponent.left = true;

            // Test enemy movement
            //Engine.IntentionComponent eIntentionComponent = enemyEntity.GetComponent<Engine.IntentionComponent>();
            //eIntentionComponent.up = true;
            //eIntentionComponent.right = true;
            //enemyEntity.state = "walkSouth";


            // Map trigger entity
            Engine.Entity m = EngineGlobals.entityManager.CreateEntity();
            m.Tags.Id = "m";
            m.Tags.AddTag("mapTrigger");
            m.AddComponent(new Engine.TransformComponent(225, 0));
            m.AddComponent(new Engine.TriggerComponent(
                new Vector2(75, 30), new Vector2(0, 0),
                doorOnCollisionEnter,
                null,
                null
            ));

            // Beach trigger entity
            Engine.Entity b = EngineGlobals.entityManager.CreateEntity();
            b.Tags.Id = "b";
            m.Tags.AddTag("beachTrigger");
            b.AddComponent(new Engine.TransformComponent(475, 1000));
            b.AddComponent(new Engine.TriggerComponent(
                new Vector2(75, 30), new Vector2(0, 0),
                null,
                beachOnCollisionEnter,
                null
            ));

            // Home trigger entity
            Engine.Entity h = EngineGlobals.entityManager.CreateEntity();
            h.Tags.Id = "h";
            m.Tags.AddTag("homeTrigger");
            h.AddComponent(new Engine.TransformComponent(155, 135));
            h.AddComponent(new Engine.TriggerComponent(
                new Vector2(20, 10), new Vector2(0, 0),
                null,
                homeOnCollisionEnter,
                null
            ));

            // create scenes
            Globals.menuScene = new MenuScene();
            Globals.gameScene = new GameScene();
            Globals.homeScene = new HomeScene();
            Globals.beachScene = new BeachScene();

            EngineGlobals.sceneManager.transition = new FadeSceneTransition(Globals.menuScene);

        }

        protected override void Update(GameTime gameTime)
        {
            if (EngineGlobals.sceneManager.IsEmpty())
                Exit();
            EngineGlobals.inputManager.Update(gameTime);
            EngineGlobals.sceneManager.Update(gameTime);
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            EngineGlobals.sceneManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
