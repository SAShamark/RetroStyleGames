namespace UI.Panels.GamePlay
{
    public class GamePlayModel
    {
        public float Health { get; private set; }
        public float Power { get; private set; }
        public int KillCount { get; private set; }

        public GamePlayModel(float health, float power, int killCount)
        {
            UpdateHealth(health);
            UpdatePower(power);
            UpdateKillCount(killCount);
        }

        public void UpdateHealth(float health)
        {
            Health = health;
        }

        public void UpdatePower(float power)
        {
            Power = power;
        }

        public void UpdateKillCount(int killCount)
        {
            KillCount = killCount;
        }
    }
}