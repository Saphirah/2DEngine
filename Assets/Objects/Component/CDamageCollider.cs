using System;
using SFML.System;
using GameJam.Assets.Optimization;

namespace GameJam.Assets
{
    class CDamageCollider : Component
    {
        public CDamageCollider(Object parent) : base(parent) 
        {
            
        }

        public override void Destroy() {}
    }
}
