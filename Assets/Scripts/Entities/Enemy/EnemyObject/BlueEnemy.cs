using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemy.EnemyObject
{
    public class BlueEnemy : BaseEnemy
    {
        [SerializeField] private ProjectileControl _projectile;
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private int _countProjectile;
        private List<ProjectileControlEnemy> _projectileControls;
        private ObjectPool<ProjectileControl> _objectPool;
        private IEnumerator _shootCorutine;
        private const float ShootDelay = 3;
        private bool _shoot;

        protected override void Start()
        {
            base.Start();
            _objectPool =
                new ObjectPool<ProjectileControl>(_projectile, _countProjectile, transform);
            _projectileControls = new List<ProjectileControlEnemy>();
            _shootCorutine = Shoot();
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
            if (Vector3.Distance(Target.position, transform.position) <= _our.stoppingDistance && !_shoot)
            {
                _shoot = true;
                StartCoroutine(_shootCorutine);
            }
            else if (!_shoot)
            {
                MoveToTarget();
            }
            else if (Vector3.Distance(Target.position, transform.position) > _our.stoppingDistance)
            {
                StopCoroutine(_shootCorutine);
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
                var projectileControlEnemy = projectile.GetComponent<ProjectileControlEnemy>();
                _projectileControls.Add(projectileControlEnemy);
                projectile.AttackValue = Attack;
                projectile.transform.localPosition = _projectileStartPosition.localPosition;
                projectileControlEnemy.Target = Target;
            }
        }
    }
}