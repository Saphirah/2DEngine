using System;
using System.Collections.Generic;
using System.Text;

namespace GameJam.Assets.Enemies
{
    //public class Exploder : MovableObject
    //{
    //    //Settings
    //    public int maxProjectiles = 10;
    //    public float speed = 90;
    //    public float shootTime = 6f;
    //    public bool hasSpawned;

    //    public override void Initialize(Vector2f position, float rotation, Vector2f size)
    //    {
    //        base.Initialize(position, rotation, size);
    //        this.Texture = new Texture("Eye.png");
    //        Size = new Vector2f(100, 100);
    //        damage = 1;
    //        collisionType = Collision.Monster;
    //        Random r = new Random();
    //        //Spawns the monster at a random position, but not too close tOneo the corners
    //        Position = new Vector2f(r.Next(4, (int)(Game.width - 150) / 50) * 50 + 25, r.Next(3, (int)(Game.height - 150) / 50) * 50 + 25);
    //    }
    //    public override void Update()
    //    {
    //        //Every 4 seconds fire projectiles
    //        if ((int)Game.timeSinceStart % shootTime == 0)
    //        {
    //            float s = 1 + MathF.Sin(Game.timeSinceStart % shootTime * MathF.PI) * 0.5f;
    //            Scale = new Vector2f(s, s);
    //            if (!hasSpawned)
    //            {
    //                hasSpawned = true;
    //                for (int x = 0; x < maxProjectiles; x++)
    //                {
    //                    //Spawn new projectiles
    //                    ExploderProjectile p = new ExploderProjectile();
    //                    p.Position = Position;
    //                    //Set the velocity so that the projectiles burst in a sphere
    //                    p.velocity = new Vector2f(MathF.Sin((float)x / (float)maxProjectiles * 2 * MathF.PI) * speed, MathF.Cos((float)x / (float)maxProjectiles * 2 * MathF.PI) * speed); //Setzt die Richtung von einem Projektil, die Projektile schießen in einem Kreis vom Gegner weg
    //                    Game.objects.Add(p);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            hasSpawned = false;
    //            float s = 1 + MathF.Sin(Game.timeSinceStart * 0.2f * MathF.PI) * 0.1f;
    //            Scale = new Vector2f(s, s);
    //        }
    //    }
    //}

    //public class ExploderProjectile : MovableObject
    //{
    //    //Settings
    //    int maxParticles = 20;

    //    public override void Initialize(Vector2f position, float rotation, Vector2f size)
    //    {
    //        base.Initialize(position, rotation, size);
    //        this.Texture = new Texture("Flame.png");
    //        Size = new Vector2f(15, 20);
    //        damage = 1;
    //        collisionType = Collision.Projectile;
    //    }

    //    //When collided destroy projectile, and spawn explosion particles
    //    public override void Collided(Object o)
    //    {
    //        base.Collided(o);
    //        if (o.collisionType != Collision.Monster)
    //        {
    //            Game.DestroyObject(this);
    //            Random r = new Random();
    //            for (int x = 0; x < maxParticles; x++)
    //            {
    //                ExplosionParticle p = new ExplosionParticle();
    //                p.Position = Position;
    //                //Spawn explosion particles in a circle. The speed is random, to create a flame effect
    //                p.velocity = new Vector2f(MathF.Sin((float)x / (float)maxParticles * 2 * MathF.PI) * 100 * (float)(r.NextDouble() + 0.5f), MathF.Cos((float)x / (float)maxParticles * 2 * MathF.PI) * 100 * (float)(r.NextDouble() + 0.5f)); //Setzt die Richtung von einem Projektil, die Projektile schießen in einem Kreis vom Gegner weg
    //                Game.objects.Add(p);
    //            }
    //        }
    //    }
    //}

    //public class ExplosionParticle : MovableObject
    //{
    //    float alpha = 255;
    //    public override void Initialize(Vector2f position, float rotation, Vector2f size)
    //    {
    //        base.Initialize(position, rotation, size);
    //        this.Texture = new Texture("Flame.png");
    //        Size = new Vector2f(5, 7);
    //        collisionType = Collision.Projectile;
    //    }

    //    public override void Update()
    //    {
    //        base.Update();
    //        //Fades out the particle, kills it when completely invisible
    //        alpha -= Game.deltaTime * 500;
    //        if (alpha <= 0)
    //            Game.DestroyObject(this);
    //        else
    //            FillColor = new Color(255, 255, 255, (byte)alpha);
    //    }

    //    //Destroy particle on collision
    //    public override void Collided(Object o)
    //    {
    //        base.Collided(o);
    //        Game.DestroyObject(this);
    //    }
    //}

    ////A class that is simply waiting for the user to press space, to restart the game in the game over screen
    //public class MenuWaiter : Object
    //{
    //    //Hide the object
    //    public override void Initialize(Vector2f position, float rotation, Vector2f size)
    //    {
    //        base.Initialize(position, rotation, size);
    //        Size = new Vector2f(0f, 0f);
    //    }

    //    //Checks if the user pressed R, then restarts game
    //    public override void Update()
    //    {
    //        if (Keyboard.IsKeyPressed(Keyboard.Key.R))
    //        {
    //            Game.objects = new List<Object>();
    //            Game.floor = 0;
    //            Game.backgroundOpacity = 1f;
    //            if (Game.player != null)
    //                Game.player.health = 5;
    //            Game.InitializeObjects();
    //        }
    //    }
    //}

    //public class MainMenuWaiter : Object
    //{
    //    //Hides the object
    //    public override void Initialize(Vector2f position, float rotation, Vector2f size)
    //    {
    //        base.Initialize(position, rotation, size);
    //        Size = new Vector2f(0f, 0f);
    //    }

    //    //Checks if the user pressed Enter, then restarts game!
    //    public override void Update()
    //    {
    //        if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
    //        {
    //            Game.objects = new List<Object>();
    //            Game.floor = 0;
    //            Game.backgroundOpacity = 1f;
    //            if (Game.player != null)
    //                Game.player.health = 5;
    //            Game.InitializeObjects();
    //        }
    //    }
    //}

    //public class Mage : Object
    //{
    //    float shootTime = 8f;
    //    float shootDuration = 10f;
    //    int index;
    //    bool hasShot;

    //    //There are 3 mages, so we need to set the index, so that the mages can fire seperately
    //    public void setIndex(int index)
    //    {
    //        this.index = index;
    //        Position = new Vector2f(Game.width - 25, Game.height / 4 * (index + 1) - 25);
    //    }

    //    //Sets the texture for the wizard
    //    public override void Initialize(Vector2f position, float rotation, Vector2f size)
    //    {
    //        base.Initialize(position, rotation, size);
    //        Texture = new Texture("Wizard.png");
    //        Scale = new Vector2f(-1f, 1f);
    //    }

    //    //Starts shooting a laser
    //    public override void Update()
    //    {
    //        base.Update();
    //        //Checks if 8 seconds (shootTime) passed
    //        if ((int)Game.timeSinceStart % shootTime == 0)
    //        {
    //            //On the second floor shoot every time, on the third floor only a random mage should shoot
    //            Random r = new Random((int)Game.timeSinceStart);
    //            if ((r.Next(0, 3) == index || Game.floor < 3) && !hasShot)
    //            {
    //                //Spawn a Fireball
    //                hasShot = true;
    //                MageLaserCharge ml = new MageLaserCharge();
    //                ml.Position = Position + new Vector2f(-25f, 25f);
    //                Game.objects.Insert(1, ml);
    //            }
    //        }
    //        else
    //            hasShot = false;
    //    }
    //}

    //public class MageLaserCharge : Object
    //{
    //    //How long the laser should charge
    //    float lifetime = 2f;

    //    //Add the fireball animation
    //    public override void Initialize(Vector2f position, float rotation, Vector2f size)
    //    {
    //        base.Initialize(position, rotation, size);
    //        collisionType = Collision.None;
    //        Texture = new Texture("Fireball1.png");
    //        frames.Add(new Texture("Fireball1.png"));
    //        frames.Add(new Texture("Fireball2.png"));
    //        frames.Add(new Texture("Fireball3.png"));
    //        frames.Add(new Texture("Fireball4.png"));
    //    }

    //    //makes the lifetime smaller each frame. When 0 destroy the fireballs and spawn a laser
    //    public override void Update()
    //    {
    //        base.Update();
    //        lifetime -= Game.deltaTime;
    //        if (lifetime <= 0)
    //        {
    //            //After 2 seconds spawn a laser and destroy this fireball
    //            MageLaser m = new MageLaser();
    //            m.setPosition(Position);
    //            Game.objects.Insert(1, m);
    //            Game.DestroyObject(this);
    //        }
    //    }
    //}

    //public class MageLaser : MovableObject
    //{
    //    //How long the laser should need to move over the screen
    //    float lifetime = 2f;
    //    public float startPositionY = 0f;

    //    //Sets the Texture and damage values
    //    public override void Initialize(Vector2f position, float rotation, Vector2f size)
    //    {
    //        base.Initialize(position, rotation, size);
    //        Size = new Vector2f(0f, 175f);
    //        damage = 1;
    //        Texture = new Texture("Laser.png");
    //    }

    //    //Sets the position at the beginning of the game
    //    public void setPosition(Vector2f position)
    //    {
    //        startPositionY = position.Y;
    //        Position = position;
    //    }

    //    //Moves the laser over the screen and kills the laser
    //    public override void Update()
    //    {
    //        base.Update();
    //        lifetime -= Game.deltaTime;
    //        //X-Coordinate: Moves the laser from the right side to the center of the screen depending on your lifetime
    //        //Y-Coordinate: Random Wiggles the laser for cooler effect
    //        Position = new Vector2f(Game.width / 2 + (lifetime / 2) * (Game.width / 2), Position.Y + MathF.Sin(Game.timeSinceStart * 100) * 10);

    //        //Shake the screen
    //        Game.screenOffset = new Vector2f(MathF.Sin(Game.timeSinceStart * 100) * 10 * lifetime, MathF.Cos(Game.timeSinceStart * 100) * 10 * lifetime);
    //        Size = new Vector2f((Game.width / 2 - lifetime / 2 * Game.width / 2) * 2, Size.Y);

    //        //After 2 seconds kill the laser and reset the screen shake
    //        if (lifetime <= 0)
    //        {
    //            Game.screenOffset = new Vector2f(0f, 0f);
    //            Game.DestroyObject(this);
    //        }
    //    }

    //    //When player collides with the laser set the damage of the laser to 0, so that the player does not get damaged every frame
    //    public override void Collided(Object o)
    //    {
    //        base.Collided(o);
    //        if (o == Game.player)
    //            damage = 0;
    //    }
    //}
}
