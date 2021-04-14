using SFML.System;
using SFML.Graphics;

namespace GameJam.Assets
{
    public class PlayerCollider : Object
    {
        protected override void Load()
        {
            base.Load();

            CTextureHandler textureHandler = new CTextureHandler(this, new Texture("Projectiles/Core1.png"));
            textureHandler.layer = 1;

            CBoxCollider collider = new CBoxCollider(this);
            collider.collisionType = CollisionType.Player;

            components.Add(textureHandler);
            components.Add(new CMovableInput(this));
            components.Add(collider);

            Size = new Vector2f(25f, 25f);
            Origin = Size / 2;

            collider.OnCollision += KillPlayer;
        }

        protected override void Update()
        {
            base.Update();
            Rotation += Game.deltaTime * 180;
        }

        public void KillPlayer(CCollider collider)
        {
            Game.DestroyObject(Game.player);
            Game.DestroyObject(this);
            Object.Create<Particle>(Position);
        }

        public override void Destroy()
        {
            GetComponent<CCollider>().OnCollision -= KillPlayer;
            ScreenShake.StartShake(1.5f, 40f, 1f);
            base.Destroy();
        }
    }
}
