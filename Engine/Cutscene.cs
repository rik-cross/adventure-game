﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace AdventureGame.Engine
{
    public class Cutscene
    {
        private static List<Action> _actionList = new List<Action>();
        private static List<bool> _waitList = new List<bool>();

        private static int _actionIndex = 0;
        private static float _delayDuration = 0f;
        private static float _delayStartTime = 0f;
        private static float _currentTime = 0f;
        private static int _fadePercentage = 0;

        /*public Cutscene()
        {
            _actionList = new List<Action>();
            _waitList = new List<bool>();

            _actionIndex = 0;
            _currentDelay = 0;
            _fadePercentage = 0;

            //Test();
        }*/

        public static void Test()
        {
            Entity playerEntity = EngineGlobals.entityManager.GetLocalPlayer();
            Entity npcEntity = EngineGlobals.entityManager.GetEntityByIdTag("blacksmith");

            AddAction(() => Fade(2));
            AddAction(() => SetDelayDuration(4)); // check - does the 4 sec delay not include the 2 sec fade?
            AddAction(() => MoveCharacter(playerEntity, 50, 0, 4), false);
            AddAction(() => SetDelayDuration(1)); // check - NPC should move after 1 second whilst player is moving
            AddAction(() => MoveCharacter(npcEntity, 10, 20, 2));
            AddAction(() => SetDelayDuration(6));
            AddAction(() => Fade(3));
            // check - is another delay needed?
        }

        // Set delay
        public static void SetDelayDuration(float amount)
        {
            _delayDuration = amount;
        }

        public static void SetDelayStartTime()
        {
            Console.WriteLine($"Delay start time: {_currentTime}");
            //_currentTime = currentTime;
            _delayStartTime = _currentTime;
        }

        public static void ClearDelay()
        {
            _delayDuration = 0f;
            _delayStartTime = 0f;
        }

        public static bool IsDelayActive()
        {
            float secondsPassed = _currentTime - _delayStartTime;
            Console.WriteLine($"Delay active at {_currentTime}: {secondsPassed} secs passed out of {_delayDuration}");
            //if (_delayDuration == 0f || _delayStartTime == 0f)
            //    return false;
            if (_currentTime - _delayStartTime >= _delayDuration)
                return false;
            return true;
        }

        public static void Advance()
        {
            _actionIndex += 1;
        }

        public static void Reset()
        {
            _actionList.Clear();
            _waitList.Clear();
            _actionIndex = 0;
            _delayDuration = 0f;
            _delayStartTime = 0f;
            //ClearDelay();
            _fadePercentage = 0;
        }

        public static void AddAction(Action action, bool waitToComplete = true)
        {
            _actionList.Add(action);
            _waitList.Add(waitToComplete);
        }

        public static void Update(GameTime gameTime)
        {
            if (_actionList.Count == 0)
                return;

            _currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check if there is an action to execute
            if (_actionIndex < _actionList.Count)
            {
                if (_delayDuration > 0f && _delayStartTime == 0f)
                    SetDelayStartTime();

                if (IsDelayActive())
                    return;

                //if (_delayDuration > 0)
                //    _delayDuration -= 1;

                if (_delayDuration <= 0)
                {
                    ClearDelay(); // Here or in IsDelayActive or elsewhere?

                    // Execute the action
                    _actionList[_actionIndex]();

                    // Should the next action wait for the current action to complete
                    bool wait = _waitList[_actionIndex];

                    // Advance the cutscene
                    Advance();

                    // Repeat while the next actions are executed concurrently
                    while (!wait)
                    {
                        if (_actionIndex < _actionList.Count)
                        {
                            _actionList[_actionIndex]();
                            wait = _waitList[_actionIndex];
                            Advance();
                        }
                    }
                }

            }

            // Check if the end of the cutscene has been reached
            if (_actionIndex >= _actionList.Count)
                Reset();
        }

        public static void Draw(GameTime gameTime)
        {

        }

        // Testing
        public static void Fade(int time)
        {
            Console.WriteLine($"Fade: time {time}");
        }

        public static void MoveCharacter(Entity entity, float xAmount, float yAmount, int time)
        {
            Console.WriteLine($"Move: entity {entity.Id}, X {xAmount}, Y {yAmount}, time {time}");
        }
    }
}