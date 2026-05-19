namespace TI_BookingApi.Domain.Entities;

public class Booking
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

public enum BookingStatus
{
    Pending,
    Confirmed,
    Cancelled
}