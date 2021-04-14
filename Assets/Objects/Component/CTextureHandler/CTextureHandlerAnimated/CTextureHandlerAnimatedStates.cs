using System.Collections.Generic;
using SFML.Graphics;

namespace GameJam.Assets
{
    public class CTextureHandlerAnimatedStates : CTextureHandlerAnimated
    {
        Dictionary<string, List<Texture>> states = new Dictionary<string, List<Texture>>();
        string defaultState = "Idle";
        string currentState = "Idle";

        public CTextureHandlerAnimatedStates(Object parent, List<Texture> textures) : base(parent, textures)
        {
            states[defaultState] = textures;
        }

        public void SetState(string name, List<Texture> frames)
        {
            states[name] = frames;
        }
        public bool ExistsState(string name)
        {
            return states.ContainsKey(name);
        }
        public void PlayState(string name)
        {
            if (ExistsState(name))
            {
                PlayAnimation(states[name]);
                currentState = name;
                OnSequenceFinished += ResetState;
            }
        }
        public void SetDefaultState(string name)
        {
            if (ExistsState(name))
            {
                if (currentState == defaultState)
                {
                    PlayState(name);
                }

                defaultState = name;
            }
        }
        public void ResetState()
        {
            PlayAnimation(states[defaultState]);
            OnSequenceFinished -= ResetState;
        }
    }
}
