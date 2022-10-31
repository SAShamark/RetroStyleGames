using System;
using System.Linq;
using Entities.Enemy;
using Zenject;

namespace Entities.Character.Abilities
{
    public class UltimateSkill
    {
        public event Action<bool> OnUltimateSkillButton;

        private EnemyFactory _enemyFactory;
        private CharacterController _characterController;

        [Inject]
        private void Construct(EnemyFactory enemyFactory,CharacterController characterController)
        {
            _enemyFactory = enemyFactory;
            _characterController = characterController;
        }

        public void UltimatePerformance(bool isActive)
        {
            OnUltimateSkillButton?.Invoke(isActive);
        }
        
        public void UseSkill()
        {
            KillAll();
            _characterController.CharacterStatsControl.ResetPower();
        }

        private void KillAll()
        {
           var enemiesContainer = _enemyFactory.EnemyRegistry.EnemiesContainer;
            if (enemiesContainer != null)
            {
                var enemyCount = enemiesContainer.Count;
                for (int i = enemyCount; i > 0; i--)
                {
                    var enemy = enemiesContainer.Last();
                    enemy.DecreaseHealth(enemy.Health);
                    _characterController.CharacterStatsControl.IncreaseKillCount();
                }
            }
        }
    }
}