using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameJam.Assets.Audio
{
    public class SoundManager
    {
        public Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();

        public SoundManager()
        {
            Game.OnTick += Update;
            sounds["backgroundMusic"] = PlaySound("Sounds/maintheme.wav", 10, true);
        }

        public Sound PlaySound(string sound, float volume, bool loop)
        {
            Sound s = new Sound(LoadSound(sound));
            s.Loop = loop;
            s.Volume = volume;
            s.Play();
            return s;
        }
        public Sound PlaySound(string sound, float volume)
        {
            return PlaySound(sound, volume, false);
        }
        public Sound PlaySound(string sound)
        {
            return PlaySound(sound, 10);
        }

        public SoundBuffer LoadSound(string sound)
        {
            return new SoundBuffer(sound);
        }

        public void Update()
        {

        }
    }
}
