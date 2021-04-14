using SFML.System;
using System;
using System.Collections.Generic;

namespace GameJam.Assets
{
    class ScreenShake
    {
        public Action OnShakeEnd;

        public float strength = 5f;
        public float speed = 1f;
        public bool loop = false;
        private float Duration = 1f;
        public float duration 
        {
            get
            {
                return Duration;
            }
            set
            {
                Duration = value;
                remainingDuration = value;
            }
        }

        public int seed;

        protected static List<ScreenShake> shakes = new List<ScreenShake>();

        protected float remainingDuration = 1f;
        protected Vector2f appliedMovement = new Vector2f();

        public ScreenShake()
        {
            Game.OnTick += Update;
            seed = Game.random.Next(0, 100000);
        }

        public static ScreenShake StartShake(float duration, float strength, float speed)
        {
            ScreenShake s = StartShake();
            s.duration = duration;
            s.strength = strength;
            s.speed = speed;
            return s;
        }
        public static ScreenShake StartShake(float duration, float strength)
        {
            ScreenShake s = StartShake();
            s.duration = duration;
            s.strength = strength;
            return s;
        }
        public static ScreenShake StartShake(float duration)
        {
            ScreenShake s = StartShake();
            s.duration = duration;
            return s;
        }
        public static ScreenShake StartShake()
        {
            return new ScreenShake();
        }

        public void Update()
        {
            remainingDuration -= Game.deltaTime;
            Game.screenOffset -= appliedMovement;
            if(remainingDuration <= 0f)
            {
                Game.OnTick -= Update;
                shakes.Remove(this);
                OnShakeEnd?.Invoke();
                return;
            } 
            appliedMovement = new Vector2f(Game.noise.GetValue(Game.gameTime * speed * 5000f, seed), Game.noise.GetValue(seed, Game.gameTime * speed * 5000f)) * strength * (remainingDuration / duration);
            Game.screenOffset += appliedMovement;
        }
    }
}
