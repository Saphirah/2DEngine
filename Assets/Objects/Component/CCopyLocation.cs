using System.Runtime;

namespace GameJam.Assets
{
    public class CCopyLocation : Component
    {
        public Object target;
        public CCopyLocation(Object parent) : base(parent)
        {
            Game.OnTick += Update;
        }

        public void Update()
        {
            if(target != null)
                parent.Position = target.Position;
        }

        public override void Destroy()
        {
            base.Destroy();
            Game.OnTick -= Update;
        }
    }
}
