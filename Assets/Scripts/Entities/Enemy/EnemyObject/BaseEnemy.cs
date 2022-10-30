using System;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy.EnemyObject
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseEnemy : MonoBehaviour
    {
        public Transform Target { get; private set; }
        public EnemyType EnemyType { get; private set; }
        public float EnergyPoint { get; private set; }
        public float MoveSpeed { get; private set; }
        public float Health { get; private set; }
        public float Attack { get; private set; }
        public float MinHealth{ get; private set; } = 0;

        //public float MinHealth => this.MinHealth;

        public event Action<BaseEnemy> OnDeath;

        protected NavMeshAgent NavMeshAgent;
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