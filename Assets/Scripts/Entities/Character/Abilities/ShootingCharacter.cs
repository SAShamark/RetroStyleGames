using Entities.Character.Data;
using Entities.Enemy.EnemyObject;
using UnityEngine;

namespace Entities.Character.Abilities
{
    public class ShootingCharacter
    {
        private readonly ObjectPool<CharacterProjectile> _objectPool;
        private readonly CharacterShootData _characterShootData;
        private readonly CharacterStatsControl _characterStatsControl;
        private readonly ServiceContainer _serviceContainer;

        public ShootingCharacter(ServiceContainer serviceContainer, CharacterShootData characterShootData,
            CharacterStatsControl characterStatsControl)
        {
            _serviceContainer = serviceContainer;
            _characterShootData = characterShootData;
            _characterStatsControl = characterStatsControl;
            _objectPool = new ObjectPool<CharacterProjectile>(_characterShootData.CharacterProjectilePrefab,
                _characterShootData.CountProjectile);
        }

        public void GetProjectile()
        {
            var projectile = _objectPool.GetFreeElement();
            SetProjectileData(projectile);
        }

        private void SetProjectileData(CharacterProjectile characterProjectile)
        {
            characterProjectile.OnKilledEnemy += UpdateStats;
            characterProjectile.OnDisableProjectile += Unsubscribe;
            characterProjectile.OnClosestEnemy += FindClosestEnemy;

            characterProjectile.IsRebound = _characterStatsControl.IsReboundProjectile();

            Transform transform;
            (transform = characterProjectile.transform).rotation = Quaternion.Euler(_characterShootData.Camera.eulerAngles.x,
                _characterShootData.Character.eulerAngles.y, 0);

            transform.position = _characterShootData.ProjectileStartPosition.position;
        }

        private void Unsubscribe(CharacterProjectile characterProjectile)
        {
            characterProjectile.OnKilledEnemy -= UpdateStats;
            characterProjectile.OnDisableProjectile -= Unsubscribe;
            characterProjectile.OnClosestEnemy -= FindClosestEnemy;
        }

        private void FindClosestEnemy(CharacterProjectile characterProjectile)
        {
            float minDistance = Mathf.Infinity;
            BaseEnemy closestEnemy = null;
            foreach (var enemy in _serviceContainer.EnemyRegistry.EnemiesContainer)
            {
                float distance = Vector3.Distance(enemy.transform.position,
                    characterProjectile.gameObject.transform.position);
                if (distance < minDistance)
                {
                    closestEnemy = enemy;
                    minDistance = distance;
                }
            }

            characterProjectile.ClosestEnemy = closestEnemy;
        }

        private void UpdateStats()
        {
            foreach (var projectileCharacter in _objectPool.Pool)
            {
                bool addPower = projectileCharacter.KillCount == 1;
                bool addPowerAndHealth = projectileCharacter.KillCount == 2;
                if (addPower)
                {
                    _characterStatsControl.IncreaseKillCount();
                    _characterStatsControl.IncreasePower(projectileCharacter.EnergyValue);
                    projectileCharacter.ResetEnergyValue();
                }
                else if (addPowerAndHealth)
                {
                    _characterStatsControl.IncreaseKillCount();
                    _characterStatsControl.IncreasePower(projectileCharacter.EnergyValue);
                    _characterStatsControl.IncreaseHealth(_characterStatsControl.Health / 2);
                    projectileCharacter.ResetEnergyValue();
                }
            }
        }
    }
}