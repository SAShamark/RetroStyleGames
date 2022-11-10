using System.Collections;
using UnityEngine;

namespace Entities.Enemy.EnemyObject
{
    public class BlueEnemy : BaseEnemy
    {
        [SerializeField] private ProjectileEnemy _projectile;
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private int _countProjectile;
        [SerializeField] private float _cooldown = 3;

        private ObjectPool<ProjectileEnemy> _objectPool;
        private bool _shoot;
        private bool _isReloaded;
        private IEnumerator _reloadCoroutine;

        protected void Start()
        {
            _objectPool = new ObjectPool<ProjectileEnemy>(_projectile, _countProjectile, transform);
            _reloadCoroutine = Reload();
            StartCoroutine(_reloadCoroutine);
        }

        private void Update()
        {
            if (CanMove())
            {
                MoveToTarget();
            }
            else if (_isReloaded)
            {
                Shoot();
            }

            bool CanMove()
            {
                return Vector3.Distance(Target.position, transform.position) > NavMeshAgent.stoppingDistance;
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(_reloadCoroutine);

            foreach (var projectile in _objectPool.Pool)
            {
                print(1);
                Destroy(projectile.gameObject);
            }

            _objectPool.Pool.Clear();
        }

        private void Shoot()
        {
            _isReloaded = false;
            var projectile = _objectPool.GetFreeElement();
            projectile.AttackValue = Attack;
            projectile.transform.position = _projectileStartPosition.position;
            projectile.Target = Target;
        }

        private IEnumerator Reload()
        {
            var delay = new WaitForSeconds(_cooldown);

            while (true)
            {
                yield return delay;
                _isReloaded = true;
            }
        }

        public override void ChangeTarget(Vector3 newTarget)
        {
            foreach (var projectileControl in _objectPool.Pool)
            {
                projectileControl.ChangeTargetPosition(newTarget);
            }
        }
    }
}