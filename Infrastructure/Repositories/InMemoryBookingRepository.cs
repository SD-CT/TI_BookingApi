using TI_BookingApi.Domain.Entities;
using TI_BookingApi.Domain.Interfaces;

namespace TI_BookingApi.Infrastructure.Repositories;

public class InMemoryBookingRepository : IBookingRepository
{
    private readonly List<Booking> _bookings = new();

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        await Task.Delay(10);
        return _bookings;
    }

    public async Task<Booking?> GetByIdAsync(Guid id)
    {
        await Task.Delay(10);
        return _bookings.FirstOrDefault(b => b.Id == id);
    }

    public async Task<IEnumerable<Booking>> GetByActivityIdAsync(Guid activityId)
    {
        await Task.Delay(10);
        return _bookings.Where(b => b.ActivityId == activityId).ToList();
    }

    public async Task<Booking> CreateAsync(Booking booking)
    {
        await Task.Delay(10);
        _bookings.Add(booking);
        return booking;
    }

    public async Task<Booking> UpdateAsync(Booking booking)
    {
        await Task.Delay(10);
        var existingIndex = _bookings.FindIndex(b => b.Id == booking.Id);
        if (existingIndex >= 0)
        {
            _bookings[existingIndex] = booking;
        }
        return booking;
    }

    public async Task DeleteAsync(Guid id)
    {
        await Task.Delay(10);
        var booking = _bookings.FirstOrDefault(b => b.Id == id);
        if (booking != null)
        {
            _bookings.Remove(booking);
        }
    }
}