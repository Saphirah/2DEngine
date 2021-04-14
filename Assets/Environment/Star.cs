using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

//namespace GameJam.Assets.Environment
//{
//    class Star : MovableObject
//    {
//        static Random r = new Random();

//        protected override TextureHandler CreateTextureHandler()
//        {
//            return new TextureHandler(new Texture("space_breaker_asset/Background/Star" + r.Next(1,4) + ".png"));
//        }

//        protected override void Load()
//        {
//            base.Load();

//            float s = (float)r.NextDouble()+1f;
//            Size = new Vector2f(5f * s, 5f * s);
//            Position = new Vector2f(Game.width * (float)r.NextDouble(), Game.height * (float)r.NextDouble());
//            velocity = new Vector2f(0f, 40 * s);
//        }

//        protected override void Update()
//        {
//            if (Game.height + Size.Y < Position.Y)
//            {
//                Position = new Vector2f(Game.width * (float)r.NextDouble(), 0);
//            }
//            base.Update();
//        }
//    }
//}
