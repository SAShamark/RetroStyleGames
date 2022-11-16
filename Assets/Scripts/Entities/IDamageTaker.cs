namespace Entities
{
    public interface IDamageTaker
    {
        void TakeDamage(float damage);
        void Death();
    }
}