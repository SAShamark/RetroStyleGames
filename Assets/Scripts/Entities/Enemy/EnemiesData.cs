using System.Collections.Generic;
using Entities.Enemy.EnemyObject.Data;
using UnityEngine;

namespace Entities.Enemy
{
    [CreateAssetMenu(fileName = "EnemiesData", menuName = "Unit/Enemy/EnemiesData", order = 1)]
    public class EnemiesData : ScriptableObject
    {
        [SerializeField] private List<EnemyData> _enemyDates;
        [SerializeField] private EnemyData _defaultEnemy;
        public List<EnemyData> EnemyDates => _enemyDates;
        public EnemyData DefaultEnemy => _defaultEnemy;
    }
}