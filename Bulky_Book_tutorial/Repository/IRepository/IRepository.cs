using System.Linq.Expressions;

namespace Bulky_Book_tutorial.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        IEnumerable<T> GetAll();

        T GetFirstOrDefault(Expression<Func<T, bool>> filter);

        void Add(T entity);
        void RemoveRange(IEnumerable<T> entity);

    }
}
