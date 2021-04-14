using SFML.System;
using SFML.Window;

namespace GameJam.Assets
{
    public class CMovableInput : CMovable
    {
        public CMovableInput(Object parent) : base(parent) { }

        protected override void Update()
        {
            Vector2i mousePos = Mouse.GetPosition(Game.SFMLWindow);
            parent.Position = new Vector2f(mousePos.X, mousePos.Y);
            base.Update();
        }
    }
}
