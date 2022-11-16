using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities.Enemy.EnemyObject;
using Entities.Enemy.EnemyObject.Data;
using Services;
using UnityEngine;
using CharacterController = Entities.Character.CharacterController;

namespace Entities.Enemy
{
    public class EnemySpawner
    {
        public Dictionary<EnemyType, ObjectPool<BaseEnemy>> EnemiesPools;
        private CharacterController _characterController;

        private int _countForSpawnEnemy = 1;
        private float _timeForSpawn;
        private const float StartTimeForSpawn = 5f;
        private const float DecreasedSpawnTimeValue = 0.5f;
        private const float MinSpawnTime = 2;
        private readonly EnemiesData _enemiesData;
        private readonly Transform _enemyContainer;

        public EnemySpawner(EnemiesData enemiesData, Transform enemyContainer)
        {
            _enemiesData = enemiesData;
            _enemyContainer = enemyContainer;
        }

        public void Init()
        {
            _characterController = ServiceLocator.SharedInstance.Resolve<CharacterController>();
            
            _timeForSpawn = StartTimeForSpawn;
            EnemiesPools = new Dictionary<EnemyType, ObjectPool<BaseEnemy>>()
            {
                {
                    EnemyType.Blue,
                    new ObjectPool<BaseEnemy>(GetEnemyData(EnemyType.Blue).BaseEnemy, 1, _enemyContainer)
                },
                {
                    EnemyType.Red,
                    new ObjectPool<BaseEnemy>(GetEnemyData(EnemyType.Red).BaseEnemy, 1, _enemyContainer)
                }
            };
        }

        public IEnumerator Spawner()
        {
            var delay = new WaitForSeconds(_timeForSpawn);
            while (true)
            {
                yield return delay;
                SpawnerTime();
                GetEnemy();
            }
        }

        private void SpawnerTime()
        {
            if (_timeForSpawn >= MinSpawnTime)
            {
                _timeForSpawn -= DecreasedSpawnTimeValue;
            }
            else
            {
                _countForSpawnEnemy++;
            }
        }


        private void GetEnemy()
        {
            var enemyType = GetEnemyType();
            for (int i = 0; i < _countForSpawnEnemy; i++)
            {
                var positionForSpawn = PositionProcessor.GetNewPosition();
                var enemyData = GetEnemyData(enemyType);

                foreach (var objectPool in EnemiesPools)
                {
                    if (objectPool.Key == enemyType)
                    {
                        var enemy = objectPool.Value.GetFreeElement();
                        var transform = enemy.transform;
                        transform.position = positionForSpawn;
                        transform.parent = _enemyContainer;
                        enemy.Init(enemyData.EnemyStaticData, _characterController.transform);
                    }
                }
            }
        }


        //Blue:Red = 1:4
        private EnemyType GetEnemyType()
        {
            int index = Random.Range(1, 6);
            return index > 1 ? EnemyType.Red : EnemyType.Blue;
        }

        private EnemyData GetEnemyData(EnemyType enemyType)
        {
            foreach (var enemyData in _enemiesData.EnemyDates.Where(enemyData => enemyData.EnemyType == enemyType))
            {
                return enemyData;
            }

            return _enemiesData.DefaultEnemy;
        }
    }
}