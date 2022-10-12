using System;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instanse;
        public event Action<bool> OnUltaButton;
        public event Action OnDeath;
        public int KillCount { get; private set; }
        public float Health => _health;
        public float Power => _power;
        public float _maxHealth = 100;
        public float MaxHealth => _maxHealth;
        [SerializeField] private float _health;
        [Range(0, 100)] [SerializeField] private float _power;


        private float _maxPower = 100;
        private float _minPower = 0;
        private float _minHealth = 0;

        private void Awake()
        {
            if (Instanse == null)
            {
                Instanse = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void IncreaseKillCount()
        {
            KillCount++;
        }

        public void IncreasePower(float value)
        {
            _power += value;
            if (_power >= _maxPower)
            {
                _power = _maxPower;
                UpdatePower(true);
            }
        }

        public void IncreaseHealth(float value)
        {
            _health += value;
            if (_health >= _maxHealth)
            {
                _health = _maxHealth;
            }
        }

        private void UpdatePower(bool isActive)
        {
            OnUltaButton?.Invoke(isActive);
        }

        public void DecreasePower(float value)
        {
            _power -= value;
            UpdatePower(false);
            if (_power < _minPower)
            {
                _power = _minPower;
            }
        }

        public void DecreaseHealth(float value)
        {
            _health -= value;
            if (_health < _minHealth)
            {
                _health = _minHealth;
                Death();
            }
        }

        private void Death()
        {
            OnDeath?.Invoke();
        }
    }
}