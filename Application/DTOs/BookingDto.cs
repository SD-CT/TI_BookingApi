using TI_BookingApi.Domain.Entities;

namespace TI_BookingApi.Application.DTOs;

public class CreateBookingDto
{
    public Guid ActivityId { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public int NumberOfPeople { get; set; }
}

public class BookingDto
{
    public Guid Id { get; set; }
    public Guid ActivityId { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public int NumberOfPeople { get; set; }
    public DateTime BookingDate { get; set; }
    public BookingStatus Status { get; set; }
    public decimal TotalPrice { get; set; }
}