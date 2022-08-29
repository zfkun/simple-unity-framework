namespace Suf.Factory
{
    public interface IFactory<T>
    {
        T Create();
    }
}