using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemy.EnemyObject
{
    public class BlueEnemy : BaseEnemy
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private int _countProjectile;
        private List<ProjectileEnemy> _projectileControls;
        private ObjectPool<Projectile> _objectPool;
        private IEnumerator _shootCoroutine;
        private const float ShootDelay = 3;
        private bool _shoot;

        protected void Start()
        {
            _objectPool =
                new ObjectPool<Projectile>(_projectile, _countProjectile, transform);
            _projectileControls = new List<ProjectileEnemy>();
            _shootCoroutine = Shoot();
        }

        public override void ChangeTarget(Vector3 newTarget)
        {
            foreach (var projectileControl in _projectileControls)
            {
                projectileControl.ChangeTargetPosition(newTarget);
            }
        }

        private void Update()
        {
            if (Vector3.Distance(Target.position, transform.position) <= NavMeshAgent.stoppingDistance && !_shoot)
            {
                _shoot = true;
                StartCoroutine(_shootCoroutine);
            }
            else if (!_shoot)
            {
                MoveToTarget();
            }
            else if (Vector3.Distance(Target.position, transform.position) > NavMeshAgent.stoppingDistance)
            {
                StopCoroutine(_shootCoroutine);
                _shoot = false;
            }
        }

        private IEnumerator Shoot()
        {
            var delay = new WaitForSeconds(ShootDelay);

            while (true)
            {
                yield return delay;
                var projectile = _objectPool.GetFreeElement();
                var projectileControlEnemy = projectile.GetComponent<ProjectileEnemy>();
                _projectileControls.Add(projectileControlEnemy);
                projectile.AttackValue = Attack;
                projectile.transform.localPosition = _projectileStartPosition.localPosition;
                projectileControlEnemy.Target = Target;
            }
        }
    }
}