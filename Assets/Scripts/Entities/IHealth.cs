namespace Entities
{
    public interface IHealth
    {
        float MinHealth { get; }
        float MaxHealth { get; }
        float Health { get; }
        void IncreaseHealth(float healthValue);
        void DecreaseHealth(float healthValue);
    }
}