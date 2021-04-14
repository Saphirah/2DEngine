using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;


namespace GameJam
{
    public class Object : RectangleShape
    {
        public Action<Object, bool> OnActivate;
        public Action OnDestroy;

        float creationTime = Game.gameTime;
        public float lifetime { get { return Game.gameTime - creationTime; } }

        public List<Component> components = new List<Component>();

        public Object() { }

        public static T Create<T>(Vector2f position, float rotation, Vector2f size) where T : Object, new()
        {
            T o = new T();
            o.Position = position;
            o.Rotation = rotation;
            o.Size = size;
            o.Origin = size / 2;
            Game.OnTick += o.Update;
            Game.objects.Add(o);
            o.Load();
            return o;
        }
        public static T Create<T>(Vector2f position, float rotation) where T : Object, new()
        {
            return Create<T>(position, rotation, new Vector2f(50f, 50f));
        }
        public static T Create<T>(Vector2f position) where T : Object, new()
        {
            return Create<T>(position, 0f);
        }
        public static T Create<T>() where T : Object, new()
        {
            return Create<T>(new Vector2f(Game.width / 2, Game.height / 2));
        }

        protected virtual void Load() { }

        protected new virtual void Update() { }

        public bool IsActive()
        {
            return Game.objects.Contains(this);
        }

        public void SetActive(bool active, int index)
        {
            if (active)
                if (index < 0)
                    Game.objects.Add(this);
                else
                    Game.objects.Insert(index, this);
            else
                Game.objects.Remove(this);
            OnActivate?.Invoke(this, active);
        }

        public void SetActive(bool active)
        {
            SetActive(active, -1);
        }

        public virtual void Destroy()
        {
            OnDestroy?.Invoke();
            Game.OnTick -= Update;
            components = new List<Component>();
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component t in components)
                if (t is T)
                    return (T)t;
            return null;
        }

        public List<T> GetComponents<T>() where T : Component
        {
            List<T> components = new List<T>();
            foreach (Component component in this.components)
                if (component is T)
                    components.Add((T)component);
            return components;
        }
    }
}
