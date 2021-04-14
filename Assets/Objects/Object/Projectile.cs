using System;
using SFML.System;

namespace GameJam.Assets
{
    public class Projectile : Object
    {
        public CMovable movementComponent;
        public CBoxCollider collider;

        protected override void Load()
        {
            base.Load();
            
            collider = new CBoxCollider(this);
            collider.collisionType = CollisionType.Player;
            Game.OnTick -= collider.Update;

            movementComponent = new CMovable(this);
            movementComponent.OnVelocityChange += RotateTowardsVelocity;
            movementComponent.velocity = new Vector2f(0f, -200f);
            
            components.Add(new CMovable(this));
            components.Add(collider);

            Size = new Vector2f(20f, 10f);
            Origin = Size / 2;
        }

        void RotateTowardsVelocity()
        {
            Vector2f vNorm = movementComponent.velocity / MathF.Max(MathF.Abs(movementComponent.velocity.X), MathF.Abs(movementComponent.velocity.Y));
            Rotation = MathF.Atan2(vNorm.Y, vNorm.X) * 180 / MathF.PI;
        }

        public override void Destroy()
        {
            base.Destroy();
            movementComponent.OnVelocityChange -= RotateTowardsVelocity;
        }
    }
}
