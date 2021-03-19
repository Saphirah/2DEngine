using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System;
using System.Collections.Generic;
using GameJam.Assets;
using System.Linq;
using GameJam.Assets.Math;
using GameJam.Assets.Environment;

namespace GameJam
{
    public class Game
    {
        public static Action OnTick;

        //Window
        public static RenderWindow SFMLWindow;
        public static int width = 1600;
        public static int height = 800;
        public static float scale = 1f;
        public static string windowName = "Dungeon Crawler";
        public static Vector2f screenOffset = new Vector2f(0f, 0f);

        //Clock
        static Clock gameClock;
        public static float deltaTime;
        public static float gameTime;

        //Game Objects / Tiles
        public static List<Object> objects = new List<Object>();
        public static List<Object> uiobjects = new List<Object>();

        public static Player player;

        public static WorldGenerator worldGenerator;

        //Fonts
        static Font font = new Font("Early_GameBoy.ttf");

        public Game()
        {
            //Create the game window
            VideoMode vMode = new VideoMode((uint)width, (uint)height);
            SFMLWindow = new RenderWindow(vMode, windowName);
            SFMLWindow.SetFramerateLimit(144);
            gameClock = new Clock();

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
                UpdateObjects();
                SFMLWindow.Clear();
                DrawBackground();
                DrawObjects();
                DrawUI();
                SFMLWindow.Display();

                SFMLWindow.DispatchEvents();
            }
        }

        public static void InitializeObjects()
        {
            player = Object.Create<Player>();
            worldGenerator = WorldGenerator.instance;
        }

        public void UpdateObjects()
        {
            OnTick?.Invoke();
        }

        public void DrawObjects()
        {
            foreach (TextureObject o in objects.Reverse<Object>())
                o.Draw();
        }

        public void DrawUI()
        {
            foreach (Object o in uiobjects.Reverse<Object>())
                SFMLWindow.Draw(o);
        }

        public void DrawBackground()
        {
            RectangleShape ground = new RectangleShape();
            int tileSize = 5;
            ground.Size = new Vector2f(tileSize, tileSize);
            ValueNoise n = new ValueNoise();

            Vector2i offset = new Vector2i((int)MathF.Floor(screenOffset.X), (int)MathF.Floor(screenOffset.Y));

            for (int x = (int)MathF.Floor(screenOffset.X); x < width; x += tileSize)
                for (int y = (int)MathF.Floor(screenOffset.Y); y < height; y += tileSize)
                {
                    byte color = (byte)(int)(255 * n.Get(new Vector2f((float)(x) / 100 + Game.gameTime, (float)(y) / 100 + Game.gameTime) ));
                    ground.FillColor = new Color(color, color, color);
                    //ground.Texture = floorTiles[r.Next(floorTiles.Count())];
                    ground.Position = new Vector2f(x, y);
                    //ground.Position -= new Vector2f(ground.Size.X * ground.Scale.X - 50, ground.Size.Y * ground.Scale.Y - 50) / 2 - screenOffset;
                    SFMLWindow.Draw(ground);
                }
        }

        public static void DestroyObject(Object o)
        {
            objects.Remove(o);
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