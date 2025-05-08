using WebAPI.Models;
using WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.Order;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? SearchTerm, int? pageIndex,  int? pageSize)
        {
            var orders = await _orderRepository.GetAllAsync(SearchTerm, pageSize, pageIndex);
            return Ok(orders);
        }

        /*[HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }*/

        [HttpPost, Authorize] 
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto dto)
        {
            var order = await _orderRepository.AddAsync(dto);
            return Ok(order);
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> Update(PutOrderDto dto)
        {
            var order = await _orderRepository.UpdateAsync(dto);
            return Ok(order);
        }

        [HttpDelete, Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            await _orderRepository.DeleteAsync(id);
            return Ok($"Order with ID {id} has been deleted successfully.");
        }

        [HttpPut("Update-order-items"), Authorize]
        public async Task<IActionResult> UpdateOrderItems(Guid orderId, List<PutOrderItemDto> dto)
        {
            await _orderRepository.UpdateOrderItemsAsync(orderId, dto); 
            return NoContent();
        }
    }
}
