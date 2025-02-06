using Microsoft.EntityFrameworkCore;
using OA.Data.Interface;
using System.Linq.Expressions;

namespace OA.Repo.Repo
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _context;
        protected DbContext Context;

        public EfRepository(ApplicationContext dbContext)
        {
            Context = dbContext;
            _context = Context.Set<T>();
        }
        public IEnumerable<T> GetAllFiltered(bool search)
        {
            return _context.Local.Where(x => x.Equals(search.Equals(false)));
        }

        public T Get(long id)
        {
            return _context.Find(id);
        }
        public T GetById(int id)
        {
            return _context.Find(id);
        }

        public T GetOne()
        {
            return _context.FirstOrDefault();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.ToListAsync();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {
                return _context.Where(predicate);
            }
            throw new ArgumentNullException();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {
                return await Task.FromResult(_context.Where(predicate));
            }
            throw new ArgumentNullException();
        }

        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {
                return await Task.FromResult(_context.Where(predicate).FirstOrDefault());
            }
            throw new ArgumentNullException();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _context.Add(entity);
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            await _context.AddAsync(entity);
        }



        public void SaveChanges()
        {
            Context.SaveChanges();
        }


        public void InserRange(IEnumerable<T> entities)
        {
            var entityList = entities as IList<T> ?? entities.ToList();
            if (!entityList.Any())
            {
                throw new ArgumentNullException();
            }
            _context.AddRange(entityList);
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            var entityList = entities as IList<T> ?? entities.ToList();
            if (!entityList.Any())
            {
                throw new ArgumentNullException();
            }
            await _context.AddRangeAsync(entityList);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _context.Update(entity);



        }

        public async Task UpdateAsync(IEnumerable<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }


            _context.UpdateRange(entity);

            await Context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _context.Remove(entity);
        }




        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException();
            }
            var entity = Get(id);
            _context.Remove(entity);
        }
    }
}
