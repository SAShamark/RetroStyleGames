using System.Collections.Generic;
using Entities.Character.Data;
using UnityEngine;

namespace Entities.Character.Abilities
{
    public class ShootingCharacter
    {
        private readonly ObjectPool<Projectile> _objectPool;
        private readonly CharacterShootData _characterShootData;
        private readonly CharacterStatsControl _characterStatsControl;
        private List<ProjectileCharacter> ProjectileCharacters { get; }

        public ShootingCharacter(CharacterShootData characterShootData, CharacterStatsControl characterStatsControl)
        {
            _characterShootData = characterShootData;
            _characterStatsControl = characterStatsControl;
            _objectPool = new ObjectPool<Projectile>(_characterShootData.ProjectilePrefab,
                _characterShootData.CountProjectile);
            ProjectileCharacters = new List<ProjectileCharacter>();
        }

        public void GetProjectile()
        {
            var projectile = _objectPool.GetFreeElement();

            var projectileCharacter = projectile.GetComponent<ProjectileCharacter>();
            ProjectileCharacters.Add(projectileCharacter);
            projectileCharacter.IsRebound = _characterStatsControl.IsReboundProjectile();
            SetProjectileData(projectileCharacter);
        }

        private void SetProjectileData(ProjectileCharacter projectileCharacter)
        {
            projectileCharacter.OnKilledEnemy += UpdateStats;
            projectileCharacter.OnDisableProjectile += CalculateKillNumber;
            projectileCharacter.OnDisableProjectile += Unsubscribe;

            projectileCharacter.transform.rotation = Quaternion.Euler(_characterShootData.Camera.eulerAngles.x,
                _characterShootData.Character.eulerAngles.y, 0);

            projectileCharacter.transform.position = _characterShootData.ProjectileStartPosition.position;
        }

        private void Unsubscribe(ProjectileCharacter projectileCharacter)
        {
            projectileCharacter.OnKilledEnemy -= UpdateStats;
            projectileCharacter.OnDisableProjectile -= Unsubscribe;
        }

        private void UpdateStats()
        {
            foreach (var projectileCharacter in ProjectileCharacters)
            {
                bool addPower = projectileCharacter.KillCount == 1;
                bool addPowerAndHealth = projectileCharacter.KillCount == 2;

                if (addPower)
                {
                    _characterStatsControl.IncreasePower(projectileCharacter.EnergyValue);
                    projectileCharacter.ResetEnergyValue();
                }
                else if (addPowerAndHealth)
                {
                    _characterStatsControl.IncreasePower(projectileCharacter.EnergyValue);
                    _characterStatsControl.IncreaseHealth(_characterStatsControl.Health / 2);
                    projectileCharacter.ResetEnergyValue();
                }
            }
        }

        private void CalculateKillNumber(ProjectileCharacter projectileCharacter)
        {
            for (int i = 0; i < projectileCharacter.KillCount; i++)
            {
                _characterStatsControl.IncreaseKillCount();
                projectileCharacter.DecreaseKillCount();
            }
        }
    }
}