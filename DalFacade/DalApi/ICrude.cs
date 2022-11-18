
namespace DalApi
{
    public interface ICrude <T>
    {
        int Add(T value);
        IEnumerable<T> Get();
        T RequestById(int value);
        void Delete(int value);
        void Update(T value);
    }
}
