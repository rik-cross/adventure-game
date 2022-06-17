﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using AdventureGame.Engine;

namespace AdventureGame
{
    public class Game1 : Game
    {
        public EntityManager entityManager; // REMOVE? reference from EngineGlobals
        public SceneManager sceneManager;

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

            // Instantiate the managers
            EngineGlobals.inputManager = new InputManager();
            EngineGlobals.componentManager = new ComponentManager();
            EngineGlobals.systemManager = new SystemManager();
            EngineGlobals.entityManager = new EntityManager();
            EngineGlobals.sceneManager = new SceneManager();

            //
            // create entities
            //

            // player entity
            Engine.Entity playerEntity = PlayerEntity.Create(180, 50);
            // enemy entity
            Engine.Entity enemyEntity = EnemyEntity.Create(-50, 150);
            // light source entity
            Engine.Entity lightSourceEntity = LightEntity.Create(300, 300);


            // Test player movement
            Engine.IntentionComponent pIntentionComponent = playerEntity.GetComponent<Engine.IntentionComponent>();
            //pIntentionComponent.up = true;
            //pIntentionComponent.left = true;

            // Test enemy movement
            Engine.IntentionComponent eIntentionComponent = enemyEntity.GetComponent<Engine.IntentionComponent>();
            eIntentionComponent.up = true;
            eIntentionComponent.right = true;
            //enemyEntity.state = "walkSouth";

            MenuScene menuScene = new MenuScene();
            EngineGlobals.sceneManager.PushScene(menuScene);

        }

        protected override void Update(GameTime gameTime)
        {
            if (EngineGlobals.sceneManager.isEmpty())
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
