using SFML.System;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GameJam.Assets.Math
{
    public abstract class Noise
    {
        public int seed = 1;
        public float scale = 1000f;

        public abstract float Get(Vector2f position);
        public abstract float Get(float position);

        public int GetSeed(float position)
        {
            return (int)(10000 * position * seed * scale);
        }

        public int GetSeed(Vector2f position)
        {
            return (int)(76543 * position.X * scale + position.Y * 12345 * scale) * seed;
        }

        protected float smoothstep(float t)
        {
            return t * t * (3 - 2 * t);
        }

        protected float Lerp(float pos1, float pos2, float alpha)
        {
            return pos1 * (1 - alpha) + pos2 * alpha;
        }
    }

    public class ValueNoise : Noise
    {
        public override float Get(Vector2f position)
        {
            Vector2f delta = new Vector2f(position.X % 1, position.Y % 1);
            Random r = new Random();
            float pos1 = (float)new Random(GetSeed(new Vector2f(MathF.Floor(position.X), MathF.Floor(position.Y)))).NextDouble();
            float pos2 = (float)new Random(GetSeed(new Vector2f(MathF.Floor(position.X), MathF.Ceiling(position.Y)))).NextDouble();
            float pos3 = (float)new Random(GetSeed(new Vector2f(MathF.Ceiling(position.X), MathF.Floor(position.Y)))).NextDouble();
            float pos4 = (float)new Random(GetSeed(new Vector2f(MathF.Ceiling(position.X), MathF.Ceiling(position.Y)))).NextDouble();

            delta = new Vector2f(smoothstep(delta.X), smoothstep(delta.Y));

            float nx0 = Lerp(pos1, pos3, delta.X);
            float nx1 = Lerp(pos2, pos4, delta.X);

            return Lerp(nx0, nx1, delta.Y);
        }

        public override float Get(float position)
        {
            float delta = position % 1;
            float pos1 = (float)new Random(GetSeed(MathF.Floor(position))).NextDouble();
            float pos2 = (float)new Random(GetSeed(MathF.Ceiling(position))).NextDouble();
            return Lerp(pos1, pos2, delta);
        }
    }
}
