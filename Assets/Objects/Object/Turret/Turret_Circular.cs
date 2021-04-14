using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;

namespace GameJam.Assets
{
    class Turret_Circular : Turret
    {
        protected override void Load()
        {
            base.Load();
            CTextureHandler ct = new CTextureHandler(this, new Texture("space_breaker_asset/Weapons/Medium/Cannon/turret_01_mk2.png"));
            ct.layer = 2;
            components.Add(ct);
            projectileShooter = new CProjectileShooter_Circular(this);
            projectileShooter.shootTime = 3f;
            projectileShooter.projectileSpeed = 100f;
            projectileShooter.collisionType = CollisionType.Enemies;
            components.Add(projectileShooter);        
        }
    }
}
