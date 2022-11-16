using System;
using Entities.Enemy.EnemyObject.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy.EnemyObject
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseEnemy : MonoBehaviour, IHeal,IDamageTaker
    {
        protected Transform Target { get; private set; }
        public EnemyType EnemyType { get; private set; }
        public float EnergyPoint { get; private set; }
        public float Health { get; private set; }
        protected float MoveSpeed { get; private set; }
        private const float MinHealth = 0;
        private float _maxHealth;
        protected float Attack { get; private set; }

        protected NavMeshAgent NavMeshAgent;


        public void Init(EnemyStaticData enemyStaticData, Transform targetTransform)
        {
            Target = targetTransform;
            EnemyType = enemyStaticData.Type;
            MoveSpeed = enemyStaticData.MoveSpeed;
            Health = enemyStaticData.Health;
            _maxHealth = Health;
            Attack = enemyStaticData.Attack;
            EnergyPoint = enemyStaticData.EnergyPoint;
            NavMeshAgent = GetComponent<NavMeshAgent>();
            NavMeshAgent.speed = MoveSpeed;
        }

        public abstract void ChangeTarget(Vector3 newTarget);
        
        public void Heal(float healthValue)
        {
            Health += healthValue;
            if (Health >= _maxHealth)
            {
                Health = _maxHealth;
            }
        }
        public void TakeDamage(float damageValue)
        {
            Health -= damageValue;
            if (Health <= MinHealth)
            {
                Health = MinHealth;
                Death();
            }
        }

        public void Death()
        {
            gameObject.SetActive(false);
        }

        protected virtual void MoveToTarget()
        {
            NavMeshAgent.destination = Target.position;
        }
    }
}