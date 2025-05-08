using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllAsync(int? pageSize, int? pageIndex, string? SearchTerm);
        Task<Menu> GetByIdAsync(Guid id);
        Task<Menu> AddAsync(string name);
        Task<Menu> UpdateAsync(Guid id, string name);
        Task DeleteAsync(Guid id);
        //Task<IEnumerable<Menu>> GetByNameAsync(string name);
        Task<IEnumerable<Menu>> GetByNameAsync(string name, int? pageSize, int? pageIndex);
        //Task<IEnumerable<Food>> GetAllFoodAsync(Guid Id, string? SearchTerm, int? pageSize, int? pageIndex);
    }
}
