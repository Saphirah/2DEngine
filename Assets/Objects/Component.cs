namespace GameJam
{
    public abstract class Component
    {
        public Object parent;

        public Component(Object parent)
        {
            this.parent = parent;
            parent.OnDestroy += Destroy;
        }

        public virtual void Destroy() { }
    }
}
