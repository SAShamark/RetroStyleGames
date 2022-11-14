namespace Entities
{
    public interface IHealth
    {
        float Health { get; }
        void IncreaseHealth(float healthValue);
        void DecreaseHealth(float healthValue);
    }
}