namespace Services
{
    public interface IServiceLocator
    {
        void Register<T>(T data);
        void Unregister<T>();
        T Resolve<T>();
    }
}