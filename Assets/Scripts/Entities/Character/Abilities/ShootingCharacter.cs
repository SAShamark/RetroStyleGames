using Entities.Character.Data;
using Entities.Enemy.EnemyObject;
using UnityEngine;

namespace Entities.Character.Abilities
{
    public class ShootingCharacter
    {
        private readonly ObjectPool<ProjectileCharacter> _objectPool;
        private readonly CharacterShootData _characterShootData;
        private readonly CharacterStatsControl _characterStatsControl;
        private readonly ApplicationStart _applicationStart;

        public ShootingCharacter(ApplicationStart applicationStart, CharacterShootData characterShootData,
            CharacterStatsControl characterStatsControl)
        {
            _applicationStart = applicationStart;
            _characterShootData = characterShootData;
            _characterStatsControl = characterStatsControl;
            _objectPool = new ObjectPool<ProjectileCharacter>(_characterShootData.ProjectilePrefab,
                _characterShootData.CountProjectile);
        }

        public void GetProjectile()
        {
            var projectile = _objectPool.GetFreeElement();
            SetProjectileData(projectile);
        }

        private void SetProjectileData(ProjectileCharacter projectileCharacter)
        {
            projectileCharacter.OnKilledEnemy += UpdateStats;
            projectileCharacter.OnDisableProjectile += Unsubscribe;
            projectileCharacter.OnClosestEnemy += FindClosestEnemy;

            projectileCharacter.IsRebound = _characterStatsControl.IsReboundProjectile();

            projectileCharacter.transform.rotation = Quaternion.Euler(_characterShootData.Camera.eulerAngles.x,
                _characterShootData.Character.eulerAngles.y, 0);

            projectileCharacter.transform.position = _characterShootData.ProjectileStartPosition.position;
        }

        private void Unsubscribe(ProjectileCharacter projectileCharacter)
        {
            projectileCharacter.OnKilledEnemy -= UpdateStats;
            projectileCharacter.OnDisableProjectile -= Unsubscribe;
            projectileCharacter.OnClosestEnemy -= FindClosestEnemy;
        }

        private void FindClosestEnemy(ProjectileCharacter projectileCharacter)
        {
            float minDistance = Mathf.Infinity;
            BaseEnemy closestEnemy = null;
            foreach (var enemy in _applicationStart.EnemyRegistry.EnemiesContainer)
            {
                float distance = Vector3.Distance(enemy.transform.position,
                    projectileCharacter.gameObject.transform.position);
                if (distance < minDistance)
                {
                    closestEnemy = enemy;
                    minDistance = distance;
                }
            }

            projectileCharacter.ClosestEnemy = closestEnemy;
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