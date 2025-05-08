using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.Food;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;

        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        /*[HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var foods = await _foodRepository.GetAllAsync();
            return Ok(foods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var food = await _foodRepository.GetByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return Ok(food);
        }*/

        [HttpPost]
        [Consumes("multipart/form-data"), Authorize]
        public async Task<IActionResult> CreateFood([FromForm] PostFoodDto dto)
        {
            var food = await _foodRepository.AddAsync(dto);
            return Ok(food);
        }

        [HttpPut]
        [Consumes("multipart/form-data"), Authorize]
        public async Task<IActionResult> Update([FromForm] PutFoodDto dto)
        {
            var food = await _foodRepository.UpdateAsync(dto);
            return Ok(food);
        }


        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _foodRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

