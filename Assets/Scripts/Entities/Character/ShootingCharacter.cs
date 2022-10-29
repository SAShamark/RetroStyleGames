using System.Collections.Generic;
using Entities.Character.Abilities;
using Entities.Character.Data;
using UnityEngine;

namespace Entities.Character
{
    public class ShootingCharacter
    {
        /*private List<ProjectileControlPlayer> ProjectileControlPlayer { get; set; }
        private ObjectPool<ProjectileControl> _objectPool;
        private CharacterShootData _characterShootData;
        private int _updateKillCount;

        private void Start()
        {
            _objectPool = new ObjectPool<ProjectileControl>(_characterShootData.Projectile, _characterShootData.CountProjectile, _characterShootData.Container);
            ProjectileControlPlayer = new List<ProjectileControlPlayer>();
            UIPanelController.OnShoot += GetProjectile;
        }

        private void OnDestroy()
        {
            UIPanelController.OnShoot -= GetProjectile;
        }

        private void GetProjectile()
        {
            _updateKillCount = 0;
            var projectile = _objectPool.GetFreeElement();
            var projectileControlPlayer = projectile.GetComponent<ProjectileControlPlayer>();
            ProjectileControlPlayer.Add(projectileControlPlayer);
            projectile.transform.rotation = Quaternion.Euler(_characterShootData.Camera.eulerAngles.x, transform.eulerAngles.y, 0);
            projectile.transform.position = _characterShootData.ProjectileStartPosition.position;
        }

        private void Update()
        {
            UpdateEnergyPoint();
        }

        private void UpdateEnergyPoint()
        {
            foreach (var projectileControlPlayer in ProjectileControlPlayer)
            {
                /*if (projectileControlPlayer.KillCount == 1 && _updateKillCount == 0)
                {
                    _characterController.IncreasePower(projectileControlPlayer.EnergyValue);
                    projectileControlPlayer.ResetEnergyValue();
                    _characterController.IncreaseKillCount();
                    _updateKillCount++;
                }
                else if (projectileControlPlayer.KillCount == 2 && _updateKillCount == 1)
                {
                    _characterController.IncreasePower(projectileControlPlayer.EnergyValue);
                    _characterController.IncreaseHealth(_characterController.Health / 2);
                    projectileControlPlayer.ResetEnergyValue();
                    _characterController.IncreaseKillCount();
                    _updateKillCount++;
                }#1#
            }
        }*/
    }
}