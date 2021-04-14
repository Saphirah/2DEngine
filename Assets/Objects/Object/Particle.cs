using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace GameJam.Assets
{
    public class Particle : Object
    {
        CTextureHandlerAnimated textureHandler;
        static List<Texture> frames = new List<Texture>()
        {
            new Texture("Explosions/Explosion_1.png"),
            new Texture("Explosions/Explosion_2.png"),
            new Texture("Explosions/Explosion_3.png"),
            new Texture("Explosions/Explosion_4.png"),
            new Texture("Explosions/Explosion_5.png"),
            new Texture("Explosions/Explosion_6.png"),
            new Texture("Explosions/Explosion_7.png")
        };

        protected override void Load()
        {
            base.Load();
            textureHandler = new CTextureHandlerAnimated(this, frames);
            textureHandler.layer = 3;
            textureHandler.framesPerSecond = 20;

            components.Add(textureHandler);

            textureHandler.OnSequenceFinished += DestroyOnFinish;

            Size = new Vector2f(60f, 60f);
            Origin = Size / 2;
        }

        protected override void Update()
        {
            base.Update();
            Size = new Vector2f((0.5f + lifetime) * 120f, (0.5f + lifetime) * 120f);
            Origin = Size / 2;
        }

        void DestroyOnFinish()
        {
            Game.DestroyObject(this);
        }

        public override void Destroy()
        {
            base.Destroy();
            textureHandler.OnSequenceFinished -= DestroyOnFinish;
        }
    }
}
