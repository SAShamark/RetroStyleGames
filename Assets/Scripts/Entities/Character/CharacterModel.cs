namespace Entities.Character
{
    public class CharacterModel
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

        public CharacterController(float health, float power)
        {
            _health = health;
            _maxHealth = _health;
            _power = power;
        }

        public void IncreaseKillCount()
        {
            KillCount++;
        }

        public void IncreasePower(float value)
        {
            _power += value;
            if (_power >= MaxPower)
            {
                _power = MaxPower;
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
            if (_power < MinPower)
            {
                _power = MinPower;
            }
        }

        public void DecreaseHealth(float value)
        {
            _health -= value;
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