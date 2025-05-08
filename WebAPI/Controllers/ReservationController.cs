using WebAPI.Models;
using WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetAll([FromQuery] string? SearchTerm, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            var reservations = await _reservationRepository.GetAllAsync(SearchTerm, pageIndex, pageSize);
            return Ok(reservations);
        }

        /*[HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }*/

        [HttpPost]
        public async Task<IActionResult> Create(PostReservationDto dto)
        {
            var reservtion = await _reservationRepository.AddAsync(dto);
            return Ok(reservtion);
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> UpdateReservation(PutReservationDto dto)
        {
            /*var reservation = await _reservationRepository.GetByIdAsync(dto.ReservationId);
            if (reservation == null)
                return NotFound();

            reservation.isConfirmed = dto.IsConfirmed;
            reservation.TableId = dto.TableId;
            reservation.ReservationDate = dto.ReservationDate;
            reservation.ReservationTime = dto.ReservationTime;
            reservation.GuestCount = dto.GuestCount;

            await _reservationRepository.UpdateAsync(reservation);
            return NoContent();*/
            var reservation = await _reservationRepository.UpdateAsync(dto);
            return Ok(reservation);
        }
    }
}
