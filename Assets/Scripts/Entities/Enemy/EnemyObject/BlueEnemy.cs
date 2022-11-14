using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Enemy.EnemyObject
{
    public class BlueEnemy : BaseEnemy
    {
        [SerializeField] private EnemyProjectile _enemyProjectile;
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private int _countProjectile;
        [SerializeField] private float _cooldown = 3;

        private ObjectPool<EnemyProjectile> _objectPool;
        private bool _shoot;
        private bool _isReloaded;
        private IEnumerator _reloadCoroutine;

        protected void Start()
        {
            _objectPool = new ObjectPool<EnemyProjectile>(_enemyProjectile, _countProjectile, transform);
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

        private IEnumerator Reload()
        {
            var delay = new WaitForSeconds(_cooldown);

            while (true)
            {
                yield return delay;
                _isReloaded = true;
            }
        }

        private void Shoot()
        {
            _isReloaded = false;
            var projectile = _objectPool.GetFreeElement();
            projectile.AttackValue = Attack;
            projectile.transform.position = _projectileStartPosition.position;
            projectile.Target = Target;
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