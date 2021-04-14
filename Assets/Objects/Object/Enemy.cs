using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace GameJam.Assets
{
    public class Enemy : Object
    {
        Vector2f anchorPosition;
        CBoxCollider boxCollider;
        protected List<Turret> turrets = new List<Turret>();

        protected override void Load()
        {
            base.Load();

            CTextureHandler textureHandler = new CTextureHandler(this, new Texture("space_breaker_asset/Ships/Medium/body_01.png"));
            textureHandler.layer = 1;
            Turret tsf = Object.Create<Turret_Rotating>();
            tsf.parent = this;
            turrets.Add(tsf);

            boxCollider = new CBoxCollider(this);
            boxCollider.collisionType = CollisionType.Enemies;
            boxCollider.OnCollision += Kill;

            components.Add(textureHandler);
            components.Add(boxCollider);


            Size = new Vector2f(40f, 80f);
            Origin = Size / 2;
            Rotation = 180;
            anchorPosition = new Vector2f(Game.random.Next(200, Game.width - 200), Game.random.Next(200, Game.height / 2));
            Position = anchorPosition + new Vector2f(0, -1000);
        }

        protected override void Update()
        {
            base.Update();
            Vector2f targetPosition = anchorPosition + new Vector2f(
                Game.noise.GetValue(anchorPosition.X + Game.gameTime * 30, anchorPosition.Y) * 150,
                Game.noise.GetValue(anchorPosition.X, anchorPosition.Y + Game.gameTime * 30) * 150
            );
            Position += (targetPosition - Position) * Game.deltaTime;
        }

        void Kill(CCollider collider)
        {
            Game.DestroyObject(this);
            Game.DestroyObject(collider.parent);
        }

        public override void Destroy()
        {
            base.Destroy();
            boxCollider.OnCollision -= Kill;
            Object.Create<Particle>(Position);
            ScreenShake.StartShake(0.4f, 20f, 1f);
            foreach (Turret t in turrets)
                Game.DestroyObject(t);
        }
    }
}
