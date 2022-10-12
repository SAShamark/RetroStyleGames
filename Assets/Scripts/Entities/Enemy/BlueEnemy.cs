using System.Collections;
using UnityEngine;

namespace Entities.Enemy
{
    public class BlueEnemy : BaseEnemy
    {
        [SerializeField] private ProjectileControl _projectile;
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private int _countProjectile;
        private float _shootDelay = 3;
        private ObjectPool<ProjectileControl> objectPool;
        private IEnumerator _shootCorutine;

        private void Start()
        {
            objectPool = new ObjectPool<ProjectileControl>(_projectile, _countProjectile, transform);
            _shootCorutine = Shoot();
        }

        private void Update()
        {
            /*if (Vector3.Distance(Target.position, transform.position) <= _our.stoppingDistance)
            {
                StartCoroutine(_shootCorutine);
            }
            else
            {
                MoveToTarget();
            }*/
        }

        private IEnumerator Shoot()
        {
            var delay = new WaitForSeconds(_shootDelay);

            while (true)
            {
                yield return delay;

                var projectile = objectPool.GetFreeElement();
                projectile.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                projectile.transform.position = _projectileStartPosition.position;
            }
        }
    }
}