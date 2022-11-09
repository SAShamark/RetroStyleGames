using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities.Enemy.EnemyObject.Data;
using UnityEngine;
using Zenject;
using CharacterController = Entities.Character.CharacterController;

namespace Entities.Enemy
{
    public class EnemySpawner
    {

        private const float StartTimeForSpawn = 5f;
        private float _timeForSpawn;
        private const float DecreasedSpawnTimeValue = 0.5f;
        private int _countForSpawnEnemy = 1;
        private const float MinSpawnTime = 2;
        private EnemyType _enemyType;

        private readonly CharacterController _characterController;
        private readonly EnemyRegistry _enemyRegistry;
        private readonly List<EnemyData> _enemyDates;
        private readonly EnemyData _defaultEnemy;
        private readonly Transform _enemyContainer;


        public EnemySpawner(CharacterController characterController, EnemyRegistry enemyRegistry,
            List<EnemyData> enemyDates, EnemyData enemyData, Transform enemyContainer)
        {
            _characterController = characterController;
            _enemyRegistry = enemyRegistry;
            _enemyDates = enemyDates;
            _defaultEnemy = enemyData;
            _enemyContainer = enemyContainer;
        }

        public void Init()
        {
            _timeForSpawn = StartTimeForSpawn;
        }

        public IEnumerator Spawner()
        {
            var delay = new WaitForSeconds(_timeForSpawn);
            while (true)
            {
                yield return delay;
                SpawnerTime();
                CreatEnemy();
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


        private void CreatEnemy()
        {
            _enemyType = GetEnemyType();
            for (int i = 0; i < _countForSpawnEnemy; i++)
            {
                var positionForSpawn = PositionProcessor.GetNewPosition();
                var enemyData = GetEnemyData(_enemyType);
                var enemy = Object.Instantiate(enemyData.BaseEnemy, positionForSpawn, Quaternion.identity,
                    _enemyContainer);

                enemy.Init(enemyData.EnemyStaticData, _characterController.transform);
                _enemyRegistry.AddEnemy(enemy);
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
            foreach (var enemyData in _enemyDates.Where(enemyData => enemyData.EnemyType == enemyType))
            {
                return enemyData;
            }

            return _defaultEnemy;
        }
    }
}