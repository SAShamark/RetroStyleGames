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
        [SerializeField] private float _cooldown = 3;

        private List<ProjectileEnemy> _projectileControls;
        private ObjectPool<Projectile> _objectPool;
        private bool _shoot;
        private bool _isReloaded;

        protected void Start()
        {
            _objectPool = new ObjectPool<Projectile>(_projectile, _countProjectile, transform);
            _projectileControls = new List<ProjectileEnemy>();
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

        private void Shoot()
        {
            var projectile = _objectPool.GetFreeElement();
            var projectileControlEnemy = projectile.GetComponent<ProjectileEnemy>();
            _projectileControls.Add(projectileControlEnemy);

            projectile.AttackValue = Attack;
            projectile.transform.localPosition = _projectileStartPosition.localPosition;
            projectileControlEnemy.Target = Target;

            _isReloaded = false;
            StartCoroutine(Reload());
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
    }
}