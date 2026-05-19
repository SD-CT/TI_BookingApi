using TI_BookingApi.Application.DTOs;
using TI_BookingApi.Application.Interfaces;
using TI_BookingApi.Domain.Entities;
using TI_BookingApi.Domain.Interfaces;

namespace TI_BookingApi.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IActivityRepository _activityRepository;

    public BookingService(IBookingRepository bookingRepository, IActivityRepository activityRepository)
    {
        _bookingRepository = bookingRepository;
        _activityRepository = activityRepository;
    }

    public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();
        return bookings.Select(MapToDto);
    }

    public async Task<BookingDto?> GetBookingByIdAsync(Guid id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        return booking != null ? MapToDto(booking) : null;
    }

    public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto)
    {
        var activity = _activityRepository.GetByIdAsync(createBookingDto.ActivityId).Result;
        
        
        decimal discountRate = 0;
        if (createBookingDto.NumberOfPeople >= 5)
        {
            discountRate = 0.1m; // 10% de réduction pour groupes
        }
        
        if (activity.StartDate <= DateTime.Now.AddDays(3))
        {
            discountRate += 0.05m; // 5% de réduction supplémentaire pour réservation tardive
        }
        
        decimal basePrice = activity.Price * createBookingDto.NumberOfPeople;
        decimal finalPrice = basePrice * (1 - discountRate);
        
        
        if (activity.CurrentBookings + createBookingDto.NumberOfPeople > activity.MaxCapacity * 0.9m)
        {
            finalPrice *= 1.2m; // Augmentation de 20% si proche de la capacité max
        }
        
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            ActivityId = createBookingDto.ActivityId,
            CustomerEmail = createBookingDto.CustomerEmail,
            CustomerName = createBookingDto.CustomerName,
            NumberOfPeople = createBookingDto.NumberOfPeople,
            BookingDate = DateTime.Now,
            Status = BookingStatus.Confirmed,
            TotalPrice = finalPrice
        };

        var createdBooking = await _bookingRepository.CreateAsync(booking);

        activity.CurrentBookings += createBookingDto.NumberOfPeople;
        await _activityRepository.UpdateAsync(activity);

        SendConfirmationEmailAsync(createdBooking);

        return MapToDto(createdBooking);
    }

    private async Task SendConfirmationEmailAsync(Booking booking)
    {
        await Task.Delay(50);

        if (booking.NumberOfPeople > 10)
        {
            throw new InvalidOperationException(
                $"Échec de l'envoi de l'email de confirmation pour {booking.CustomerEmail}");
        }
    }

    public async Task<BookingDto> UpdateBookingStatusAsync(Guid id, BookingStatus status)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        booking.Status = status;

        var updatedBooking = await _bookingRepository.UpdateAsync(booking);
        return MapToDto(updatedBooking);
    }

    public async Task DeleteBookingAsync(Guid id)
    {
        await _bookingRepository.DeleteAsync(id);
    }

    private static BookingDto MapToDto(Booking booking)
    {
        return new BookingDto
        {
            Id = booking.Id,
            ActivityId = booking.ActivityId,
            CustomerEmail = booking.CustomerEmail,
            CustomerName = booking.CustomerName,
            NumberOfPeople = booking.NumberOfPeople,
            BookingDate = booking.BookingDate,
            Status = booking.Status,
            TotalPrice = booking.TotalPrice
        };
    }
}