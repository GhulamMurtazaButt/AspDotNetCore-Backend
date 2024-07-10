using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using DataLibrary.Dtos.GetAll;


namespace DataLibrary.Repositry.Impl
{

    public class GenericRepositry<T> : IGenericRepositry<T> where T : class
    {
        private readonly DbSet<T> _dbset;
        private readonly DataContext _context;
        public GenericRepositry(DataContext context)
        {
            _dbset = context.Set<T>();
            _context = context;

        }
        public async Task<List<T>> GetAllAsync(GetAllDto getAllDto)
        {
            IQueryable<T> query = _dbset.AsQueryable();
            if (!string.IsNullOrEmpty(getAllDto.sortBy))
            {
                query = query.OrderBy(getAllDto.sortBy);
            }

            if (!string.IsNullOrEmpty(getAllDto.searchTerm))
            {
                query = query.Where(getAllDto.searchTerm);
            }

            query = query.Skip((getAllDto.page - 1) * getAllDto.limit).Take(getAllDto.limit);
            return query.ToList();
        }

        public async Task<T> AddAsync(T item)
        {
            await _dbset.AddAsync(item);
            return item;

        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _dbset.Update(entity);
            return entity;

        }
        public async Task<T> DeleteAsync(T entity)
        {
            _dbset.Remove(entity);
            return entity;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

}
