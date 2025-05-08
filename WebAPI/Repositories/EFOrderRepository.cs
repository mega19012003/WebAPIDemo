using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dtos.Order;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Repositories
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public EFOrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(string? SearchTerm, int? pageSize, int? pageIndex)
        {
            if (pageSize <= 0 || pageSize == null)
            {
                pageSize = 10;
            }
            if(pageIndex <= 0 || pageIndex == null)
            {
                pageIndex = 1;
            }
            var order = _context.Orders.AsQueryable();
            var result = await order.Skip((int)(pageSize * (pageIndex - 1))).Take((int)pageSize).ToListAsync();
            return result;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> AddAsync(OrderDto dto)
        {
            /*await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();*/
            var order = new Order
            {
                Id = Guid.NewGuid(),
                TableId = dto.TableId,
                OrderTime = DateTime.UtcNow,
                OrderItems = dto.OrderItems.Select(i => new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    OrderFoodId = i.FoodId,
                    Quantity = i.Quantity,
                    Subtotal = i.Subtotal
                }).ToList()
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task<Order> UpdateAsync(PutOrderDto dto)
        {
            var existingOrder = await _context.Orders.FindAsync(dto.OrderId);
            if (existingOrder == null)
                throw new Exception("Order not found");

            existingOrder.Status = dto.Status;
            _context.Update(existingOrder);
            await _context.SaveChangesAsync();
            // The issue is here: existingOrder is of type 'Order', but the method expects a return type of 'Menu'.  
            // To fix this, you need to either change the return type of the method to 'Order' or return a 'Menu' object.  

            // Example fix: Change the return type to 'Order'  
            return existingOrder;
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {   
                _context.OrderItems.RemoveRange(order.OrderItems);
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateOrderItemsAsync(Guid orderId, List<PutOrderItemDto> dto)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                foreach (var item in dto)
                {
                    var orderItem = order.OrderItems.FirstOrDefault(i => i.OrderFoodId == item.OrderFoodId);
                    if (orderItem != null)
                    {
                        orderItem.Quantity = item.Quantity;
                        orderItem.Subtotal = item.Subtotal;
                        orderItem.isCancelled = item.isCancelled;
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
