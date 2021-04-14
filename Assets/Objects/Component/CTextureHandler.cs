using System;
using SFML.Graphics;

namespace GameJam.Assets
{
    public class CTextureHandler : Component
    {
        public Action OnTextureChanged;

        public Texture texture { get; protected set; }
        public int layer = 0;

        public CTextureHandler(Object parent, Texture texture) : base(parent)
        {
            SetTexture(texture);
            Game.OnRenderObjects += Draw;
        }

        public virtual void SetTexture(Texture texture)
        {
            this.texture = texture;
            parent.Texture = texture;
            parent.TextureRect = new IntRect(0, 0, (int)texture.Size.X, (int)texture.Size.Y);
            OnTextureChanged?.Invoke();
        }

        public virtual void Draw(int layer)
        {
            if (layer == this.layer)
            {
                if (parent.Texture != texture)
                    parent.Texture = texture;
                parent.Position += Game.screenOffset;
                Game.SFMLWindow.Draw(parent);
                parent.Position -= Game.screenOffset;
            }
        }

        public override void Destroy()
        {
            Game.OnRenderObjects -= Draw;
        }
    }
}
