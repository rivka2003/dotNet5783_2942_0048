
namespace DalApi
{
    public interface ICrude <T> where T: struct
    {
        int Add(T value);
        void Delete(int value);
        void Update(T value);
        IEnumerable<T?> RequestAllByPredicate(Func<T?, bool>?  predicate = null);
        T RequestByPredicate(Func<T?, bool>? predicate);
    }
}
