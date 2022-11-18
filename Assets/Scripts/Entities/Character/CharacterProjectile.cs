using System;
using System.Collections;
using Entities.Enemy.EnemyObject;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Character
{
    public class CharacterProjectile : Projectile
    {
        public event Action OnKilledEnemy;
        public event Action<CharacterProjectile> OnDisableProjectile;
        public event Action<CharacterProjectile> OnClosestEnemy;

        public bool IsRebound
        {
            set => _isRebound = value;
        }

        public float EnergyValue { get; private set; }
        public BaseEnemy ClosestEnemy { get; set; }

        private const int EnemyLayer = 9;
        private const float PossibleReboundPercent = 0.1f;
        private bool _isRebound;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.gameObject.layer == EnemyLayer)
            {
                var enemy = other.gameObject.GetComponentInParent<BaseEnemy>();
                KillEnemy(enemy);
            }
        }

        private void KillEnemy(BaseEnemy enemy)
        {
            enemy.Death();

            IncreaseKillCount();
            EnergyValue += GetEnergyPoint(enemy);
            OnKilledEnemy?.Invoke();
            NextProjectileAction();
        }

        private void NextProjectileAction()
        {
            switch (KillCount)
            {
                case 1:
                    TryGetNextEnemy();
                    break;
                case > 1:
                    TurnOffProjectile();
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

        private float GetEnergyPoint(BaseEnemy enemy)
        {
            return enemy.gameObject.GetComponent<BaseEnemy>().EnergyPoint;
        }

        public void ResetEnergyValue()
        {
            EnergyValue = 0;
        }

        protected override void TurnOffProjectile()
        {
            ClosestEnemy = null;
            ResetEnergyValue();
            OnDisableProjectile?.Invoke(this);
            base.TurnOffProjectile();
        }

        protected override IEnumerator TurnOffProjectile(float delay)
        {
            yield return new WaitForSeconds(delay);
            ResetKillCount();
            ResetEnergyValue();
            OnDisableProjectile?.Invoke(this);
            ClosestEnemy = null;
            gameObject.SetActive(false);
        }

        protected override void MoveProjectile()
        {
            if (ClosestEnemy != null)
            {
                MoveProjectileToClosestEnemy();
            }

            MoveProjectileForward();
        }

        private void MoveProjectileForward()
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * _moveSpeed));
        }

        private void MoveProjectileToClosestEnemy()
        {
            var transformPosition = transform.position;
            transformPosition += (ClosestEnemy.transform.position - transformPosition).normalized *
                                 (_moveSpeed * Time.deltaTime);
            transform.position = transformPosition;
        }
    }
}