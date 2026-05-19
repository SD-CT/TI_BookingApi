using Microsoft.AspNetCore.Mvc;
using TI_BookingApi.Application.Services;
using TI_BookingApi.Domain.Entities;
using TI_BookingApi.Infrastructure.Repositories;

namespace TI_BookingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly ReportingService _reportingService;
    private readonly InMemoryActivityRepository _activityRepository;
    private readonly InMemoryBookingRepository _bookingRepository;

    public ReportsController(
        ReportingService reportingService,
        InMemoryActivityRepository activityRepository, 
        InMemoryBookingRepository bookingRepository)
    {
        _reportingService = reportingService;
        _activityRepository = activityRepository;
        _bookingRepository = bookingRepository;
    }

    [HttpGet("business")]
    public async Task<ActionResult<string>> GetBusinessReport()
    {
        var report = await _reportingService.GenerateComplexBusinessReport();
        return Ok(report);
    }

    [HttpGet("quick-stats")]
    public async Task<ActionResult<object>> GetQuickStats()
    {
        var activities = await _activityRepository.GetAllAsync();
        var bookings = await _bookingRepository.GetAllAsync();

        var totalRevenue = bookings.Where(b => b.Status == BookingStatus.Confirmed)
                                  .Sum(b => b.TotalPrice);
        
        var averagePrice = activities.Average(a => a.Price);
        
        var warningActivities = activities.Where(a => 
            a.StartDate > DateTime.Now.AddDays(1) && 
            a.CurrentBookings < (a.MaxCapacity * 0.2)).ToList();

        return Ok(new
        {
            TotalRevenue = totalRevenue,
            AveragePrice = averagePrice,
            WarningActivities = warningActivities.Count,
            GeneratedAt = DateTime.Now
        });
    }

    [HttpPost("bulk-update")]
    public async Task<IActionResult> BulkUpdateActivityPrices([FromBody] decimal percentage)
    {
        var activities = await _activityRepository.GetAllAsync();
        
        foreach (var activity in activities)
        {
            activity.Price *= (1 + percentage / 100);
            await _activityRepository.UpdateAsync(activity);
            
            if (activity.Price > 500)
            {
                activity.IsActive = false;
            }
            
            await _activityRepository.UpdateAsync(activity);
        }

        return Ok("Prices updated");
    }
}