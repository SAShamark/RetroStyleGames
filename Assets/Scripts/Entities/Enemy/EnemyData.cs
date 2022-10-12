using System;

namespace Entities.Enemy
{
    [Serializable]
    public class EnemyData
    {
        public EnemyType EnemyType;
        public BaseEnemy BaseEnemy;
        public EnemyStaticData EnemyStaticData;
    }
}