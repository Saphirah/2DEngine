
using System.Collections.Generic;

namespace GameJam.Assets
{
    public class EnemyManager
    {
        public List<Enemy> enemies = new List<Enemy>();

        public EnemyManager()
        {
            SpawnWave();
        }

        public void SpawnWave()
        {
            for (int x = 0; x < 10; x++)
            {
                Enemy e = Object.Create<Enemy>();
                e.OnDestroy += EnemyDied;
                enemies.Add(e);
            }
        }

        public void EnemyDied()
        {
            if (getEnemyCount() <= 0) {
                foreach (Enemy e in enemies)
                    e.OnDestroy -= EnemyDied;
                enemies = new List<Enemy>();
                SpawnWave();
            }

        }

        public int getEnemyCount()
        {
            int livingEnemies = 0;
            foreach(Enemy e in enemies)
            {
                if (e.IsActive())
                    livingEnemies++;
            }
            return livingEnemies;
        }
    }
}