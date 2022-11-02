using System;

namespace Entities.Enemy.EnemyObject.Data
{
    [Serializable]
    public class EnemyData
    {
        public EnemyType EnemyType;
        public BaseEnemy BaseEnemy;
        public EnemyStaticData EnemyStaticData;
    }
}