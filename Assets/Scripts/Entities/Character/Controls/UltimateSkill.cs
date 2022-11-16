using System;
using System.Linq;
using Entities.Enemy;

namespace Entities.Character.Controllers
{
    public class UltimateSkill
    {
        public event Action<bool> OnUltimateSkillButton;
        private readonly EnemySpawner _enemySpawner;
        private readonly CharacterStatsControl _characterStatsControl;

        public UltimateSkill(EnemySpawner enemySpawner,CharacterStatsControl characterStatsControl)
        {
            _enemySpawner = enemySpawner;
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
            var enemiesPools = _enemySpawner.EnemiesPools;
            if (enemiesPools != null)
            {
                foreach (var enemy in enemiesPools.SelectMany(enemiesPool => enemiesPool.Value.Pool))
                {
                    enemy.Death();
                    _characterStatsControl.IncreaseKillCount();
                }
            }
        }
    }
}