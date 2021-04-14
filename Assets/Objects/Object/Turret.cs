using SFML.Graphics;
using System;

namespace GameJam.Assets
{
    public class Turret : Object
    {
        public Action OnParentChange;

        private CProjectileShooter ProjectileShooter;
        protected CProjectileShooter projectileShooter
        {
            get
            {
                return ProjectileShooter;
            }
            set
            {
                if (ProjectileShooter != null)
                    ProjectileShooter.OnAngleChange -= UpdateAngle;
                ProjectileShooter = value;
                ProjectileShooter.OnAngleChange += UpdateAngle;
                UpdateAngle();
            }
        }
        protected CCopyLocation anchor;
        private Object Parent;
        public Object parent
        {
            get
            {
                return Parent;
            }
            set
            {
                Parent = value;
                anchor.target = value;
                OnParentChange?.Invoke();
            }
        }

        protected override void Load()
        {
            base.Load();
            anchor = new CCopyLocation(this);
        }

        private void UpdateAngle()
        {
            Rotation = projectileShooter.angle + 180;
        }

        public override void Destroy()
        {
            base.Destroy();
            if(projectileShooter != null)
                ProjectileShooter.OnAngleChange -= UpdateAngle;
        }
    }
}
