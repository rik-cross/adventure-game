﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using AdventureGame.Engine;

namespace AdventureGame
{
    public class Game1 : Game
    {
        public EntityManager entityManager;
        public SceneManager sceneManager;

        public Game1()
        {
            Globals.graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
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

            EngineGlobals.systems.Add(new InputSystem());
            //EngineGlobals.systems.Add(new ControlSystem()); // broken: needs ref to input action
            EngineGlobals.systems.Add(new SpriteSystem());
            EngineGlobals.systems.Add(new AnimationSystem());
            EngineGlobals.systems.Add(new PhysicsSystem());
            EngineGlobals.systems.Add(new HitboxSystem());
            EngineGlobals.systems.Add(new HurtboxSystem());
            EngineGlobals.systems.Add(new DamageSystem());
            EngineGlobals.systems.Add(new CollisionSystem());
            EngineGlobals.systems.Add(new TriggerSystem());
            EngineGlobals.systems.Add(new TextSystem());

            entityManager = new EntityManager();

            sceneManager = new SceneManager();
            //sceneManager.PushScene(new GameScene());
            sceneManager.PushScene(new GameScene(entityManager));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (sceneManager.isEmpty())
                Exit();

            sceneManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            sceneManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
