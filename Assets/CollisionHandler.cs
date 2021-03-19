using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJam.Assets
{
    public enum CollisionType { None, Player, Entity, Environment }

    public interface Collider
    {
        public CollisionType collisionType { get; set; }
        public Action<Collider> OnCollision { get; set; }

        public bool IsColliding();
        public bool IsColliding(CollisionType ct);
        public Collider GetCollidingObject();
        public Collider GetCollidingObject(CollisionType ct);
        public List<Object> GetCollidingObjects();
        public List<Object> GetCollidingObjects(CollisionType ct);
    }

    public class BoxCollider : Collider
    {
        TextureObject Parent;

        public Action<Collider> OnCollision { get; set; }
        public CollisionType collisionType { get; set; }


        public BoxCollider(TextureObject parent)
        {
            Parent = parent;
        }


        public bool IsColliding()
        {
            return GetCollidingObject() != null;
        }

        public bool IsColliding(CollisionType ct)
        {
            return GetCollidingObject(ct) != null;
        }

        public Collider GetCollidingObject()
        {
            FloatRect ownBounds = GetBoundingBox(Parent);
            foreach (CollisionObject o in Game.objects)
                if (o.collider != this)
                {
                    FloatRect otherBounds = GetBoundingBox(o);
                    if (ownBounds.Intersects(otherBounds))
                        return o.collider;
                }
            return null;
        }

        public Collider GetCollidingObject(CollisionType ct)
        {
            FloatRect ownBounds = GetBoundingBox(Parent);
            foreach (CollisionObject o in Game.objects)
                if (o.collider != null)
                    if (o.collider.collisionType == ct)
                    {
                        FloatRect otherBounds = GetBoundingBox(o);
                        if (ownBounds.Intersects(otherBounds))
                            return o.collider;
                    }
            return null;
        }

        public List<Object> GetCollidingObjects()
        {
            List<Object> collidingObjects = new List<Object>();
            FloatRect ownBounds = GetBoundingBox(Parent);
            foreach (CollisionObject o in Game.objects)
                if (o.collider != null)
                {
                    FloatRect otherBounds = GetBoundingBox(o);
                    if (ownBounds.Intersects(otherBounds))
                        collidingObjects.Add(o);
                }
            return collidingObjects;
        }

        public List<Object> GetCollidingObjects(CollisionType ct)
        {
            List<Object> collidingObjects = new List<Object>();
            FloatRect ownBounds = GetBoundingBox(Parent);
            foreach (CollisionObject o in Game.objects)
                if (o.collider != null)
                    if (o.collider.collisionType == ct)
                    {
                        FloatRect otherBounds = GetBoundingBox(o);
                        if (ownBounds.Intersects(otherBounds))
                            collidingObjects.Add(o);
                    }
            return collidingObjects;
        }

        public FloatRect GetBoundingBox(RectangleShape o)
        {
            //o.Position -= new Vector2f(o.Size.X * o.Scale.X, o.Size.Y * o.Scale.Y) / 2;
            FloatRect ownBounds = o.GetGlobalBounds();
            //o.Position += new Vector2f(o.Size.X * o.Scale.X, o.Size.Y * o.Scale.Y) / 2;
            return ownBounds;
        }
    }
}
