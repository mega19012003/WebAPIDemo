using System.Reflection.Metadata.Ecma335;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dtos.Food;
using WebAPI.Services;

namespace WebAPI.Repositories
{
    public class EFFoodRepository : IFoodRepository
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;
        
        public EFFoodRepository(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<IEnumerable<Food>> GetAllAsync()
        {
            return await _context.Foods.ToListAsync();
        }

        public async Task<IEnumerable<Food>> GetAllFoodAsync(Guid Id, string? SearchTerm, int? pageSize, int? pageIndex)
        {

            if (pageSize == null || pageSize <= 0)
            {
                pageSize = 10;
            }
            if (pageIndex == null || pageIndex <= 0)
            {
                pageIndex = 1;
            }
            var items = _context.Foods.AsQueryable();
            if (Id != null)
            {
                items = items.Where(m => m.Id.Equals(Id));
            }
            return await _context.Foods.ToListAsync();
        }

        public async Task<Food> GetByIdAsync(Guid id)
        {
            return await _context.Foods.FindAsync(id);
        }

        public async Task<Food> AddAsync(PostFoodDto dto)
        {
            var food = new Food
            {
                Id = Guid.NewGuid(),
                MenuId = dto.MenuId,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImgUrl = new List<string>()
            };

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            food.ImgUrl = await _fileService.SaveFileAsync(dto.Img, uploadsFolder);

            await _context.Foods.AddAsync(food);
            await _context.SaveChangesAsync();
            return food;
        }

        public async Task<Food> UpdateAsync(PutFoodDto dto)
        {
            /*_context.Foods.Update(food);
            await _context.SaveChangesAsync();*/
            var existingFood = await _context.Foods.FindAsync(dto.FoodId);
            if (existingFood == null)
                throw new Exception("Food not found");
            existingFood.Name = dto.Name;
            existingFood.Description = dto.Description;
            existingFood.Price = dto.Price;
            existingFood.MenuId = dto.MenuId;

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            existingFood.ImgUrl = await _fileService.UpdateFileAsync(dto.Img, uploadsFolder, existingFood.ImgUrl);

            _context.Foods.Update(existingFood);
            await _context.SaveChangesAsync();
            return existingFood;
        }

        public async Task DeleteAsync(Guid id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food != null)
            {
                _context.Foods.Remove(food);
                await _context.SaveChangesAsync();
            }
        }
    }
}
