using System;
using Entities.Character;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy.EnemyObject
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        public Transform Target { get; set; }
        public EnemyType EnemyType { get; private set; }
        public float EnergyPoint { get; private set; }
        public float MoveSpeed { get; private set; }
        public float Health { get; private set; }
        public float Attack { get; private set; }
        
        public event Action<BaseEnemy> OnDeath;

        protected NavMeshAgent NavMeshAgent;
        private const float MinHealth = 0;
        
        public void Init(EnemyStaticData enemyStaticData, Transform transformTarget)
        {
            Target = transformTarget;
            EnemyType = enemyStaticData.Type;
            MoveSpeed = enemyStaticData.MoveSpeed;
            Health = enemyStaticData.Health;
            Attack = enemyStaticData.Attack;
            EnergyPoint = enemyStaticData.EnergyPoint;
            NavMeshAgent = GetComponent<NavMeshAgent>();
            NavMeshAgent.speed = MoveSpeed;
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

        public abstract void ChangeTarget(Vector3 newTarget);


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