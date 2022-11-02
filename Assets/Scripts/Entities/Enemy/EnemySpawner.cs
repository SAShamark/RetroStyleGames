using System.Collections;
using System.Collections.Generic;
using Entities.Enemy.EnemyObject;
using Entities.Enemy.EnemyObject.Data;
using UnityEngine;
using Zenject;
using CharacterController = Entities.Character.CharacterController;

namespace Entities.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public EnemyRegistry EnemyRegistry { get; private set; }

        
        [SerializeField] private List<EnemyData> _enemyDates;
        [SerializeField] private EnemyData _defaultEnemy;
        [SerializeField] private Transform _enemyContainer;
        
        private CharacterController _characterController;
        private const float StartTimeForSpawn = 5f;
        private float _timeForSpawn;
        private const float DecreasedSpawnTimeValue = 0.5f;
        private int _countForSpawnEnemy = 1;
        private const float MinSpawnTime = 2;
        private EnemyType _enemyType;

        [Inject]
        private void Construct(CharacterController characterController)
        {
            _characterController = characterController;
        }
        public void Start()
        {
            _timeForSpawn = StartTimeForSpawn;
            EnemyRegistry = new EnemyRegistry();
            Spawn();
        }

        private void Spawn()
        {
            StartCoroutine(Spawner());
        }

        private IEnumerator Spawner()
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


        private void CreatEnemy()
        {
            _enemyType = GetEnemyType();
            for (int i = 0; i < _countForSpawnEnemy; i++)
            {
                var positionForSpawn = PositionProcessor.GetNewPosition();
                var enemyPrefab = GetEnemyForSpawn(_enemyType);
                var enemy = Instantiate(enemyPrefab, positionForSpawn, Quaternion.identity, _enemyContainer);
                enemy.Init(SearchNeededEnemyData(_enemyType),_characterController.transform);
                EnemyRegistry.AddEnemy(enemy);
            }
        }

        

        //Blue:Red = 1:4
        private EnemyType GetEnemyType()
        {
            var index = Random.Range(1, 6);
            return index > 1 ? EnemyType.Red : EnemyType.Blue;
        }

        private BaseEnemy GetEnemyForSpawn(EnemyType enemyType)
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