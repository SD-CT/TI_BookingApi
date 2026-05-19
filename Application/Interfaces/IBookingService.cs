using TI_BookingApi.Application.DTOs;

namespace TI_BookingApi.Application.Interfaces;

public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
    Task<BookingDto?> GetBookingByIdAsync(Guid id);
    Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto);
    Task<BookingDto> UpdateBookingStatusAsync(Guid id, Domain.Entities.BookingStatus status);
    Task DeleteBookingAsync(Guid id);
}