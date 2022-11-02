using System;
using Entities.Character.Data;
using UI;
using Zenject;

namespace Entities.Character.Abilities
{
    public class CharacterStatsControl
    {
        public event Action OnChangeHealth;
        public event Action OnChangePower;
        public event Action<GameTab> OnDeath;
        public int KillCount { get; private set; }
        public float Health { get; private set; }
        public float Power { get; private set; }
        private float MaxHealth { get; }

        private const float MaxPower = 100;
        private const float MinPower = 0;
        private const float MinHealth = 0;
        private readonly CharacterController _characterController;

        public CharacterStatsControl(CharacterData characterData, CharacterController characterController)
        {
            Health = characterData.Health;
            MaxHealth = Health;
            Power = characterData.Power;

            _characterController = characterController;
        }

        public void IncreaseKillCount()
        {
            KillCount++;
        }

        public void IncreasePower(float powerValue)
        {
            Power += powerValue;
            if (Power >= MaxPower)
            {
                Power = MaxPower;
                _characterController.UltimateSkill.UltimatePerformance(true);
            }

            OnChangePower?.Invoke();
        }

        public void DecreasePower(float powerValue)
        {
            Power -= powerValue;
            _characterController.UltimateSkill.UltimatePerformance(false);
            if (Power < MinPower)
            {
                Power = MinPower;
            }

            OnChangePower?.Invoke();
        }

        public void ResetPower()
        {
            Power = MinPower;
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
            OnDeath?.Invoke(GameTab.Death);
        }

        public bool IsReboundProjectile()
        {
            return MaxHealth - Health > 10;
        }
    }
}