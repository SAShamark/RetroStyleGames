using Entities.Character.Data;
using Entities.Enemy;
using Entities.Enemy.EnemyObject;
using Services;
using UnityEngine;

namespace Entities.Character.Controllers
{
    public class ShootingCharacter: IShooter
    {
        private readonly ObjectPool<CharacterProjectile> _projectilePool;
        private readonly CharacterShootData _characterShootData;
        private readonly CharacterStatsControl _characterStatsControl;
        private readonly EnemySpawner _enemySpawner;

        public ShootingCharacter(EnemySpawner enemySpawner, CharacterShootData characterShootData,
            CharacterStatsControl characterStatsControl)
        {
            _enemySpawner = enemySpawner;
            _characterShootData = characterShootData;
            _characterStatsControl = characterStatsControl;
            _projectilePool = new ObjectPool<CharacterProjectile>(_characterShootData.CharacterProjectilePrefab,
                _characterShootData.CountProjectile, _characterShootData.ProjectileContainer);
        }

        public void Shoot()
        {
            var projectile = _projectilePool.GetFreeElement();
            SetProjectileData(projectile);
        }

        private void SetProjectileData(CharacterProjectile characterProjectile)
        {
            characterProjectile.OnKilledEnemy += UpdateStats;
            characterProjectile.OnDisableProjectile += Unsubscribe;
            characterProjectile.OnClosestEnemy += FindClosestEnemy;

            characterProjectile.IsRebound = _characterStatsControl.IsReboundProjectile();

            Transform transform;
            (transform = characterProjectile.transform).rotation = Quaternion.Euler(
                _characterShootData.Camera.eulerAngles.x,
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
            var enemiesPools = _enemySpawner.EnemiesPools;
            if (enemiesPools != null)
            {
                foreach (var enemiesPool in enemiesPools)
                {
                    foreach (var enemy in enemiesPool.Value.Pool)
                    {
                        if (enemy.gameObject.activeSelf)
                        {
                            float distance = Vector3.Distance(enemy.transform.position,
                                characterProjectile.gameObject.transform.position);
                            if (distance < minDistance)
                            {
                                closestEnemy = enemy;
                                minDistance = distance;
                            }
                        }
                    }
                }
            }

            characterProjectile.ClosestEnemy = closestEnemy;
        }

        private void UpdateStats()
        {
            foreach (var projectileCharacter in _projectilePool.Pool)
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
                    _characterStatsControl.Heal(_characterStatsControl.Health / 2);
                    projectileCharacter.ResetEnergyValue();
                }
            }
        }
    }
}