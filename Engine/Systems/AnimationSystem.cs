using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;

namespace AdventureGame.Engine
{
    public class AnimationSystem : System
    {
        public AnimationSystem()
        {
            RequiredComponent<AnimationComponent>();
        }

        public override void UpdateEntity(GameTime gameTime, Scene scene, Entity entity)
        {
            AnimationComponent animationComponent = entity.GetComponent<AnimationComponent>();

            string animationState = entity.state;

            animationComponent.animation.Play(animationState);
            animationComponent.animation.Update(gameTime);
        }

        public override void DrawEntity(GameTime gameTime, Scene scene, Entity entity)
        {
            AnimationComponent animationComponent = entity.GetComponent<AnimationComponent>();
            TransformComponent transformComponent = entity.GetComponent<TransformComponent>();

            if (animationComponent == null || transformComponent == null)
                return;

            Globals.spriteBatch.Draw(animationComponent.animation, transformComponent.position);
        }
    }
}
