using System;
using System.Collections;
using Entities.Enemy;
using Entities.Enemy.EnemyObject;
using ModestTree.Util;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Entities.Character
{
    public sealed class ProjectileCharacter : Projectile
    {
        public bool IsRebound
        {
            set => _isRebound = value;
        }

        public event Action OnDisableProjectile; 
        public int KillCount { get; private set; }
        public float EnergyValue { get; private set; }

        private const int EnemyLayer = 9;
        private const float PossibleReboundPercent = 0.1f;
        private bool _isRebound;
        private BaseEnemy _closestEnemy;
        private EnemyFactory _enemyFactory;

        [Inject]
        private void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        private void Start()
        {
            TurnOffProjectileCoroutine = TurnOffProjectile(_lifeTime);
            StartCoroutine(TurnOffProjectileCoroutine);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.gameObject.layer == EnemyLayer)
            {
                var enemy = other.gameObject.GetComponentInParent<BaseEnemy>();
                EnergyValue = GetEnergyPoint(enemy);
                HitEnemy(enemy);
            }
        }

        private void HitEnemy(BaseEnemy enemy)
        {
            enemy.DecreaseHealth(enemy.Health);

            if (enemy.Health <= enemy.MinHealth)
            {
                IncreaseKillCount();
                NextProjectileAction();
            }
            else
            {
                TurnOffProjectile();
            }
        }

        private void NextProjectileAction()
        {
            if (KillCount == 1)
            {
                TryGetNextEnemy();
            }
            else if (KillCount > 1)
            {
                ResetKillCount();
                TurnOffProjectile();
                StopCoroutine(TurnOffProjectileCoroutine);
            }
        }

        public void IncreaseKillCount()
        {
            KillCount++;
        }

        public void DecreaseKillCount()
        {
            if (KillCount > 0)
            {
                KillCount--;
            }
            else
            {
                Debug.LogError("The number of murders cannot be less than zero");
            }
        }

        public void ResetKillCount()
        {
            KillCount = 0;
        }

        private void TryGetNextEnemy()
        {
            var reboundChance = Random.Range(0, 1);
            if (reboundChance >= PossibleReboundPercent || _isRebound)
            {
                _closestEnemy = FindClosestEnemy();
            }
        }

        private BaseEnemy FindClosestEnemy()
        {
            float distance = Mathf.Infinity;
            Vector3 projectilePosition = transform.position;

            foreach (var enemy in _enemyFactory.EnemyRegistry.EnemiesContainer)
            {
                Vector3 diff = enemy.transform.position - projectilePosition;
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

        protected override IEnumerator TurnOffProjectile(float delay)
        {
            yield return new WaitForSeconds(delay);
            OnDisableProjectile?.Invoke();
            gameObject.SetActive(false);
        }

        private void TurnOffProjectile()
        {
            gameObject.SetActive(false);
            OnDisableProjectile?.Invoke();
            StopCoroutine(TurnOffProjectileCoroutine);
        }

        protected override void MoveProjectile()
        {
            if (_closestEnemy == null)
            {
                MoveProjectileForward();
            }
            else
            {
                MoveProjectileToClosestEnemy();
            }
        }

        private void MoveProjectileForward()
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * _moveSpeed));
        }

        private void MoveProjectileToClosestEnemy()
        {
            var position = transform.position;
            var closestEnemyPosition = _closestEnemy.transform.position;

            position = Vector3.MoveTowards(position, closestEnemyPosition, _moveSpeed);
            position += (closestEnemyPosition - position).normalized * (_moveSpeed * Time.deltaTime);

            transform.position = position;
        }
    }
}