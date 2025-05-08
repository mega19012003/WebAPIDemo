using WebAPI.Dtos.Food;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IFoodRepository
    {
        Task<IEnumerable<Food>> GetAllAsync();
        Task<Food> GetByIdAsync(Guid id);
        Task<Food> AddAsync(PostFoodDto dto);
        Task<Food> UpdateAsync(PutFoodDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Food>> GetAllFoodAsync(Guid Id, string? SearchTerm, int? pageSize, int? pageIndex);
    }
}
