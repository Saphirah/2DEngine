using SFML.Graphics;
using SFML.System;

namespace GameJam.Assets
{
    public class T : Projectile
    {
        protected override void Load()
        {
            base.Load();
            CTextureHandler textureHandler = new CTextureHandler(this, new Texture("Projectiles/Projectile1.png"));
            textureHandler.layer = 1; 
            components.Add(textureHandler);
        }
    }
}
