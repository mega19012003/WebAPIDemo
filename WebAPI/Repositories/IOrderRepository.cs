using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.Order;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync(string? SearchTerm, int? pageSize,  int? pageIndex);
        Task<Order> GetByIdAsync(Guid id);
        Task<Order> AddAsync(OrderDto dto);
        Task<Order> UpdateAsync(PutOrderDto dto);
        Task DeleteAsync(Guid id);
        Task UpdateOrderItemsAsync(Guid orderId, List<PutOrderItemDto> dto);
    }
}
