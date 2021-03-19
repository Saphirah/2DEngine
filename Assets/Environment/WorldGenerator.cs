using SFML.System;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GameJam.Assets.Environment
{
    public class WorldGenerator : Object
    {
        public static Action OnChunkCreated;
        public static Action OnChunkDestroyed;

        private static WorldGenerator Instance = null;
        public static WorldGenerator instance {
            get
            {
                if (Instance == null)
                    Instance = new WorldGenerator();
                return Instance;
            }
        }

        private WorldGenerator() { }

        static Dictionary<Vector2i, Chunk> chunks = new Dictionary<Vector2i, Chunk>();
        public static Vector2i chunkDimension = new Vector2i(16, 16);
        public static Vector2f chunkSize = new Vector2f(400f, 400f);

        public static Object GetTileAtPosition(Vector2i position)
        {
            Chunk chunk = GetChunkAtPosition(position);
            if (chunk != null)
                return chunk.GetTile(GetLocalCoordinate(position));
            else
                return null;
        }

        public static Chunk GetChunkAtPosition(Vector2i position)
        {
            Vector2i chunkCoordinates = position - GetLocalCoordinate(position);
            chunkCoordinates = new Vector2i(chunkCoordinates.X / chunkDimension.X, chunkCoordinates.Y / chunkDimension.Y);
            if (chunks.ContainsKey(chunkCoordinates))
                return chunks[chunkCoordinates];
            else
                return null;
        }

        public static Vector2i GetLocalCoordinate(Vector2i position)
        {
            return new Vector2i(position.X % chunkDimension.X, position.Y % chunkDimension.Y);
        }
    }

    public class Chunk
    {
        Action OnTileModified;
        Action OnGenerated;

        Vector2i chunkCoodinate;
        
        List<List<Object>> tiles = new List<List<Object>>();

        public Chunk(Vector2i position)
        {
            this.chunkCoodinate = position;
            Generate();
        }

        public void Generate()
        {
            for (int x = 0; x < WorldGenerator.chunkDimension.X; x++)
            {
                tiles.Add(new List<Object>());
                for (int y = 0; y < WorldGenerator.chunkDimension.Y; y++)
                    tiles[x].Add(GenerateTileAtPosition(new Vector2i(x, y) + new Vector2i((int)(chunkCoodinate.X * WorldGenerator.chunkSize.X), (int)(chunkCoodinate.Y * WorldGenerator.chunkSize.Y))));
            }
            OnGenerated?.Invoke();
        }
        public Object GetTile(Vector2i position)
        {
            if (position.X < 0 || position.Y < 0 || position.X >= WorldGenerator.chunkDimension.X || position.Y >= WorldGenerator.chunkDimension.Y)
                return null;
            else
                return tiles[position.Y][position.X];
        }
 
        public Vector2i GetGlobalCoordinate()
        {
            return new Vector2i(chunkCoodinate.X * WorldGenerator.chunkDimension.X, chunkCoodinate.Y * WorldGenerator.chunkDimension.Y);
        }
        public Vector2i GetGlobalCoordinate(Vector2i position)
        {
            return position + GetGlobalCoordinate();
        }

        public virtual Object GenerateTileAtPosition(Vector2i position)
        {
            return new Object();
        }
    } 
}
