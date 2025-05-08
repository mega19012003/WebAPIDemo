using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dtos.Reservation;

namespace WebAPI.Repositories
{
    public class EFReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public EFReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync(string? searchTerm, int? pageIndex, int? pageSize)
        {
            if (pageSize == null || pageSize <= 0)
            {
                pageSize = 10;
            }
            if(pageIndex == null || pageIndex <= 0)
            {
                pageIndex = 1;
            }
            var item = _context.Reservations.AsQueryable();
            if(!string.IsNullOrEmpty(searchTerm))
            {
                item = item.Where(m => m.Email.ToLower().Equals(searchTerm.ToLower()) || m.Fullname.ToLower().Contains(searchTerm.ToLower()) || m.Phone.ToLower().Equals(searchTerm.ToLower()));
            }
            var result = await item.Skip((int)(pageSize * (pageIndex - 1))).Take((int)pageSize).ToListAsync();
            return result;
        }

        public async Task<Reservation> GetByIdAsync(Guid id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task<Reservation> AddAsync(PostReservationDto dto)
        {
            var reservation = new Reservation
            {
                ReservationId = Guid.NewGuid(),
                Email = dto.Email,
                Phone = dto.Phone,
                Fullname = dto.Fullname,
                GuestCount = dto.GuestCount,
                ReservationDate = dto.ReservationDate,
                ReservationTime = dto.ReservationTime,
            };
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        public async Task<Reservation> UpdateAsync(PutReservationDto dto)
        {
            /*_context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();*/
            var existingReservation = await _context.Reservations.FindAsync(dto.ReservationId);
            if(existingReservation == null)
                throw new Exception("Reservation not found");

            existingReservation.isConfirmed = dto.IsConfirmed;
            existingReservation.TableId = dto.TableId;
            existingReservation.ReservationDate = dto.ReservationDate;
            existingReservation.ReservationTime = dto.ReservationTime;
            existingReservation.GuestCount = dto.GuestCount;

            _context.Update(existingReservation);
            await _context.SaveChangesAsync();
            return existingReservation;

        }

        public async Task DeleteAsync(Guid id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
