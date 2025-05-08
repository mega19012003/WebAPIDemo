using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dtos.Table;
using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI.Repositories
{
    public class EFTableRepository : ITableRepository
    {
        private readonly AppDbContext _context;

        public EFTableRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Table>> GetAllAsync()
        {
            return await _context.Tables.ToListAsync();
        }

        public async Task<Table> GetByIdAsync(Guid id)
        {
            return await _context.Tables.FindAsync(id);
        }

        public async Task<Table> AddAsync(PostTableDto dto)
        {
            var table = new Table
            {
                TableId = Guid.NewGuid(),
                TableName = dto.TableName,
                Seats = dto.Seats
            };
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
            return table;
        }

        public async Task<Table> UpdateAsync(PutTableDto dto)
        {
            var existing = await _context.Tables.FindAsync(dto.TableId);
            if (existing == null)
                throw new Exception("Table not found");

            existing.TableName = dto.TableName;
            existing.Seats = dto.Seats;

            _context.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteAsync(Guid id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table != null)
            {
                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }
        }
    }
}
