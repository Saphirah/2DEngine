using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;

namespace GameJam.Assets
{
    class Turret_SingleFire : Turret
    {
        protected override void Load()
        {
            base.Load();
            CTextureHandler ct = new CTextureHandler(this, new Texture("space_breaker_asset/Weapons/Medium/Cannon/turret_01_mk1.png"));
            ct.layer = 2;
            components.Add(ct);
            projectileShooter = new CProjectileShooter(this, 0f);
            projectileShooter.shootTime = 1f;
            projectileShooter.projectileSpeed = 100f;
            projectileShooter.collisionType = CollisionType.Enemies;
            components.Add(projectileShooter);
        }
    }
}
