using System;
using SFML.Graphics;
using SFML.System;

namespace GameJam.Assets
{
    public class CMovable : Component
    {
        public Action OnVelocityChange;

        Vector2f Velocity;
        public Vector2f velocity
        {
            get { return Velocity; }
            set
            {
                Velocity = value;
                OnVelocityChange?.Invoke();
            }
        }

        public CCollider collider { get; set; }

        public CMovable(Object parent) : base(parent)
        {
            Game.OnTick += Update;
        }

        protected virtual void Update()
        {
            Move(velocity * Game.deltaTime);
        }

        public void Move(Vector2f relativeDirection) { Move(relativeDirection, true); }
        public void Move(Vector2f relativeDirection, bool doCollisionCheck)
        {
            parent.Position += relativeDirection;
            if (doCollisionCheck && collider != null)
            {
                CCollider collidedObject = collider.GetCollidingObject(CollisionType.Environment);
                if (collidedObject != null)
                {
                    parent.Position -= relativeDirection;
                    collider.OnCollision?.Invoke(collider);
                    collidedObject.OnCollision?.Invoke(collidedObject);
                }
            }

            if (!parent.GetGlobalBounds().Intersects(new FloatRect(0, 0, Game.width, Game.height)))
                parent.SetActive(false);
        }

        public override void Destroy()
        {
            Game.OnTick -= Update;
        }
    }
}
