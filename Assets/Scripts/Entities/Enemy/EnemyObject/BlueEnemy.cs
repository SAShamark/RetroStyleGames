using System.Collections;
using Services;
using UnityEngine;

namespace Entities.Enemy.EnemyObject
{
    public class BlueEnemy : BaseEnemy, IShooter
    {
        [SerializeField] private EnemyProjectile _enemyProjectile;
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private int _countProjectile;
        [SerializeField] private float _cooldown = 3;

        private ObjectPool<EnemyProjectile> _projectilePool;
        private bool _shoot;
        private IEnumerator _reloadCoroutine;

        protected void Start()
        {
            _projectilePool = new ObjectPool<EnemyProjectile>(_enemyProjectile, _countProjectile, transform);
            _reloadCoroutine = Reload();
            StartCoroutine(_reloadCoroutine);
        }

        private void Update()
        {
            if (CanMove())
            {
                MoveToTarget();
            }
        }

        private void OnDestroy()
        {
            if (_reloadCoroutine != null)
            {
                StopCoroutine(_reloadCoroutine);
            }

            foreach (var projectile in _projectilePool.Pool)
            {
                Destroy(projectile.gameObject);
            }

            _projectilePool.Pool.Clear();
        }

        private IEnumerator Reload()
        {
            var delay = new WaitForSeconds(_cooldown);

            while (true)
            {
                yield return delay;
                if (!CanMove())
                {
                    Shoot();
                }
            }
        }

        public void Shoot()
        {
            var projectile = _projectilePool.GetFreeElement();
            projectile.AttackValue = Attack;
            projectile.transform.position = _projectileStartPosition.position;
            projectile.Target = Target;
        }

        public override void ChangeTarget(Vector3 newTarget)
        {
            foreach (var projectileControl in _projectilePool.Pool)
            {
                projectileControl.ChangeTargetPosition(newTarget);
            }
        }

        private bool CanMove()
        {
            return Vector3.Distance(Target.position, transform.position) > NavMeshAgent.stoppingDistance;
        }
    }
}