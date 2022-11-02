using UnityEngine;

namespace Entities.Enemy.EnemyObject.Data
{
    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "Unit/EnemyStaticData", order = 1)]
    public class EnemyStaticData : ScriptableObject
    {
        [SerializeField] private EnemyType _type;
        [SerializeField] private float _attack;
        [SerializeField] private float _health;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _energyPoint;

        public EnemyType Type => _type;
        public float Attack => _attack;
        public float Health => _health;
        public float MoveSpeed => _moveSpeed;

        public float EnergyPoint => _energyPoint;
    }
}