using System.Collections;
using System.Collections.Generic;
using Entities.Enemy.EnemyObject;
using UnityEngine;

namespace Entities.Enemy
{
    public class EntitiesFactory : MonoBehaviour
    {
        public static EntitiesFactory Instance;
        public List<BaseEnemy> EnemiesContainer { get; private set; }
        [SerializeField] private CharacterView _characterPrefab;
        [SerializeField] private List<EnemyData> _enemyDates;
        [SerializeField] private EnemyData _defaultEnemy;
        [SerializeField] private Transform _enemyContainer;
        private const float StartTimeForSpawn = 5f;
        private float _timeForSpawn;
        private const float DecreasedSpawnTimeValue = 0.5f;
        private int _countForSpawnEnemy = 1;
        private const float MinSpawnTime = 2;
        private EnemyType _enemyType;

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
            _timeForSpawn = StartTimeForSpawn;
            StartCoroutine(SpawnEnemyMethod());
        }

        private IEnumerator SpawnEnemyMethod()
        {
            var delay = new WaitForSeconds(_timeForSpawn);
            while (true)
            {
                yield return delay;
                if (_timeForSpawn >= MinSpawnTime)
                {
                    _timeForSpawn -= DecreasedSpawnTimeValue;
                }
                else
                {
                    _countForSpawnEnemy++;
                }

                CreatEnemy();
            }
        }

        private void OnDestroy()
        {
            /*foreach (var enemy in EnemiesContainer)
            {
                _target.OnTelepot -= enemy.ChangeTarget;
            }*/
        }

        private void CreatEnemy()
        {
            _enemyType = GetEnemyType();
            for (int i = 0; i < _countForSpawnEnemy; i++)
            {
                var newPosition = PositionProcessor.GetNewPosition();
                var enemyPrefab = GetEnemyForSpawn(_enemyType);
                var enemy = Instantiate(enemyPrefab, newPosition, Quaternion.identity, _enemyContainer);
                enemy.OnDeath += RemoveEnemy;
                enemy.Target = _characterPrefab.gameObject.transform.parent;
                //_target.OnTelepot += enemy.ChangeTarget;
                enemy.EnemyStaticData = SearchNeededEnemyData(_enemyType);
                EnemiesContainer.Add(enemy);
            }
        }

        private void RemoveEnemy(BaseEnemy enemy)
        {
            enemy.OnDeath -= RemoveEnemy;
            EnemiesContainer.Remove(enemy);
        }

        //Blue:Red = 1:4
        private EnemyType GetEnemyType()
        {
            var index = Random.Range(1, 6);
            return index > 1 ? EnemyType.Red : EnemyType.Blue;
        }

        private BaseEnemy GetEnemyForSpawn(EnemyType enemyType)
        {
            return SearchNeededEnemy(enemyType);
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

            return _defaultEnemy.BaseEnemy;
        }

        private EnemyStaticData SearchNeededEnemyData(EnemyType enemyType)
        {
            foreach (var enemyData in _enemyDates)
            {
                if (enemyData.EnemyType == enemyType)
                {
                    return enemyData.EnemyStaticData;
                }
            }

            return _defaultEnemy.EnemyStaticData;
        }
    }
}