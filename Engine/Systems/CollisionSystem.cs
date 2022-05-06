﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;

namespace AdventureGame.Engine
{
    public class CollisionSystem : System
    {
        readonly ComponentManager componentManager; // REMOVE once SystemManager handles the relevant entity lists?

        public CollisionSystem()
        {
            componentManager = EngineGlobals.componentManager;

            // MOVE to SystemManager?
            // Create a signature that flags which components the system requires
            string[] requiredComponents = {
                "ColliderComponent",
                "TransformComponent"};

            systemSignature = componentManager.CreateSignature(requiredComponents);
        }

        public override void UpdateEntity(GameTime gameTime, Scene scene, Entity entity)
        {
            // CHANGE to a list of all relevant entitys (from SystemManager?)
            // and perform the check there and/or in Scene._update()

            // Compare the entity signature to system signature
            if (!entity.CheckComponents(entity.signature, systemSignature))
                return;

            //Console.WriteLine(entity.Id + "   " + entity.Signature);

            ColliderComponent colliderComponent = entity.GetComponent<ColliderComponent>();
            TransformComponent transformComponent = entity.GetComponent<TransformComponent>();

            /*
            if (colliderComponent == null || transformComponent == null)
                return;
            */

            // track entity here or elsewhere? - EngineGlobals DEBUG?
            // CHECK why can't components be passed as parameters? Eg TrackEntity(ColliderComponent colliderComponent, TransformComponent transformComponent)
            Vector2 newPosition = transformComponent.position;
            int w = colliderComponent.rectangle.Width;
            int h = colliderComponent.rectangle.Height;
            colliderComponent.rectangle.X = (int)newPosition.X - (int)(w / 2) + colliderComponent.xOffset;
            colliderComponent.rectangle.Y = (int)newPosition.Y - (int)(h / 2) + colliderComponent.yOffset;

            // check for collider intersects
            foreach (Entity e in scene.entities)
            {
                if (entity != e)
                {
                    ColliderComponent eColliderComponent = e.GetComponent<ColliderComponent>();
                    TransformComponent eTransformComponent = e.GetComponent<TransformComponent>();

                    if (eColliderComponent != null && eTransformComponent != null)
                        if (colliderComponent.rectangle.Intersects(eColliderComponent.rectangle))
                        {
                            colliderComponent.color = Color.Orange;
                            eColliderComponent.color = Color.Orange;

                            if (colliderComponent.active)
                            {
                                // set both entities states to collide
                                colliderComponent.collidedEntityId = e.id; // or list or Guid?
                                eColliderComponent.collidedEntityId = entity.id;
                                Console.WriteLine($"Entity {entity.id} collided with {e.id}"); // Testing

                                // change to OnCollisionEnter / OnCollision / OnCollisionExit?
                                colliderComponent.active = false;
                                colliderComponent.active = false;
                                // FIX currently collision with entity 2 only registers one way

                                // return; or keep checking & handle multiple collisions?
                            }
                        }
                }
            }
        }

        public override void DrawEntity(GameTime gameTime, Scene scene, Entity entity)
        {
            ColliderComponent colliderComponent = entity.GetComponent<ColliderComponent>();
            TransformComponent transformComponent = entity.GetComponent<TransformComponent>();

            if (colliderComponent == null || transformComponent == null)
                return;

            // TESTING draw collider rectangle outline
            Rectangle rectangle = colliderComponent.rectangle;
            Color color = colliderComponent.color;
            int lineWidth = 1;
            DrawRectangleOutline(rectangle, color, lineWidth);
        }

        // TESTING draw rectangle outline
        public void DrawRectangleOutline(Rectangle rectangle, Color color, int lineWidth)
        {
            Texture2D pointTexture = new Texture2D(Globals.spriteBatch.GraphicsDevice, 1, 1);
            pointTexture.SetData<Color>(new Color[] { Color.White });

            Globals.spriteBatch.Draw(pointTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height), color);
            Globals.spriteBatch.Draw(pointTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, lineWidth), color);
            Globals.spriteBatch.Draw(pointTexture, new Rectangle(rectangle.X + rectangle.Width - lineWidth, rectangle.Y, lineWidth, rectangle.Height), color);
            Globals.spriteBatch.Draw(pointTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - lineWidth, rectangle.Width, lineWidth), color);
        }

    }
}
