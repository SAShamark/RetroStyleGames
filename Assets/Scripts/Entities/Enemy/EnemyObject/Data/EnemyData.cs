using System;
using UnityEngine;

namespace Entities.Enemy.EnemyObject.Data
{
    [Serializable]
    public class EnemyData
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private BaseEnemy _baseEnemy;
        [SerializeField] private EnemyStaticData _enemyStaticData;
        public EnemyType EnemyType => _enemyType;
        public BaseEnemy BaseEnemy => _baseEnemy;
        public EnemyStaticData EnemyStaticData => _enemyStaticData;
    }
}