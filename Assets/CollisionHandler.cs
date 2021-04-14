using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJam.Assets
{
    public enum CollisionType { None, Player, Enemies, Environment }

    public abstract class CCollider : Component
    {
        public CollisionType collisionType { get; set; }
        public Action<CCollider> OnCollision { get; set; }

        public List<CollisionType> collidingTypes 
        {
            get 
            {
                switch (collisionType)
                {
                    case CollisionType.Enemies:
                        return new List<CollisionType>() { CollisionType.Environment, CollisionType.Player };
                    case CollisionType.Player:
                        return new List<CollisionType>() { CollisionType.Environment, CollisionType.Enemies };
                    case CollisionType.Environment:
                         return new List<CollisionType>() { CollisionType.Environment, CollisionType.Enemies, CollisionType.Player };
                    default:
                        return new List<CollisionType>();
                }
            }
        }

        public CCollider(Object parent) : base(parent) 
        {
            Game.OnTick += Update;
        }

        public override void Destroy()
        {
            Game.OnTick -= Update;
        }

        public void Update()
        {
            List<CCollider> colliders = GetCollidingObjects();

            foreach(CCollider collider in colliders) {
                OnCollision?.Invoke(collider);
                collider.OnCollision?.Invoke(this);
            }
        }

        public abstract bool IsColliding();
        public abstract bool IsColliding(CollisionType ct);
        public abstract CCollider GetCollidingObject();
        public abstract CCollider GetCollidingObject(CollisionType ct);
        public abstract List<CCollider> GetCollidingObjects();
        public abstract List<CCollider> GetCollidingObjects(CollisionType ct);
    }

    public class CBoxCollider : CCollider
    {
        public CBoxCollider(Object parent) : base(parent) { }

        public override bool IsColliding()
        {
            return GetCollidingObject() != null;
        }

        public override bool IsColliding(CollisionType ct)
        {
            return GetCollidingObject(ct) != null;
        }

        public override CCollider GetCollidingObject()
        {
            List<CollisionType> collidingTypes = this.collidingTypes;
            FloatRect ownBounds = GetBoundingBox(parent);
            foreach (Object o in Game.objects)
                if (o != parent)
                    foreach (CCollider c in o.components)
                        if (collidingTypes.Contains(c.collisionType))
                        {
                            FloatRect otherBounds = GetBoundingBox(o);
                            if (ownBounds.Intersects(otherBounds))
                                return c;
                        }
            return null;
        }

        public override CCollider GetCollidingObject(CollisionType ct)
        {
            FloatRect ownBounds = GetBoundingBox(parent);
            foreach (Object o in Game.objects)
                if(o != parent)
                    foreach (CCollider c in o.components)
                        if (c.collisionType == ct)
                        {
                            FloatRect otherBounds = GetBoundingBox((RectangleShape)o);
                            if (ownBounds.Intersects(otherBounds))
                                return c;
                        }
            return null;
        }

        public override List<CCollider> GetCollidingObjects()
        {
            List<CCollider> collidingObjects = new List<CCollider>();
            List<CollisionType> collidingTypes = this.collidingTypes;
            FloatRect ownBounds = GetBoundingBox(parent);
            foreach (Object o in Game.objects)
                if (o != parent)
                    foreach (CCollider c in o.GetComponents<CCollider>())
                        if (collidingTypes.Contains(c.collisionType))
                        {
                            FloatRect otherBounds = GetBoundingBox(o);
                            if (ownBounds.Intersects(otherBounds))
                                collidingObjects.Add(c);
                        }
            return collidingObjects;
        }

        public override List<CCollider> GetCollidingObjects(CollisionType ct)
        {
            List<CCollider> collidingObjects = new List<CCollider>();
            FloatRect ownBounds = GetBoundingBox(parent);
            foreach (Object o in Game.objects)
                if (o != parent)
                    foreach (CCollider c in o.GetComponents<CCollider>())
                        if (c.collisionType == ct)
                        {
                            FloatRect otherBounds = GetBoundingBox((RectangleShape)o);
                            if (ownBounds.Intersects(otherBounds))
                                collidingObjects.Add(c);
                        }
            return collidingObjects;
        }

        public FloatRect GetBoundingBox(RectangleShape o)
        {
            return o.GetGlobalBounds();
        }

    }
}
