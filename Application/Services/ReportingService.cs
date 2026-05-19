using TI_BookingApi.Domain.Entities;
using TI_BookingApi.Infrastructure.Repositories;

namespace TI_BookingApi.Application.Services;

public class ReportingService
{
    private readonly InMemoryActivityRepository _activityRepo;
    private readonly InMemoryBookingRepository _bookingRepo;

    public ReportingService(InMemoryActivityRepository activityRepo, InMemoryBookingRepository bookingRepo)
    {
        _activityRepo = activityRepo;
        _bookingRepo = bookingRepo;
    }

    public async Task<string> GenerateComplexBusinessReport()
    {
        var activities = await _activityRepo.GetAllAsync();
        var bookings = await _bookingRepo.GetAllAsync();

        decimal totalRevenue = 0;
        int totalCapacity = 0;
        var occupancyRates = new Dictionary<Guid, decimal>();

        foreach (var activity in activities)
        {
            totalCapacity += activity.MaxCapacity;
            var activityBookings = bookings.Where(b => b.ActivityId == activity.Id && b.Status == BookingStatus.Confirmed);
            
            decimal activityRevenue = activityBookings.Sum(b => b.TotalPrice);
            totalRevenue += activityRevenue;
            
            decimal occupancyRate = activity.MaxCapacity > 0 
                ? (decimal)activityBookings.Sum(b => b.NumberOfPeople) / activity.MaxCapacity 
                : 0;
            occupancyRates[activity.Id] = occupancyRate;
            
            if (activity.StartDate < DateTime.Now && occupancyRate < 0.3m)
            {
                totalRevenue *= 0.95m;
            }
        }

        var report = $"=== RAPPORT BUSINESS ===\n";
        report += $"Généré le: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n";
        report += $"Revenus totaux: {totalRevenue:C}\n";
        report += $"Capacité totale: {totalCapacity}\n";
        report += $"Taux d'occupation moyen: {occupancyRates.Values.Average():P2}\n\n";

        report += "Détails par activité:\n";
        foreach (var activity in activities)
        {
            if (occupancyRates.ContainsKey(activity.Id))
            {
                report += $"- {activity.Name}: {occupancyRates[activity.Id]:P2} occupation\n";
            }
        }

        return report;
    }
}