using Entities.Enemy.EnemyObject.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy.EnemyObject
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseEnemy : MonoBehaviour, IHeal, IDamageTaker
    {
        public EnemyType EnemyType { get; private set; }
        public float EnergyPoint { get; private set; }
        
        protected Transform Target;
        protected float Attack;
        protected float Health;
        protected float MoveSpeed;
        protected float MaxHealth;
        protected const float MinHealth = 0;

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

        public void Heal(float healthValue)
        {
            Health += healthValue;
            if (Health >= MaxHealth)
            {
                Health = MaxHealth;
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
            Health = MinHealth;
            gameObject.SetActive(false);
        }

        protected virtual void MoveToTarget()
        {
            NavMeshAgent.destination = Target.position;
        }
    }
}