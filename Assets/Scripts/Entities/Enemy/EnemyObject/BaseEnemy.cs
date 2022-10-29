using System;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy.EnemyObject
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        public event Action<BaseEnemy> OnDeath;
        public EnemyType Type { get; set; }
        public Transform Target { get; set; }
        public EnemyStaticData EnemyStaticData { get; set; }
        public float EnergyPoint { get; private set; }
        public float MoveSpeed { get; private set; }
        public float Health { get; private set; }
        public float Attack { get; private set; }
        private EntitiesFactory _entitiesFactory;

        protected NavMeshAgent Our;
        private float _minHealth = 0;


        protected virtual void Start()
        {
            Init();
        }

        private void Init()
        {
            Type = EnemyStaticData.Type;
            MoveSpeed = EnemyStaticData.MoveSpeed;
            Health = EnemyStaticData.Health;
            Attack = EnemyStaticData.Attack;
            EnergyPoint = EnemyStaticData.EnergyPoint;
            Our = GetComponent<NavMeshAgent>();
            Our.speed = MoveSpeed;
        }

        public void DecreaseHealth(float value)
        {
            Health -= value;
            if (Health <= _minHealth)
            {
                Health = _minHealth;
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
            Our.destination = Target.position;
        }
    }
}