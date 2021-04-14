using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace GameJam.Assets
{
    public class CTextureHandlerAnimated : CTextureHandler
    {
        public Action OnSequenceFinished;

        public List<Texture> frames = new List<Texture>();
        public float framesPerSecond = 4;
        float frameOffset = Game.gameTime;
        int index;

        public CTextureHandlerAnimated(Object parent, Texture texture) : base(parent, texture)
        {
            frames.Add(texture);
            Game.OnTick += Update;
        }
        public CTextureHandlerAnimated(Object parent, List<Texture> textures) : base(parent, textures.Count > 0 ? textures[0] : null)
        {
            frames = textures;
            Game.OnTick += Update;
        }

        public virtual void Update()
        {
            if (frames.Count > 0)
            {
                int index = ((int)((Game.gameTime - frameOffset) * framesPerSecond)) % frames.Count;
                SetTexture(frames[index]);
                if (index == 0 && index != this.index)
                    OnSequenceFinished?.Invoke();
                this.index = index;
            }
        }

        public void PlayAnimation(List<Texture> animationSequence)
        {
            frameOffset = Game.gameTime;
            frames = animationSequence;
        }

        public override void Destroy()
        {
            base.Destroy();
            Game.OnTick -= Update;
        }
    }
}
