using System;
using Entities.Enemy.EnemyObject.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy.EnemyObject
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseEnemy : MonoBehaviour, IHealth
    {
        protected Transform Target { get; private set; }
        public EnemyType EnemyType { get; private set; }
        public float EnergyPoint { get; private set; }
        public float MoveSpeed { get; private set; }
        public float MinHealth => 0;
        public float MaxHealth { get; private set; }
        public float Health { get; private set; }
        public float Attack { get; private set; }
        public event Action<BaseEnemy> OnDeath;

        protected NavMeshAgent NavMeshAgent;


        public void Init(EnemyStaticData enemyStaticData, Transform targetTransform)
        {
            Target = targetTransform;
            EnemyType = enemyStaticData.Type;
            MoveSpeed = enemyStaticData.MoveSpeed;
            Health = enemyStaticData.Health;
            MaxHealth = Health;
            Attack = enemyStaticData.Attack;
            EnergyPoint = enemyStaticData.EnergyPoint;
            NavMeshAgent = GetComponent<NavMeshAgent>();
            NavMeshAgent.speed = MoveSpeed;
        }

        public abstract void ChangeTarget(Vector3 newTarget);

        public void IncreaseHealth(float healthValue)
        {
            Health += healthValue;
            if (Health >= MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        public void DecreaseHealth(float value)
        {
            Health -= value;
            if (Health <= MinHealth)
            {
                Health = MinHealth;
                Death();
            }
        }

        private void Death()
        {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }

        protected virtual void MoveToTarget()
        {
            NavMeshAgent.destination = Target.position;
        }
    }
}