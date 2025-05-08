using WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebAPI.Repositories
{
    public class EFMenuRepository : IMenuRepository
    {
        private readonly AppDbContext _context;

        public EFMenuRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetAllAsync(int? pageSize, int? pageIndex, string? SearchTerm)
        {
            if (pageSize == null || pageSize <= 0)
            {
                pageSize = 10;
            }
            if (pageIndex == null || pageIndex <= 0)
            {
                pageIndex = 1;
            }
            var item = _context.Menus.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                item = item.Where(m => m.Name.ToLower().Contains(SearchTerm.ToLower()));
            }
            return await item.Skip((int)(pageSize * (pageIndex - 1))).Take((int)pageSize).ToListAsync();
            //return await _context.Menus.ToListAsync();
        }

        public async Task<Menu> GetByIdAsync(Guid id)
        {
            return await _context.Menus.FindAsync(id);
        }

        public async Task<Menu> AddAsync(string name)
        {
            var menu = new Menu
            {
                MenuId = Guid.NewGuid(),
                Name = name,

            };
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task<Menu> UpdateAsync(Guid id, string newName)
        {
            var existingMenu = await _context.Menus.FindAsync(id);
            if (existingMenu == null)
                throw new Exception("Menu not found");
            existingMenu.Name = newName;
            _context.Update(existingMenu);
            await _context.SaveChangesAsync();
            return existingMenu;
        }

        public async Task DeleteAsync(Guid id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Menu>> GetByNameAsync(string name, int? pageSize, int? pageIndex)
        {
            if (pageSize == null || pageSize <= 0)
            {
                pageSize = 10;
            }
            if (pageIndex == null || pageIndex <= 0)
            {
                pageIndex = 1;
            }
            var items = _context.Menus.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                items = items.Where(m => m.Name.ToLower().Contains(name.ToLower()));
            }
            var result = await items.Skip((int)(pageSize * (pageIndex - 1))).Take((int)pageSize).ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Menu>> GetByNameAsync(string name)
        //{
        //    var items = _context.Menus.AsQueryable();
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        items = items.Where(m => m.Name.Contains(name));
        //    }
        //    return items;
        //}

        /*public async Task<IEnumerable<Food>> GetAllFoodAsync(Guid Id, string? SearchTerm, int? pageSize, int? pageIndex)
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
        }*/
    }
}
