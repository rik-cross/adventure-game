﻿using AdventureGame.Engine;
using Microsoft.Xna.Framework;

using System;
using S = System.Diagnostics.Debug;

namespace AdventureGame
{
    public class MenuScene : Engine.Scene
    {
        public Engine.UIButton BtnContinue { get; private set; }

        private Engine.Text _title;
        private Engine.Image _keyboardImage;
        private Engine.Image _controllerImage;
        private Engine.Image _inputImage;
        private Engine.Text _inputText;
        private Engine.Text _versionText;

        private Engine.Camera _camera;
        private Engine.Entity _mainMenuPlayer;
        private Engine.Entity _mainMenuCharacter1;
        private int _nextCatch;
        private int _frameOdo;
        private Random _random;


        public override void Init()
        {
            EngineGlobals.DEBUG = false;

            UIButton.drawMethod = UICustomisations.DrawButton;

            AddMap("Maps/Map_MainMenu");

            _camera = new Engine.Camera(
                    name: "main",
                    size: new Vector2(Globals.ScreenWidth, Globals.ScreenHeight),
                    zoom: 4.0f,
                    backgroundColour: Color.Black
                );

            _camera.SetWorldPosition(new Vector2(1280, 864), instant: true);
            //camera.zoomIncrement = 0.005f;
            //camera.SetZoom(3.0f);
            //AddCamera(n);
            CameraList.Add(_camera);

            LightLevel = 1.0f;


            // Character sprites
            string dir = Globals.characterDir;
            string characterStr = Globals.playerStr;
            string baseStr = Globals.characterBaseStr;
            string toolStr = Globals.characterToolStr;
            string folder = "";
            string keyStr = "";
            Vector2 offset = new Vector2(-41, -21);

            //
            // Player fishing
            //
            _mainMenuPlayer = EngineGlobals.entityManager.CreateEntity();
            _mainMenuPlayer.AddComponent(new Engine.TransformComponent(new Vector2(1184, 870), new Vector2(15, 20)));
            //mainMenuPlayer.AddComponent(new Engine.ColliderComponent(new Vector2(15, 20)));

            Engine.AnimatedSpriteComponent animatedComponent = _mainMenuPlayer.AddComponent<AnimatedSpriteComponent>();
            CreatePlayerSprites();

            //string dir = "Characters/Players/long_hair/";
            //Vector2 offset = new Vector2(-41, -21);

            //Engine.SpriteComponent spriteComponent = mainMenuPlayer.AddComponent<Engine.SpriteComponent>();
            //spriteComponent.AddAnimatedSprite(dir + "spr_waiting_strip9", "waiting", 0, 8, offset: offset);
            //spriteComponent.AddAnimatedSprite(dir + "spr_casting_strip15", "casting", 0, 14, offset: offset);
            //spriteComponent.AddAnimatedSprite(dir + "spr_caught_strip10", "caught", 0, 9, offset: offset);

            //spriteComponent.GetSprite("casting").OnComplete = (Engine.Entity e) => e.State = "waiting";
            //spriteComponent.GetSprite("caught").OnComplete = (Engine.Entity e) => e.State = "casting";

            _mainMenuPlayer.State = "casting";

            _random = new Random();
            _nextCatch = _random.Next(1500, 5000);
            _frameOdo = 0;

            AddEntity(_mainMenuPlayer);


            //
            // Character swimming
            //
            _mainMenuCharacter1 = EngineGlobals.entityManager.CreateEntity();
            _mainMenuCharacter1.AddComponent<Engine.TransformComponent>(new Engine.TransformComponent(new Vector2(1400, 920), new Vector2(15, 20)));
            //mainMenuCharacter1.AddComponent(new Engine.ColliderComponent(new Vector2(15, 20)));

            Engine.AnimatedSpriteComponent animatedComponentC1 = _mainMenuCharacter1.AddComponent<AnimatedSpriteComponent>();

            // Swimming
            folder = "SWIMMING/";
            keyStr = "_swimming_strip12.png";
            characterStr = "bowlhair";
            animatedComponentC1.AddAnimatedSprite(dir + folder + baseStr + keyStr,
                "swimming", 0, 11, offset: offset, flipH: true);
            animatedComponentC1.AddAnimatedSprite(dir + folder + characterStr + keyStr,
                "swimming", 0, 11, offset: offset, flipH: true);
            animatedComponentC1.AddAnimatedSprite(dir + folder + toolStr + keyStr,
                "swimming", 0, 11, offset: offset, flipH: true);

            //string dirPlayer2 = "Characters/Players/";
            //Vector2 offsetPlayer2 = new Vector2(-41, -21);

            //Engine.SpriteComponent spriteComponentP2 = mainMenuCharacter1.AddComponent<Engine.SpriteComponent>();
            //spriteComponentP2.AddAnimatedSprite(dirPlayer2 + "spr_swimming_strip12", "swimming", 0, 11, offset: offsetPlayer2, flipH: true);

            _mainMenuCharacter1.State = "swimming";

            AddEntity(_mainMenuCharacter1);


            // title text
            _title = new Engine.Text(
                caption: "Fortuna",
                font: Theme.FontTitle,
                colour: Color.White,
                anchor: Anchor.TopCenter,
                padding: new Padding(top: 100),
                outline: true,
                outlineColour: Color.Black,
                outlineThickness: 8
            );

            // title text
            _inputText = new Engine.Text(
                caption: "Keyboard controls",
                font: Theme.FontSecondary,
                colour: Color.White,
                anchor: Anchor.BottomLeft,
                padding: new Padding(bottom: 0, left: 15),
                outline: true,
                outlineColour: Color.Black,
                outlineThickness: 4
            );

            // title text
            _versionText = new Engine.Text(
                caption: "v0.0",
                font: Theme.FontSecondary,
                colour: Color.White,
                anchor: Anchor.BottomRight,
                padding: new Padding(bottom: 0, right: 15),
                outline: true,
                outlineColour: Color.Black,
                outlineThickness: 4
            );

            UIMenu.AddUIElement(
                new UIButton(
                    position: new Vector2((Globals.ScreenWidth / 2) - 70, Globals.ScreenHeight - 300),
                    size: new Vector2(140, 45),
                    text: "New Game",
                    textColour: Color.White,
                    outlineColour: Color.White,
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    func: LoadNewGameScene
                )
            );

            BtnContinue = new UIButton(
                position: new Vector2((Globals.ScreenWidth / 2) - 70, Globals.ScreenHeight - 250),
                size: new Vector2(140, 45),
                text: "Continue",
                textColour: Color.White,
                outlineColour: Color.White,
                outlineThickness: 2,
                backgroundColour: Color.DarkSlateGray,
                func: LoadContinueGameScene,
                active: false
            );
            UIMenu.AddUIElement(BtnContinue);

            UIMenu.AddUIElement(
                new UIButton(
                    position: new Vector2((Globals.ScreenWidth / 2) - 70, Globals.ScreenHeight - 200),
                    size: new Vector2(140, 45),
                    text: "Options",
                    textColour: Color.White,
                    outlineColour: Color.White,
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    func: LoadOptionsScene
                )
            );

            UIMenu.AddUIElement(
                new UIButton(
                    position: new Vector2((Globals.ScreenWidth / 2) - 70, Globals.ScreenHeight - 150),
                    size: new Vector2(140, 45),
                    text: "Credits",
                    textColour: Color.White,
                    outlineColour: Color.White,
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    func: LoadCreditsScene
                )
            );

            UIMenu.AddUIElement(
                new UIButton(
                    position: new Vector2((Globals.ScreenWidth / 2) - 70, Globals.ScreenHeight - 100),
                    size: new Vector2(140, 45),
                    text: "Quit",
                    textColour: Color.White,
                    outlineColour: Color.White,
                    outlineThickness: 2,
                    backgroundColour: Color.DarkSlateGray,
                    func: UnloadMenuScene
                )
            );


            // control images
            _controllerImage = new Engine.Image(
                Utils.LoadTexture("UI/xbox360.png"),
                size: new Vector2(118, 76),
                anchor: Anchor.BottomLeft,
                padding: new Padding(bottom: 30, left: 30)
            );
            _keyboardImage = new Engine.Image(
                Utils.LoadTexture("UI/keyboard.png"),
                size: new Vector2(198, 63),
                anchor: Anchor.BottomLeft,
                padding: new Padding(bottom: 30, left: 30)
            );

        }

        public void LoadNewGameScene(UIButton button)
        {
            // todo - bug!! button events can be fired multiple times if spamming enter
            // have an option to disable a button temporarily after Action executed??

            Globals.newGame = true;

            // Reset the VillageScene
            EngineGlobals.sceneManager.ResetScene<VillageScene>();

            // Reset the player components
            PlayerEntity.RemoveComponents();  // move to PlayerManager??
            PlayerEntity.AddComponents();
            Console.WriteLine(string.Join(", ", EngineGlobals.entityManager.GetLocalPlayer().Components));

            // Reset the player character default sprite
            Globals.playerIndex = 0;
            Globals.playerStr = Globals.allCharacters[0];
            PlayerEntity.UpdateSprites();  // move to PlayerManager??

            // Transition to the PlayerSelectScene and load the VillageScene below
            EngineGlobals.sceneManager.ChangeScene<
                FadeSceneTransition, VillageScene, PlayerSelectScene>(false);

            // Position the player
            Entity player = EngineGlobals.entityManager.GetLocalPlayer();
            Vector2 playerPosition = new Vector2(680, 580);
            if (player != null)
                player.GetComponent<TransformComponent>().Position = playerPosition;

            // Clear necessary player components if a game has already been started
            if (player.GetComponent<TutorialComponent>() != null)
                player.GetComponent<TutorialComponent>().ClearTutorials();

        }

        public void LoadContinueGameScene(UIButton button)
        {
            EngineGlobals.sceneManager.ChangeScene<FadeSceneTransition, VillageScene>(false);
        }

        public void LoadOptionsScene(UIButton button)
        {
            EngineGlobals.sceneManager.ChangeScene<
                FadeSceneTransition, OptionsScene>(false);
        }

        public void LoadCreditsScene(UIButton button)
        {
            EngineGlobals.sceneManager.ChangeScene<
                FadeSceneTransition, CreditsScene>(false);
        }

        public void UnloadMenuScene(UIButton button)
        {
            EngineGlobals.soundManager.Volume = 0;
            EngineGlobals.sceneManager.UnloadAllScenes();
        }

        public override void OnEnter()
        {
            EngineGlobals.DEBUG = false;

            if (Globals.newGame)
            {
                // todo - check if most of this is needed? move to VillageScene OnEnter

                EngineGlobals.soundManager.PlaySongFade(Utils.LoadSong("Music/citadel.ogg"));

                if (EngineGlobals.entityManager.GetLocalPlayer().GetComponent<InputComponent>().TopControllerLabel == "dialogue")
                {
                    EngineGlobals.entityManager.GetLocalPlayer().GetComponent<InputComponent>().PopController();
                    EngineGlobals.entityManager.GetLocalPlayer().GetComponent<InputComponent>().TopControllerLabel = "";
                }
                EngineGlobals.entityManager.GetLocalPlayer().Reset();
                EngineGlobals.entityManager.GetLocalPlayer().RemoveComponent<EmoteComponent>();
                EngineGlobals.entityManager.GetLocalPlayer().RemoveComponent<AnimatedEmoteComponent>();
                EngineGlobals.entityManager.GetLocalPlayer().GetComponent<DialogueComponent>().dialoguePages.Clear();
                EngineGlobals.entityManager.GetLocalPlayer().GetComponent<DialogueComponent>().alpha.Set(0.0);
                EngineGlobals.entityManager.GetLocalPlayer().State = "idle_right";

                if (EngineGlobals.entityManager.GetLocalPlayer().GetComponent<InputComponent>().Input == Engine.Inputs.keyboard)
                    //inputImage = keyboardImage;
                    _inputText.Caption = "Keyboard";
                else
                    //inputImage = controllerImage;
                    _inputText.Caption = "Controller";
            }
            else
            {
                // Re-create the player sprites in case player has changed
                CreatePlayerSprites();
            }
        }

        public override void OnExit()
        {

        }
        public override void Input(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            _frameOdo++;

            //S.WriteLine(frameOdo + " " + nextCatch);
            if (_frameOdo == _nextCatch)
            {
                _frameOdo = 0;
                _nextCatch = (int)_random.Next(1500, 5000);
                _mainMenuPlayer.State = "caught";
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _title.Draw();
            _inputText.Draw();
            _versionText.Draw();
        }

        // Used to change the player style and re-create the sprites
        public void CreatePlayerSprites()
        {
            AnimatedSpriteComponent animatedComponent = _mainMenuPlayer.GetComponent<AnimatedSpriteComponent>();
            animatedComponent.ClearAllAnimatedSprites();
            Console.WriteLine($"Main menu animated sprites {animatedComponent.AnimatedSprites.Count}");

            // Character sprites
            string dir = Globals.characterDir;
            string characterStr = Globals.playerStr;
            string baseStr = Globals.characterBaseStr;
            string toolStr = Globals.characterToolStr;
            string folder = "";
            string keyStr = "";
            Vector2 offset = new Vector2(-41, -21);

            // Testing
            //characterStr = "spikeyhair";

            // Waiting
            folder = "WAITING/";
            keyStr = "_waiting_strip9.png";
            animatedComponent.AddAnimatedSprite(dir + folder + baseStr + keyStr,
                "waiting", 0, 8, offset: offset);
            animatedComponent.AddAnimatedSprite(dir + folder + characterStr + keyStr,
                "waiting", 0, 8, offset: offset);
            animatedComponent.AddAnimatedSprite(dir + folder + toolStr + keyStr,
                "waiting", 0, 8, offset: offset);

            // Casting
            folder = "CASTING/";
            keyStr = "_casting_strip15.png";
            animatedComponent.AddAnimatedSprite(dir + folder + baseStr + keyStr,
                "casting", 0, 14, offset: offset);
            animatedComponent.AddAnimatedSprite(dir + folder + characterStr + keyStr,
                "casting", 0, 14, offset: offset);
            animatedComponent.AddAnimatedSprite(dir + folder + toolStr + keyStr,
                "casting", 0, 14, offset: offset);
            animatedComponent.GetAnimatedSprite("casting").OnComplete = (Engine.Entity e) => e.State = "waiting";

            // Caught
            folder = "CAUGHT/";
            keyStr = "_caught_strip10.png";
            animatedComponent.AddAnimatedSprite(dir + folder + baseStr + keyStr,
                "caught", 0, 9, offset: offset);
            animatedComponent.AddAnimatedSprite(dir + folder + characterStr + keyStr,
                "caught", 0, 9, offset: offset);
            animatedComponent.AddAnimatedSprite(dir + folder + toolStr + keyStr,
                "caught", 0, 9, offset: offset);
            animatedComponent.GetAnimatedSprite("caught").OnComplete = (Engine.Entity e) => e.State = "casting";
        }

    }

}
