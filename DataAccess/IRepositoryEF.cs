using System.Linq.Expressions;

namespace DataAccess
{
    public interface IRepositoryEF<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetByIdAsync(int id);

        T GetById(int id);
        void Update(T entity);

        void UpdateFieldsSave(T entity, params Expression<Func<T, object>>[] includeProperties);

        Task<T> Obtener<T>(Expression<Func<T, bool>> condicion) where T : class;
        Task<List<T>> ObtenerList<T>(Expression<Func<T, bool>> condicion) where T : class;
        Task Insert(T entity);
        Task Delete(Expression<Func<T, bool>> condicion);
        void DeleteList(List<T> entity);
    }
}
