using System;
using Entities.Character.Data;

namespace Entities.Character.Abilities
{
    public class CharacterStatsControl
    {
        public event Action<bool> OnUltaButton;
        public event Action OnDeath;
        public int KillCount { get; private set; }
        public float Health => _health;
        public float Power => _power;
        public float MaxHealth => _maxHealth;

        private float _health;
        private float _power;

        private const float MaxPower = 100;
        private const float MinPower = 0;
        private readonly float _maxHealth;
        private const float MinHealth = 0;

        public CharacterStatsControl(CharacterData characterData)
        {
            _health = characterData.Health;
            _maxHealth = _health;
            _power = characterData.Power;
        }

        public void IncreaseKillCount()
        {
            KillCount++;
        }

        public void IncreasePower(float powerValue)
        {
            _power += powerValue;
            if (_power >= MaxPower)
            {
                _power = MaxPower;
                UpdatePower(true);
            }
        }

        public void IncreaseHealth(float healthValue)
        {
            _health += healthValue;
            if (_health >= _maxHealth)
            {
                _health = _maxHealth;
            }
        }

        private void UpdatePower(bool isActive)
        {
            OnUltaButton?.Invoke(isActive);
        }

        public void DecreasePower(float powerValue)
        {
            _power -= powerValue;
            UpdatePower(false);
            if (_power < MinPower)
            {
                _power = MinPower;
            }
        }

        public void DecreaseHealth(float healthValue)
        {
            _health -= healthValue;
            if (_health < MinHealth)
            {
                _health = MinHealth;
                Death();
            }
        }

        private void Death()
        {
            OnDeath?.Invoke();
        }
    }
}