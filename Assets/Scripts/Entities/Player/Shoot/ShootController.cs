using UnityEngine;

namespace Entities.Player.Shoot
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private ProjectileControl _projectile;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private int _countProjectile;
        private ObjectPool<ProjectileControl> objectPool;

        private void Start()
        {
            objectPool = new ObjectPool<ProjectileControl>(_projectile, _countProjectile, _container);

            ShootButton.OnShoot += GetProjectile;
        }

        private void OnDestroy()
        {
            ShootButton.OnShoot -= GetProjectile;
        }

        private void GetProjectile()
        {
            var projectile = objectPool.GetFreeElement();
            projectile.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            projectile.transform.position = _projectileStartPosition.position;
        }
    }
}