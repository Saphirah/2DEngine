using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using GameJam.Assets;
using SFML.Window;
using System.Dynamic;

namespace GameJam
{

    //The basic object
    public class Object : RectangleShape
    {
        float creationTime = Game.gameTime;
        public float lifetime { get { return Game.gameTime - creationTime; } }

        public static T Create<T>(Vector2f position, float rotation, Vector2f size) where T : Object, new()
        {
            T o = new T();
            o.Origin = size / 2;
            o.Position = position;
            o.Rotation = rotation;
            o.Size = size;
            Game.OnTick += o.Update;
            o.Load();
            return o;
        }
        public static T Create<T>(Vector2f position, float rotation) where T : Object, new()
        {
            return Create<T>(position, rotation, new Vector2f(50f, 50f));
        }
        public static T Create<T>(Vector2f position) where T : Object, new()
        {
            return Create<T>(position, 0f);
        }
        public static T Create<T>() where T : Object, new()
        {
            return Create<T>(new Vector2f(Game.width / 2, Game.height / 2));
        }

        protected virtual void Load() { }

        protected new virtual void Update() { }
    }

    public class TextureObject : Object
    {
        //Frames to animate the object. If the list is empty then it is ignored
        protected TextureHandler textureHandler;

        public virtual void Draw()
        {
            Game.SFMLWindow.Draw(this);
        }
    }

    public class TextureHandler
    {
        protected Texture texture;

        public TextureHandler(Texture texture)
        {
            this.texture = texture;
        }

        public virtual Texture GetTexture()
        {
            return texture;
        }
    }

    public class TextureHandlerAnimated : TextureHandler
    {
        public Action OnSequenceFinished;

        public List<Texture> frames = new List<Texture>();
        float framesPerSecond = 4;
        float frameOffset = Game.gameTime;
        int index;

        public TextureHandlerAnimated(Texture texture) : base(texture)
        {
            frames.Add(texture);
            Game.OnTick += Update;
        }
        public TextureHandlerAnimated(List<Texture> textures) : base(textures.Count > 0 ? textures[0] : null)
        {
            frames = textures;
            Game.OnTick += Update;
        }

        public virtual void Update()
        {
            if (frames.Count > 0)
            {
                int index = ((int)((Game.gameTime - frameOffset) * framesPerSecond)) % frames.Count;
                texture = frames[index];
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
    }

    public class TextureHandlerAnimatedStates : TextureHandlerAnimated
    {
        Dictionary<string, List<Texture>> states;
        string defaultState = "Idle";
        string currentState = "Idle";

        public TextureHandlerAnimatedStates(List<Texture> textures) : base(textures) 
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
                if(currentState == defaultState)
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

    public class CollisionObject : TextureObject
    {
        public Collider collider;

        protected override void Load()
        {
            base.Load();
            collider = new BoxCollider(this);
        }
    }

    public class MovableObject : CollisionObject
    {
        public Vector2f velocity;

        protected override void Update()
        {
            base.Update();
            Move(velocity * Game.deltaTime);
        }

        public void Move(Vector2f relativeDirection) { Move(relativeDirection, true); }
        public void Move(Vector2f relativeDirection, bool doCollisionCheck)
        {
            Position += relativeDirection;
            if (doCollisionCheck)
            {
                Collider collidedObject = collider.GetCollidingObject(CollisionType.Environment);
                if (collidedObject != null)
                {
                    Position -= relativeDirection;
                    collider.OnCollision?.Invoke(collider);
                    collidedObject.OnCollision?.Invoke(collidedObject);
                }
            }
        }
    }

    public class Player : MovableObject
    {

        public float speed = 100f;

        //2 Animations for walking and idle
        public static List<Texture> idleAnimation = new List<Texture>() {
            new Texture("Fairy1.png"),
            new Texture("Fairy2.png"),
            new Texture("Fairy3.png"),
            new Texture("Fairy4.png") };

        public static List<Texture> walkAnimation = new List<Texture>() {
            new Texture("Fairy1.png"),
            new Texture("Fairy2.png"),
            new Texture("Fairy3.png"),
            new Texture("Fairy4.png") };

        protected override void Load()
        {
            base.Load();
            TextureHandlerAnimatedStates textureHandler = new TextureHandlerAnimatedStates(idleAnimation);
            textureHandler.SetState("Walk", walkAnimation);
            textureHandler.SetState("Idle", idleAnimation);
            this.textureHandler = textureHandler;
        }

        protected override void Update()
        {
            velocity = new Vector2f(0f, 0f);

            //Keyboard Input
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                velocity += new Vector2f(0f, -speed);
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                velocity += new Vector2f(0f, speed);
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                velocity += new Vector2f(-speed, 0f);
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                velocity += new Vector2f(speed, 0f);

            //Rotate the player to the left and to the right
            if (velocity.X > 0 && Scale.X < 0)
                Scale = new Vector2f(1f, Scale.Y);
            else if (velocity.X < 0 && Scale.X > 0)
                Scale = new Vector2f(-1f, Scale.Y);

            //Set the animation to idle when the player does not move
            if (velocity.X == 0 && velocity.Y == 0)
                ((TextureHandlerAnimatedStates)textureHandler).SetDefaultState("Idle");
            else
                ((TextureHandlerAnimatedStates)textureHandler).SetDefaultState("Walk");

            base.Update();
        }
    }
}
