using System;
using Entities.Character.Data;
using UI;

namespace Entities.Character.Controllers
{
    public class CharacterStatsControl : IHeal, IDamageTaker
    {
        public event Action OnChangeHealth;
        public event Action OnChangePower;
        public event Action OnChangeKillCount;
        public event Action<bool> OnMaxPower;
        public event Action<GameTab> OnChangeTab;
        public int KillCount { get; private set; }
        public float Health { get; private set; }
        public float Power { get; private set; }

        private readonly float _maxHealth;
        private const float MaxPower = 100;
        private const float MinPower = 0;
        private const float MinHealth = 0;

        public CharacterStatsControl(CharacterData characterData)
        {
            Health = characterData.Health;
            _maxHealth = Health;
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
        public void Heal(float healthValue)
        {
            Health += healthValue;
            if (Health >= _maxHealth)
            {
                Health = _maxHealth;
            }

            OnChangeHealth?.Invoke();
        }


        public void TakeDamage(float damageValue)
        {
            Health -= damageValue;
            if (Health < MinHealth)
            {
                Health = MinHealth;
                Death();
            }

            OnChangeHealth?.Invoke();
        }

        public void Death()
        {
            OnChangeTab?.Invoke(GameTab.Death);
            OnChangeKillCount?.Invoke();
        }

        public bool IsReboundProjectile()
        {
            return _maxHealth - Health > 10;
        }
    }
}