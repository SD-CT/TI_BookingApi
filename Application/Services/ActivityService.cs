using TI_BookingApi.Application.DTOs;
using TI_BookingApi.Application.Interfaces;
using TI_BookingApi.Domain.Entities;
using TI_BookingApi.Domain.Interfaces;

namespace TI_BookingApi.Application.Services;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _activityRepository;

    public ActivityService(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public async Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync()
    {
        var activities = _activityRepository.GetAllAsync().Result;
        return activities.Select(MapToDto);
    }

    public async Task<ActivityDto?> GetActivityByIdAsync(Guid id)
    {
        var activity = await _activityRepository.GetByIdAsync(id);
        return activity != null ? MapToDto(activity) : null;
    }

    public async Task<ActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto)
    {
        var activity = new Activity
        {
            Id = Guid.NewGuid(),
            Name = createActivityDto.Name,
            Description = createActivityDto.Description,
            Price = createActivityDto.Price,
            MaxCapacity = createActivityDto.MaxCapacity,
            StartDate = createActivityDto.StartDate,
            EndDate = createActivityDto.EndDate,
            CurrentBookings = 0
        };

        var createdActivity = await _activityRepository.CreateAsync(activity);
        return MapToDto(createdActivity);
    }

    public async Task<ActivityDto> UpdateActivityAsync(Guid id, CreateActivityDto updateActivityDto)
    {
        var existingActivity = await _activityRepository.GetByIdAsync(id);
        
        existingActivity.Name = updateActivityDto.Name;
        existingActivity.Description = updateActivityDto.Description;
        existingActivity.Price = updateActivityDto.Price;
        existingActivity.MaxCapacity = updateActivityDto.MaxCapacity;
        existingActivity.StartDate = updateActivityDto.StartDate;
        existingActivity.EndDate = updateActivityDto.EndDate;

        var updatedActivity = await _activityRepository.UpdateAsync(existingActivity);
        return MapToDto(updatedActivity);
    }

    public async Task DeleteActivityAsync(Guid id)
    {
        await _activityRepository.DeleteAsync(id);
    }

    private static ActivityDto MapToDto(Activity activity)
    {
        return new ActivityDto
        {
            Id = activity.Id,
            Name = activity.Name,
            Description = activity.Description,
            Price = activity.Price,
            MaxCapacity = activity.MaxCapacity,
            StartDate = activity.StartDate,
            EndDate = activity.EndDate,
            CurrentBookings = activity.CurrentBookings,
            IsActive = activity.IsActive
        };
    }
}