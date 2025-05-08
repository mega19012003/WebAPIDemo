using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IFoodRepository _foodRepository;

        public MenuController(IMenuRepository menuRepository, IFoodRepository foodRepository)
        {
            _menuRepository = menuRepository;
            _foodRepository = foodRepository;
        }

        /*[HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var menus = await _menuRepository.GetAllAsync();
            return Ok(menus);
        }*/

        /*[HttpGet]
        public async Task<IActionResult> GetById(Guid id, [FromQuery] int pageSize, [FromQuery] int pageIndex, [FromQuery] string? SearchTerm)
        {
            var menu = await _menuRepository.GetByIdAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }*/

        [HttpGet]
        public async Task<IActionResult> GetAllFood([FromQuery] Guid id, string? SearchTerm, int? pageSize, int? pageIndex)
        {
            var food = await _foodRepository.GetAllFoodAsync(id, SearchTerm, pageSize, pageIndex);
            return Ok(food);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery]string name)
        {
            var menu = await _menuRepository.AddAsync(name);
            return Ok(menu);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] Guid id, [FromQuery] string newName)
        {
            var menu = await _menuRepository.UpdateAsync(id, newName);
            return Ok(menu);
        }

        [HttpDelete, Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _menuRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetByName([FromQuery] string SearchTerm, [FromQuery] int? pageSize, [FromQuery] int? pageIndex)
        {
            var results = await _menuRepository.GetByNameAsync(SearchTerm, pageSize, pageIndex);
            return Ok(results);
        }

        //[HttpGet("name")]
        //public async Task<IActionResult> GetByName([FromQuery] string SearchTerm, [FromQuery] int pageSize, [FromQuery] int pageIndex)
        //{
        //    var results = await _menuRepository.GetByNameAsync(SearchTerm);
        //    return Ok(results);
        //}
        [HttpGet("all")]
        public async Task<IActionResult> GetAllMenus([FromQuery] int? pageSize, [FromQuery] int? pageIndex, [FromQuery] string? SearchTerm)
        {
            var menus = await _menuRepository.GetAllAsync(pageSize, pageIndex, SearchTerm);
            return Ok(menus);
        }

    }
}
