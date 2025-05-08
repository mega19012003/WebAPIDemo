using WebAPI.Models;
using WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.Table;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _tableRepository;

        public TableController(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetAll()
        {
            var tables = await _tableRepository.GetAllAsync();
            return Ok(tables);
        }

        /*[HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return Ok(table);
        }*/

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(PostTableDto dto)
        {
            var table = await _tableRepository.AddAsync(dto);
            return Ok(table);
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> UpdateTable([FromBody] PutTableDto dto)
        {
            var table = await _tableRepository.UpdateAsync(dto);
            return Ok(table);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _tableRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
