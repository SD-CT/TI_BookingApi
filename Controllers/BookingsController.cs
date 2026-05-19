using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TI_BookingApi.Application.DTOs;
using TI_BookingApi.Application.Interfaces;
using TI_BookingApi.Domain.Entities;

namespace TI_BookingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[SwaggerTag("Gestion des réservations d'activités")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
    {
        var bookings = await _bookingService.GetAllBookingsAsync();
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingDto>> GetBooking(Guid id)
    {
        var booking = await _bookingService.GetBookingByIdAsync(id);
        if (booking == null)
        {
            return NotFound();
        }
        return Ok(booking);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Crée une nouvelle réservation",
        Description = "Effectue une réservation pour une activité donnée"
    )]
    [SwaggerResponse(201, "Réservation créée avec succès", typeof(BookingDto))]
    [SwaggerResponse(400, "Données invalides ou capacité insuffisante")]
    [SwaggerResponse(404, "Activité non trouvée")]
    public async Task<ActionResult<BookingDto>> CreateBooking(CreateBookingDto createBookingDto)
    {
        try
        {
            var booking = await _bookingService.CreateBookingAsync(createBookingDto);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }
        catch (Exception ex)
        {
            return BadRequest("Something went wrong");
        }
    }

    [HttpPost("{id}/status")]
    public async Task<ActionResult<BookingDto>> UpdateBookingStatus(Guid id, [FromBody] BookingStatus status)
    {
        var booking = await _bookingService.UpdateBookingStatusAsync(id, status);
        return Ok(booking);
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> DeleteBooking(Guid id)
    {
        await _bookingService.DeleteBookingAsync(id);
        return NoContent();
    }
}