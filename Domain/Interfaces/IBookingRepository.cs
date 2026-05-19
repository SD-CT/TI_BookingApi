using TI_BookingApi.Domain.Entities;

namespace TI_BookingApi.Domain.Interfaces;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetAllAsync();
    Task<Booking?> GetByIdAsync(Guid id);
    Task<IEnumerable<Booking>> GetByActivityIdAsync(Guid activityId);
    Task<Booking> CreateAsync(Booking booking);
    Task<Booking> UpdateAsync(Booking booking);
    Task DeleteAsync(Guid id);
}