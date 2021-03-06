using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace AdventureGame.Engine
{
    public class CollisionSystem : System
    {
        public HashSet<Entity> collisionStarted;
        public HashSet<Entity> collisionEnded;

        public CollisionSystem()
        {
            RequiredComponent<ColliderComponent>();
            RequiredComponent<TransformComponent>();

            collisionStarted = new HashSet<Entity>();
            collisionEnded = new HashSet<Entity>();
        }

        public override void UpdateEntity(GameTime gameTime, Scene scene, Entity entity)
        {

            ColliderComponent colliderComponent = entity.GetComponent<ColliderComponent>();
            TransformComponent transformComponent = entity.GetComponent<TransformComponent>();

            colliderComponent.rect = new Rectangle(
                (int)transformComponent.position.X + (int)colliderComponent.offset.X,
                (int)transformComponent.position.Y + (int)colliderComponent.offset.Y,
                (int)colliderComponent.size.X, (int)colliderComponent.size.Y
            );

            // check for collider intersects
            foreach (Entity otherE in scene.entityList)
            {
                if (entityList.Contains(otherE) && entity != otherE)
                {
                    ColliderComponent otherColliderComponent = otherE.GetComponent<ColliderComponent>();
                    TransformComponent otherTransformComponent = otherE.GetComponent<TransformComponent>();

                    if (otherColliderComponent != null && otherTransformComponent != null)
                    {

                        // Check if the entities have collided
                        if (colliderComponent.rect.Intersects(otherColliderComponent.rect))
                        {
                            // Check if the entities are already colliding
                            if (!(collisionStarted.Contains(entity)
                                && colliderComponent.collidedEntities.Contains(otherE)))
                            {
                                // Add the entity to the collision started set
                                collisionStarted.Add(entity);

                                // Add the collided entity to the component set
                                colliderComponent.collidedEntities.Add(otherE);

                                // Testing: change component outline colour
                                colliderComponent.color = Color.Orange;
                                otherColliderComponent.color = Color.Orange;

                                // Testing: output collided entities set
                                Console.WriteLine("\nCollision started set: ");
                                Console.WriteLine($"Entity {entity.id}");
                                foreach (Entity e in colliderComponent.collidedEntities)
                                    Console.WriteLine($"Other entity {e.id}  ");
                            }
                        }
                        // Check if the entities were colliding and now they are not
                        else if (collisionStarted.Contains(entity)
                            && colliderComponent.collidedEntities.Contains(otherE))
                        {
                            // Remove the collided entity from the component set
                            colliderComponent.collidedEntities.Remove(otherE);

                            // Check if the entity has any more collisions to handle
                            if (colliderComponent.collidedEntities.Count == 0)
                                // Remove the entity from the collision started set
                                collisionStarted.Remove(entity);

                            // Add the entities to the collision ended sets
                            collisionEnded.Add(entity);
                            colliderComponent.collidedEntitiesEnded.Add(otherE);

                            // Remove the other entity from the resolved collisions set
                            //colliderComponent.resolvedCollisions.Remove(otherE);

                            // Testing: output collided entities ended set
                            Console.WriteLine("\nCollision ended set: ");
                            Console.WriteLine($"Entity {entity.id}");
                            foreach (Entity e in colliderComponent.collidedEntitiesEnded)
                                Console.WriteLine($"Other entity {e.id}  ");
                            Console.WriteLine();
                        }
                        // Check if the entites are exiting a previous collision
                        else if (collisionEnded.Contains(entity)
                            && colliderComponent.collidedEntitiesEnded.Contains(otherE))
                        {
                            // Remove the collided entity from the component set
                            colliderComponent.collidedEntitiesEnded.Remove(otherE);

                            // Check if the entity has any more ended collisions to handle
                            if (colliderComponent.collidedEntitiesEnded.Count == 0)
                                // Remove the entity from the collision ended set
                                collisionEnded.Remove(entity);

                            // colliderComponent.collidingUp = false;
                            // OR
                            //colliderComponent.collidingDirection = "";

                            // Testing: change component outline colour
                            colliderComponent.color = Color.Yellow;
                            otherColliderComponent.color = Color.Yellow;
                        }
                    }
                }
            }
        }

        public HashSet<Entity> GetCollidedEntities(Entity entity)
        {
            ColliderComponent colliderComponent = entity.GetComponent<ColliderComponent>();
            return colliderComponent.collidedEntities;
        }

        public HashSet<Entity> GetCollidedEntitiesEnded(Entity entity)
        {
            ColliderComponent colliderComponent = entity.GetComponent<ColliderComponent>();
            return colliderComponent.collidedEntitiesEnded;
        }

        public void OnCollisionEnter(Entity e, Entity otherEntity)
        {

        }

        public override void DrawEntity(GameTime gameTime, Scene scene, Entity entity)
        {
            if (!EngineGlobals.DEBUG)
                return;

            ColliderComponent colliderComponent = entity.GetComponent<ColliderComponent>();
            
            Color color = colliderComponent.color;
            int lineWidth = 1;
            Globals.spriteBatch.DrawRectangle(colliderComponent.rect, color, lineWidth);
        }

    }
}
