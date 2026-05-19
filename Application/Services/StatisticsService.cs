using TI_BookingApi.Application.Interfaces;
using TI_BookingApi.Domain.Interfaces;

namespace TI_BookingApi.Application.Services;

public class StatisticsService
{
    private readonly IActivityRepository _activityRepository;
    private readonly IBookingRepository _bookingRepository;

    public StatisticsService(IActivityRepository activityRepository, IBookingRepository bookingRepository)
    {
        _activityRepository = activityRepository;
        _bookingRepository = bookingRepository;
    }

    public int GetTotalActivities()
    {
        var activities = _activityRepository.GetAllAsync().Result;
        return activities.Count();
    }

    public decimal GetTotalRevenue()
    {
        var bookingsTask = _bookingRepository.GetAllAsync();
        bookingsTask.Wait();
        
        return bookingsTask.Result.Sum(b => b.TotalPrice);
    }

    public string GenerateReport()
    {
        var totalActivities = GetTotalActivities();
        var totalRevenue = GetTotalRevenue();
        var activities = _activityRepository.GetAllAsync().Result;
        
        var report = $"Rapport du {DateTime.Now:yyyy-MM-dd}\n";
        report += $"Nombre total d'activités: {totalActivities}\n";
        report += $"Revenus totaux: {totalRevenue:C}\n";
        report += "Détails des activités:\n";
        
        foreach (var activity in activities)
        {
            report += $"- {activity.Name}: {activity.CurrentBookings}/{activity.MaxCapacity} places réservées\n";
        }
        
        return report;
    }
}