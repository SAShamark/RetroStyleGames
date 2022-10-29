using System.Collections.Generic;
using Entities.Character;
using Entities.Enemy.EnemyObject;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyRegistry : MonoBehaviour
    {
        public List<BaseEnemy> EnemiesContainer;

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