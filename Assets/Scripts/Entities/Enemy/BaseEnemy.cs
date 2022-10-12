using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        public EnemyType Type;
        public Transform Target { get; set; }
        public EnemyStaticData EnemyStaticData { get; set; }
        public float MoveSpeed { get; private set; }
        public float Health { get; private set; }
        public float Attack { get; private set; }

        protected NavMeshAgent _our;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            Type = EnemyStaticData.Type;
            MoveSpeed = EnemyStaticData.MoveSpeed;
            Health = EnemyStaticData.Health;
            Attack = EnemyStaticData.Attack;
            _our = GetComponent<NavMeshAgent>();
            _our.speed = MoveSpeed;
        }

        protected void MoveToTarget()
        {
            _our.destination = Target.position;
        }
    }
}