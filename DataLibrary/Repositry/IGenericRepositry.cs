using DataLibrary.Dtos.GetAll;

namespace DataLibrary.Repositry
{
    public interface IGenericRepositry<T> where T : class
    {
        Task<List<T>> GetAllAsync(GetAllDto getAllDto);
        Task<T> AddAsync(T item);
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        public void SaveChanges();
    }
}
