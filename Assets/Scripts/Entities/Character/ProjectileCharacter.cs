﻿using System;
using System.Collections;
using Entities.Enemy.EnemyObject;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Character
{
    public class ProjectileCharacter : Projectile
    {
        public bool IsRebound
        {
            set => _isRebound = value;
        }

        public event Action OnKilledEnemy;
        public event Action<ProjectileCharacter> OnDisableProjectile;
        public event Action<ProjectileCharacter> OnClosestEnemy;

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
                HitEnemy(enemy);
            }
        }
        private void HitEnemy(BaseEnemy enemy)
        {
            enemy.DecreaseHealth(enemy.Health);

            if (enemy.Health <= enemy.MinHealth)
            {
                IncreaseKillCount();
                EnergyValue += GetEnergyPoint(enemy);
                OnKilledEnemy?.Invoke();
                NextProjectileAction();
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
            gameObject.SetActive(false);
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
            transform.position += (ClosestEnemy.transform.position - transform.position).normalized *
                                  (_moveSpeed * Time.deltaTime);
        }
    }
}