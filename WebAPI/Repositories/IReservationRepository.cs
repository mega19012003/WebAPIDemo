using WebAPI.Dtos.Reservation;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllAsync(string? searchTerm, int? pageIndex, int? pageSize);
        Task<Reservation> GetByIdAsync(Guid id);
        Task<Reservation> AddAsync(PostReservationDto dto);
        Task<Reservation> UpdateAsync(PutReservationDto dto);
        Task DeleteAsync(Guid id);
    }
}
