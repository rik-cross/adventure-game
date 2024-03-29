﻿using Microsoft.Xna.Framework;

namespace AdventureGame.Engine
{
    public class InputSystem : System
    {
        public InputSystem()
        {
            RequiredComponent<InputComponent>();
            RequiredComponent<IntentionComponent>();
        }

        public override void InputEntity(GameTime gameTime, Scene scene, Entity entity)
        {
            InputComponent inputComponent = entity.GetComponent<InputComponent>();
            IntentionComponent intentionComponent = entity.GetComponent<IntentionComponent>();
            
            if (EngineGlobals.sceneManager.Transition != null)
            {
                intentionComponent.Reset();
                //entity.State = "idle";
                return;
            }
            
            if (inputComponent.InputControllerStack.Count > 0)
                inputComponent.PeekController().Invoke(entity);
        
        }
    }
}
