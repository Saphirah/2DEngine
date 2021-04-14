using SFML.Graphics;
using SFML.System;

namespace GameJam.Assets
{
    public class Player : Object
    {
        protected override void Load()
        {
            base.Load();

            CTextureHandler textureHandler = new CTextureHandler(this, new Texture("space_breaker_asset/Ships/Medium/body_01.png"));
            textureHandler.layer = 1;
            CBoxCollider collider = new CBoxCollider(this);
            collider.collisionType = CollisionType.Player;
            CProjectileShooter projectileShooter = new CProjectileShooter(this, 180);
            projectileShooter.collisionType = CollisionType.Player;

            components.Add(textureHandler);
            components.Add(new CMovableInput(this));
            components.Add(collider);
            components.Add(projectileShooter);

            Size = new Vector2f(40f, 80f);
            Origin = Size / 2;
            Object.Create<PlayerCollider>();
        }
    }
}
