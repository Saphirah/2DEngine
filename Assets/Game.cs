using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using GameJam.Assets;
using System.Linq;
using GameJam.Assets.Environment;
using GameJam.Assets.Audio;

namespace GameJam
{
    public class Game
    {
        public static Action OnTick;
        public static Action OnRenderBackground;
        public static Action<int> OnRenderObjects;
        public static Action OnRenderUI;

        //Window
        public static RenderWindow SFMLWindow;
        public static int width = 1600;
        public static int height = 800;
        public static float scale = 1f;
        public static string windowName = "Dungeon Crawler";
        public static Vector2f screenOffset = new Vector2f(0f, 0f);

        public static int renderLayers = 4;

        //Clock
        static Clock gameClock;
        public static float deltaTime;
        public static float gameTime;

        //Game Objects / Tiles
        public static List<Object> objects = new List<Object>();
        public static List<Object> uiobjects = new List<Object>();

        public static Player player;

        public static WorldGenerator worldGenerator;

        static RectangleShape ground = new RectangleShape();
        public static Random random = new Random();
        public static FastNoise noise = new FastNoise();

        public static SoundManager soundManager = new SoundManager();
        public static EnemyManager enemyManager = new EnemyManager();

        //Fonts
        static Font font = new Font("Early_GameBoy.ttf");

        public Game()
        {
            //Create the game window
            VideoMode vMode = new VideoMode((uint)width, (uint)height);
            SFMLWindow = new RenderWindow(vMode, windowName);
            SFMLWindow.SetFramerateLimit(144);
            SFMLWindow.SetMouseCursorVisible(false);
            gameClock = new Clock();

            OnRenderBackground += DrawBackground;
            OnRenderUI += DrawUI;

            //Start the game loop
            GameLoop();
        }

        public void GameLoop()
        {
            InitializeObjects();
            while (!Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                //Set time values
                deltaTime = gameClock.Restart().AsSeconds();
                gameTime += deltaTime;

                //Update and draw all objects
                OnTick?.Invoke();

                SFMLWindow.Clear();
                    OnRenderBackground?.Invoke();
                    for(int x = 0; x < renderLayers; x++)
                        OnRenderObjects?.Invoke(x);
                    OnRenderUI?.Invoke();
                SFMLWindow.Display();

                SFMLWindow.DispatchEvents();
            }
        }

        public static void InitializeObjects()
        {
            player = Object.Create<Player>();
            worldGenerator = WorldGenerator.instance;
            //for (int x = 0; x < 100; x++)
            //    objects.Add(Object.Create<Star>());
            ground.Size = new Vector2f(Game.width, Game.height);
            ground.Texture = new Texture("Nebulas/Nebula.png");
            
        }

        public void DrawUI()
        {
            foreach (Object o in uiobjects.Reverse<Object>())
                SFMLWindow.Draw(o);
        }

        public void DrawBackground()
        {
            for (int y = 0; y < 2; y++)
            {
                ground.Position = new Vector2f(0, (Game.gameTime * 10) % height - (height * y)) + Game.screenOffset;
                SFMLWindow.Draw(ground);
            }
        }

        public static void DestroyObject(Object o)
        {
            objects.Remove(o);
            o.Destroy();
        }
    }



    //This class creates a box over the screen and fades out, then restarts the level
    public class ScreenFade : Object
    {
        Action OnFinish;

        float alpha = 0f;
        float direction = 1f;
        float seconds = 1f;

        protected override void Load()
        {
            base.Load();
            FillColor = new Color(0, 0, 0, 0);
            Size = new Vector2f(Game.width, Game.height);
            OnFinish += Finish;
            Game.OnTick -= Update;
        }

        protected override void Update()
        {
            base.Update();
            alpha += Game.deltaTime * 255 * direction / seconds;
            alpha = MathF.Min((int)alpha, 255);
            FillColor = new Color(0, 0, 0, (byte)alpha);

            if (alpha == 255)
                OnFinish?.Invoke();
        }

        protected virtual void Finish() 
        {
            Game.OnTick -= Update;
        }

        public void FadeIn(float seconds)
        {
            direction = -1f;
            this.seconds = seconds;
            Game.OnTick += Update;
        }

        public void FadeOut(float seconds)
        {
            direction = 1f;
            this.seconds = seconds;
            Game.OnTick += Update;
        }
    }
}