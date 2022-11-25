
namespace DalApi
{
    public interface ICrude <T>
    {
        int Add(T value);
        IEnumerable<T> GetAll();
        T Get(int value);
        void Delete(int value);
        void Update(T value);
    }
}
