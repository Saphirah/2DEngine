
using SFML.System;
using System;
using System.Runtime.CompilerServices;

namespace GameJam.Assets
{
    public static class Vector2Extension
    {
        public static float Distance(this Vector2f v1, Vector2f v2)
        {
            return MathF.Sqrt(MathF.Pow(v1.X - v2.X, 2) + MathF.Pow(v1.Y - v2.Y, 2));
        }

        public static float Distance(this Vector2f v1, Vector2i v2)
        {
            return MathF.Sqrt(MathF.Pow(v1.X - v2.X, 2) + MathF.Pow(v1.Y - v2.Y, 2));
        }

        public static float Distance(this Vector2i v1, Vector2f v2)
        {
            return MathF.Sqrt(MathF.Pow(v1.X - v2.X, 2) + MathF.Pow(v1.Y - v2.Y, 2));
        }

        public static float Distance(this Vector2i v1, Vector2i v2)
        {
            return MathF.Sqrt(MathF.Pow(v1.X - v2.X, 2) + MathF.Pow(v1.Y - v2.Y, 2));
        }
    }
}
