using System;
using SFML.System;

namespace GameJam.Assets
{
    class CProjectileShooter_Rotating : CProjectileShooter
    {

        public float rotationSpeed = 45f;
        public CProjectileShooter_Rotating(Object parent) : base(parent, 0f) {}
        public CProjectileShooter_Rotating(Object parent, float angle) : base(parent, angle) {}

        protected override void Update()
        {
            base.Update();
            angle += Game.deltaTime * rotationSpeed;
        }
    }
}
