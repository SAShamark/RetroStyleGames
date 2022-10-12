using System.Collections;
using Entities.Enemy;
using Entities.Enemy.EnemyObject;
using UnityEngine;

namespace Entities.Player.Shoot
{
    public class ProjectileControlPlayer : ProjectileControl
    {
        private EnemySpawner _enemySpawner;
        public int KillCount { get; set; }
        public float EnergyValue { get; private set; }
        private const int EnemyLayer = 9;
        private BaseEnemy _closestEnemy;
        private int _killCount;

        private void Start()
        {
            _enemySpawner = EnemySpawner.Instance;
            _turnOffProjectileCoroutine = TurnOffProjectile(_lifeTime);
            StartCoroutine(_turnOffProjectileCoroutine);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.gameObject.layer == EnemyLayer)
            {
                Debug.LogError(13);
                var enemy = other.gameObject.GetComponentInParent<BaseEnemy>();
                EnergyValue = GetEnergyPoint(enemy);
                KillCount++;
                enemy.DecreaseHealth(enemy.Health);
            }
        }

        public void TryGetNextEnemy()
        {
            _closestEnemy = FindClosestEnemy();
        }

        private BaseEnemy FindClosestEnemy()
        {
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (var enemy in _enemySpawner.EnemiesContainer)
            {
                Vector3 diff = enemy.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    _closestEnemy = enemy;
                    distance = curDistance;
                }
            }

            return _closestEnemy;
        }

        private float GetEnergyPoint(BaseEnemy enemy)
        {
            return enemy.gameObject.GetComponent<BaseEnemy>().EnergyPoint;
        }

        public void ResetEnergyValue()
        {
            EnergyValue = 0;
        }

        protected override IEnumerator TurnOffProjectile(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            gameObject.SetActive(false);
        }

        protected override void Move()
        {
            if (_closestEnemy == null)
            {
                transform.Translate(Vector3.forward * (Time.deltaTime * _moveSpeed));
            }
            else
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, _closestEnemy.transform.position, _moveSpeed);
            }
        }
    }
}