using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;

namespace GameJam.Assets
{
    class Turret_DoubleFire : Turret
    {
        protected override void Load()
        {
            base.Load();
            CTextureHandler ct = new CTextureHandler(this, new Texture("space_breaker_asset/Weapons/Medium/Cannon/turret_01_mk2.png"));
            ct.layer = 2;
            components.Add(ct);
            for (int x = 0; x < 2; x++)
            {
                projectileShooter = new CProjectileShooter(this, 0f);
                projectileShooter.shootTime = 1f;
                projectileShooter.projectileSpeed = 100f;
                projectileShooter.collisionType = CollisionType.Enemies;
                projectileShooter.timer += x * 0.25f;
                components.Add(projectileShooter);
            }
        }
    }
}
