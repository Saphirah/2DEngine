using System;
using System.Collections.Generic;
using System.Text;

namespace GameJam.Assets.Optimization
{
    public class ObjectPooler
    {
        List<Object> activeObjects = new List<Object>();
        List<Object> inactiveObjects = new List<Object>();

        private static Dictionary<Type, ObjectPooler> poolers = new Dictionary<Type, ObjectPooler>();

        public static ObjectPooler GetPooler<T>() where T : Object
        {
            Type type = typeof(T);
            if (!poolers.ContainsKey(type))
                poolers[type] = new ObjectPooler();
            return poolers[type];
        }

        public Type type = typeof(T);

        public Object InstantiateObject()
        {
            Object o;
            if (inactiveObjects.Count > 0)
            {
                o = inactiveObjects[0];
                inactiveObjects.Remove(o);
            }
            else
                o = CreateObject();
            activeObjects.Add(o);
            o.SetActive(true);
            o.OnActivate += ActivateObject;
            return o;
        }

        public void ActivateObject(Object o, bool active)
        {
            if (!active)
            {
                o.OnActivate -= ActivateObject;
                DeactivateObject(o);
            }
        }

        public void DeactivateObject(Object o)
        {
            if (activeObjects.Contains(o))
            {
                activeObjects.Remove(o);
                o.SetActive(false);
                inactiveObjects.Add(o);
            }
        }

        protected virtual Object CreateObject() 
        {
            T p = Object.Create<T>();
            //Remove it from the Games objects because the object is handled by the pool
            Game.objects.Remove(p);
            return p;
        }
    }
}
