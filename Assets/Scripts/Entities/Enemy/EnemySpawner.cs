using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance;
        public List<BaseEnemy> EnemiesContainer { get; private set; }
        [SerializeField] private Transform _enemyTarget;
        [SerializeField] private List<EnemyData> _enemyDates;
        [SerializeField] private BaseEnemy _defaultEnemy;
        [SerializeField] private Transform _enemyContainer;
        private float _startTimeForSpawn = 5f;
        private float _timeForSpawn;
        private float _decreasedSpawnTimeValue = 0.5f;
        private int _countForSpawnEnemy = 1;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            EnemiesContainer = new List<BaseEnemy>();
            _timeForSpawn = _startTimeForSpawn;
            StartCoroutine(SpawnEnemyMethod());
        }

        private IEnumerator SpawnEnemyMethod()
        {
            var delay = new WaitForSeconds(_timeForSpawn);
            while (true)
            {
                yield return delay;
                if (_timeForSpawn >= 2f)
                {
                    _timeForSpawn -= _decreasedSpawnTimeValue;
                }
                else
                {
                    _countForSpawnEnemy++;
                }

                CreatEnemy();
            }
        }

        private void CreatEnemy()
        {
            for (int i = 0; i < _countForSpawnEnemy; i++)
            {
                var pos = PositionProcessor.GetNewPosition();
                var enemyPrefab = GetEnemyForSpawn();
                var enemy = Instantiate(enemyPrefab, pos, Quaternion.identity, _enemyContainer);
                enemy.Target = _enemyTarget;
                EnemiesContainer.Add(enemy);
            }
        }

        private BaseEnemy GetEnemyForSpawn()
        {
            //var index = Random.Range(1, 6);
            //return SearchNeededEnemy(index > 1 ? EnemyType.Red : EnemyType.Blue);
            return SearchNeededEnemy(EnemyType.Blue);
        }

        private BaseEnemy SearchNeededEnemy(EnemyType enemyType)
        {
            foreach (var enemyData in _enemyDates)
            {
                if (enemyData.EnemyType == enemyType)
                {
                    return enemyData.BaseEnemy;
                }
            }

            return _defaultEnemy;
        }
    }
}