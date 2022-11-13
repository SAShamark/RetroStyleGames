using System;
using Entities.Character.Data;
using UI;

namespace Entities.Character.Abilities
{
    public class CharacterStatsControl : IHealth
    {
        public event Action OnChangeHealth;
        public event Action OnChangePower;
        public event Action OnChangeKillCount;
        public event Action<bool> OnMaxPower;
        public event Action<GameTab> OnChangeTab;
        public int KillCount { get; private set; }
        public float MinHealth => 0;
        public float MaxHealth { get; }
        public float Health { get; private set; }
        public float Power { get; private set; }

        private const float MaxPower = 100;
        private const float MinPower = 0;

        public CharacterStatsControl(CharacterData characterData)
        {
            Health = characterData.Health;
            MaxHealth = Health;
            Power = characterData.Power;
        }

        public void IncreaseKillCount()
        {
            KillCount++;
            OnChangeKillCount?.Invoke();
        }

        public void IncreasePower(float powerValue)
        {
            Power += powerValue;
            if (Power >= MaxPower)
            {
                Power = MaxPower;
                OnMaxPower?.Invoke(true);
            }

            OnChangePower?.Invoke();
        }

        public void DecreasePower(float powerValue)
        {
            Power -= powerValue;
            if (Power < MinPower)
            {
                Power = MinPower;
            }

            OnMaxPower?.Invoke(false);
            OnChangePower?.Invoke();
        }

        public void ResetPower()
        {
            Power = MinPower;
            OnMaxPower?.Invoke(false);
            OnChangePower?.Invoke();
        }

        public void IncreaseHealth(float healthValue)
        {
            Health += healthValue;
            if (Health >= MaxHealth)
            {
                Health = MaxHealth;
            }

            OnChangeHealth?.Invoke();
        }

        public void DecreaseHealth(float healthValue)
        {
            Health -= healthValue;
            if (Health < MinHealth)
            {
                Health = MinHealth;
                Death();
            }

            OnChangeHealth?.Invoke();
        }

        private void Death()
        {
            OnChangeTab?.Invoke(GameTab.Death);
            OnChangeKillCount?.Invoke();
        }

        public bool IsReboundProjectile()
        {
            return MaxHealth - Health > 10;
        }
    }
}