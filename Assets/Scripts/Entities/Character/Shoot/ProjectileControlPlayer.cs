using System.Collections;
using Entities.Enemy;
using Entities.Enemy.EnemyObject;
using UnityEngine;
using CharacterController = Entities.Character.CharacterController;

namespace Entities.Player.Shoot
{
    public sealed class ProjectileControlPlayer : ProjectileControl
    {
        private CharacterController _characterController;
        private EntitiesFactory _entitiesFactory;
        public int KillCount { get; private set; }
        public float EnergyValue { get; private set; }
        private const int EnemyLayer = 9;
        private BaseEnemy _closestEnemy;

        private void Start()
        {
            _entitiesFactory = EntitiesFactory.Instance;
           // _characterController = CharacterController.Instanse;
            TurnOffProjectileCoroutine = TurnOffProjectile(_lifeTime);
            StartCoroutine(TurnOffProjectileCoroutine);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.gameObject.layer == EnemyLayer)
            {
                var enemy = other.gameObject.GetComponentInParent<BaseEnemy>();
                EnergyValue = GetEnergyPoint(enemy);
                KillCount++;
                enemy.DecreaseHealth(enemy.Health);
                if (KillCount == 1)
                {
                    TryGetNextEnemy();
                }
                else if (KillCount > 1)
                {
                    KillCount = 0;
                    gameObject.SetActive(false);
                    StopCoroutine(TurnOffProjectileCoroutine);
                }
            }
        }

        private void TryGetNextEnemy()
        {
            var doIt = Random.Range(0, 10);
            if (doIt == 0 || _characterController.Health < _characterController.MaxHealth / 10)
            {
                _closestEnemy = FindClosestEnemy();
            }
        }

        private BaseEnemy FindClosestEnemy()
        {
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (var enemy in _entitiesFactory.EnemiesContainer)
            {
                Vector3 diff = enemy.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    _closestEnemy = enemy;
                    distance = curDistance;
                }
            }

            return _closestEnemy;
        }

        private float GetEnergyPoint(BaseEnemy enemy)
        {
            return enemy.gameObject.GetComponent<BaseEnemy>().EnergyPoint;
        }

        public void ResetEnergyValue()
        {
            EnergyValue = 0;
        }

        private IEnumerator TurnOffProjectile(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            gameObject.SetActive(false);
        }

        protected override void Move()
        {
            if (_closestEnemy == null)
            {
                transform.Translate(Vector3.forward * (Time.deltaTime * _moveSpeed));
            }
            else
            {
                /*transform.position =
                    Vector3.MoveTowards(transform.position, _closestEnemy.transform.position, _moveSpeed);*/
                transform.position += (_closestEnemy.transform.position - transform.position).normalized *
                                      (_moveSpeed * Time.deltaTime);
            }
        }
    }
}