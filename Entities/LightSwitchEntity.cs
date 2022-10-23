﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;

using System.Collections.Generic;

using AdventureGame.Engine;

namespace AdventureGame
{
    public static class LightSwitchEntity
    {

        public static void lightOnCollide(Entity thisEntity, Entity otherEntity, float distance)
        {
            if (otherEntity.IsPlayerType())
            {
                InputComponent playerInputComponent = otherEntity.GetComponent<InputComponent>();
                if (playerInputComponent != null && EngineGlobals.inputManager.IsPressed(playerInputComponent.input.button1))
                    EngineGlobals.entityManager.GetEntityById("homeLight1").GetComponent<LightComponent>().visible = !EngineGlobals.entityManager.GetEntityById("homeLight1").GetComponent<LightComponent>().visible;
            }
        }

        public static Engine.Entity Create(int x, int y)
        {
            Entity entity = EngineGlobals.entityManager.CreateEntity();

            entity.Tags.Id = "lightSwitch1"; // REMOVE
            entity.Tags.AddTag("lightSwitch");

            entity.AddComponent(new Engine.TransformComponent(new Vector2(x, y), new Vector2(8, 8)));
            entity.AddComponent(new Engine.SpriteComponent("lightSwitch"));
            entity.AddComponent(new TriggerComponent(
                new Vector2(8, 8),
                new Vector2(0, 0),
                onCollide: lightOnCollide
            ));

            return entity;
        }
    }
}
