using System;
using SFML.System;
using GameJam.Assets.Optimization;
using GameJam.Assets;

namespace GameJam.Assets
{
    public class CProjectileShooter : Component
    {
        public Action OnAngleChange;

        public float shootTime = 0.1f;
        public float timer = 0f;
        private float Angle = 0f;
        public float angle
        {
            get
            {
                return Angle;
            }
            set
            {
                Angle = value;
                OnAngleChange?.Invoke();
            }
        }
        public float projectileSpeed = 400f;
        public float projectileDamage = 100f;
        public CollisionType collisionType = CollisionType.None;

        protected ObjectPooler pooler = new ObjectPooler();

        public CProjectileShooter(Object parent, float angle) : base(parent)
        {
            Game.OnTick += Update;
            this.angle = angle;
        }

        protected virtual void Update()
        {
            timer -= Game.deltaTime;
            if (timer < 0)
            {
                timer = shootTime;
                Shoot();
            }
        }

        protected virtual void Shoot()
        {
            Projectile projectile = (Projectile)pooler.InstantiateObject();
            projectile.Position = parent.Position;
            projectile.movementComponent.velocity = new Vector2f(MathF.Cos((angle + 90) / 180 * MathF.PI) * projectileSpeed, MathF.Sin((angle + 90) / 180 * MathF.PI) * projectileSpeed);
            projectile.collider.collisionType = collisionType;
        }

        public override void Destroy()
        {
            Game.OnTick -= Update;
        }
    }
}
