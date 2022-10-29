using System.Collections.Generic;
using Entities.Player.Shoot;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Character.Shoot
{
    public class ShootController : MonoBehaviour
    {
        [FormerlySerializedAs("_playerController")] [SerializeField] private CharacterController _characterController;
        private List<ProjectileControlPlayer> ProjectileControlPlayer { get; set; }
        [SerializeField] private ProjectileControl _projectile;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private int _countProjectile;
        [SerializeField] private Transform _camera;
        private ObjectPool<ProjectileControl> _objectPool;
        private int _updateKillCount;

        private void Start()
        {
            _objectPool = new ObjectPool<ProjectileControl>(_projectile, _countProjectile, _container);
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
            projectile.transform.rotation = Quaternion.Euler(_camera.eulerAngles.x, transform.eulerAngles.y, 0);
            projectile.transform.position = _projectileStartPosition.position;
        }

        private void Update()
        {
            UpdateEnergyPoint();
        }

        private void UpdateEnergyPoint()
        {
            foreach (var projectileControlPlayer in ProjectileControlPlayer)
            {
                if (projectileControlPlayer.KillCount == 1 && _updateKillCount == 0)
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
                }
            }
        }
    }
}