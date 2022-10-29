using System.Linq;
using Entities.Enemy;
using Zenject;

namespace Entities.Character.Abilities
{
    public class UltimateSkill
    {
        private CharacterController _characterController;
        private EnemyFactory _enemyFactory;

        [Inject]
        private void Construct(CharacterController characterController, EnemyFactory enemyFactory)
        {
            _characterController = characterController;
            _enemyFactory = enemyFactory;
        }

        public void UseSkill()
        {
            DestroyAllEnemy();
            ResetPower();
        }

        private void DestroyAllEnemy()
        {
            if (_enemyFactory.EnemyRegistry.EnemiesContainer != null)
            {
                var enemyCount = _enemyFactory.EnemyRegistry.EnemiesContainer.Count;
                for (int i = enemyCount; i > 0; i--)
                {
                    var enemy = _enemyFactory.EnemyRegistry.EnemiesContainer.Last();
                    _enemyFactory.EnemyRegistry.EnemiesContainer.Remove(enemy);
                    enemy.DecreaseHealth(enemy.Health);
                }
            }
        }

        private void ResetPower()
        {
            _characterController.CharacterStatsControl.DecreasePower(_characterController.CharacterStatsControl.Power);
        }
    }
}