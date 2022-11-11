using System.Collections.Generic;
using Entities.Enemy.EnemyObject;

namespace Entities.Enemy
{
    public class EnemyRegistry
    {
        public List<BaseEnemy> EnemiesContainer { get; }

        public EnemyRegistry()
        {
            EnemiesContainer = new List<BaseEnemy>();
        }
        public void AddEnemy(BaseEnemy enemy)
        {
            enemy.OnDeath += RemoveEnemy;
            EnemiesContainer.Add(enemy);
        }
        private void RemoveEnemy(BaseEnemy enemy)
        {
            enemy.OnDeath -= RemoveEnemy;
            EnemiesContainer.Remove(enemy);
        }
    }
}