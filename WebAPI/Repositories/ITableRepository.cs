using WebAPI.Dtos.Table;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllAsync();
        Task<Table> GetByIdAsync(Guid id);
        Task<Table> AddAsync(PostTableDto dto);
        Task<Table> UpdateAsync(PutTableDto dto);
        Task DeleteAsync(Guid id);
    }
}
