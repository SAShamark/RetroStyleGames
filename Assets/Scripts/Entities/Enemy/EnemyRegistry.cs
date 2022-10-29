using System.Collections.Generic;
using Entities.Enemy.EnemyObject;

namespace Entities.Enemy
{
    public class EnemyRegistry
    {
        public List<BaseEnemy> EnemiesContainer { get; private set; }

        private void Start()
        {
            EnemiesContainer = new List<BaseEnemy>();
        }
        /*private void OnDestroy()
        {
            foreach (var enemy in EnemiesContainer)
            {
                _target.OnTelepot -= enemy.ChangeTarget;
            }
        }*/
        public void AddEnemy(BaseEnemy enemy)
        {
            enemy.OnDeath += RemoveEnemy;
            //CharacterView.OnTelepot += enemy.ChangeTarget;
            EnemiesContainer.Add(enemy);
        }
        public void RemoveEnemy(BaseEnemy enemy)
        {
            enemy.OnDeath -= RemoveEnemy;
            EnemiesContainer.Remove(enemy);
        }
    }
}