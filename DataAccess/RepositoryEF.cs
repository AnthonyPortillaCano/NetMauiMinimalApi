using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess
{
    public class RepositoryEF<T>:IRepositoryEF<T> where T : class
    {
        protected readonly DbContext _context;
        public RepositoryEF(DbContext context)
        {
            _context=context;
        }
         public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Update(T entity)
        {
            _context.Update(entity);
        }
        public void UpdateFieldsSave(T entity,params Expression<Func<T, object>>[] includeProperties) 
        {
                 var dbEntry=_context.Entry(entity);
             foreach(var property in includeProperties)
            {
                dbEntry.Property(property).IsModified = true;
            }
        }
        public async Task<T> Obtener<T> (Expression<Func<T, bool>> condicion) where T : class
        {
            return await _context.Set<T>().FirstOrDefaultAsync(condicion);
        }
        public async Task<List<T>> ObtenerList<T>(Expression<Func<T, bool>> condicion) where T : class
        {
            return await _context.Set<T>().Where(condicion).ToListAsync();
        }
        public async Task Insert(T entity)
        {
            await _context.AddAsync(entity);
        }
        public async Task Delete(Expression<Func<T, bool>> condicion)
        {
            await _context.Set<T>().Where(condicion).ExecuteDeleteAsync();
        }
        public void DeleteList(List<T> entity)
        {
            _context.RemoveRange(entity);
        }

    }
}
