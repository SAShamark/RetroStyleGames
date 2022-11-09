using System;
using System.Linq;

namespace Entities.Character.Abilities
{
    public class UltimateSkill
    {
        public event Action<bool> OnUltimateSkillButton;
        private readonly ApplicationStart _applicationStart;
        private readonly CharacterStatsControl _characterStatsControl;

        public UltimateSkill(ApplicationStart applicationStart,CharacterStatsControl characterStatsControl)
        {
            _applicationStart = applicationStart;
            _characterStatsControl = characterStatsControl;
        }
        public void UltimatePerformance(bool isActive)
        {
            OnUltimateSkillButton?.Invoke(isActive);
        }

        public void UseSkill()
        {
            KillAll();
            _characterStatsControl.ResetPower();
        }

        private void KillAll()
        {
            var enemiesContainer = _applicationStart.EnemyRegistry.EnemiesContainer;
            
            if (enemiesContainer != null)
            {
                int enemyCount = enemiesContainer.Count;
                for (int i = enemyCount; i > 0; i--)
                {
                    var enemy = enemiesContainer.Last();
                    enemy.DecreaseHealth(enemy.Health);
                    _characterStatsControl.IncreaseKillCount();
                }
            }
        }
    }
}