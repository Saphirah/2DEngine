using System;
using SFML.System;

namespace GameJam.Assets
{
    class CProjectileShooter_Circular : CProjectileShooter
    {
        public int projectileCount = 10;

        public CProjectileShooter_Circular(Object parent) : base(parent, 0f)
        {

        }

        protected override void Shoot()
        {
            for (int x = 0; x < projectileCount; x++)
            {
                Projectile projectile = (Projectile)pooler.InstantiateObject();
                projectile.Position = parent.Position;
                projectile.movementComponent.velocity = new Vector2f(MathF.Cos((2f / projectileCount * x) * MathF.PI), MathF.Sin((2f / projectileCount * x) * MathF.PI)) * projectileSpeed;
                projectile.collider.collisionType = collisionType;
            }
        }
    }
}
