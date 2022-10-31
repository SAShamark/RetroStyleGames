namespace UI
{
    internal interface IViewController
    {
        void Initialize(params object[] args);
        void Dispose();
    }
}