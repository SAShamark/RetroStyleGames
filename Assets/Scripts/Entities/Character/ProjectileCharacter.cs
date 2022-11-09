using System;
using System.Collections;
using Entities.Enemy.EnemyObject;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Character
{
    public sealed class ProjectileCharacter : Projectile
    {
        public bool IsRebound
        {
            set => _isRebound = value;
        }

        public event Action OnKilledEnemy;
        public event Action<ProjectileCharacter> OnDisableProjectile;
        public event Action<ProjectileCharacter> OnClosestEnemy;
        public int KillCount { get; private set; }
        public float EnergyValue { get; private set; }
        public BaseEnemy ClosestEnemy { get; set; }

        private const int EnemyLayer = 9;
        private const float PossibleReboundPercent = 0.1f;
        private bool _isRebound;


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
                OnKilledEnemy?.Invoke();
            }
            else
            {
                TurnOffProjectile();
            }
        }

        private void NextProjectileAction()
        {
            switch (KillCount)
            {
                case 1:
                    TryGetNextEnemy();
                    break;
                case > 1:
                    ResetKillCount();
                    TurnOffProjectile();
                    StopCoroutine(TurnOffProjectileCoroutine);
                    break;
            }
        }

        private void TryGetNextEnemy()
        {
            int reboundChance = Random.Range(0, 1);
            if (reboundChance >= PossibleReboundPercent || _isRebound)
            {
                OnClosestEnemy?.Invoke(this);
            }
        }


        private void IncreaseKillCount()
        {
            KillCount++;
        }
        public void ResetKillCount()
        {
            KillCount = 0;
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
            gameObject.SetActive(false);
            OnDisableProjectile?.Invoke(this);
        }

        protected override void TurnOffProjectile()
        {
            base.TurnOffProjectile();
            OnDisableProjectile?.Invoke(this);
        }

        protected override void MoveProjectile()
        {
            if (ClosestEnemy == null)
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
            var closestEnemyPosition = ClosestEnemy.transform.position;

            position = Vector3.MoveTowards(position, closestEnemyPosition, _moveSpeed);
            position += (closestEnemyPosition - position).normalized * (_moveSpeed * Time.deltaTime);

            transform.position = position;
        }
    }
}