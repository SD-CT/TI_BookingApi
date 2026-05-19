using TI_BookingApi.Application.DTOs;

namespace TI_BookingApi.Application.Interfaces;

public interface IActivityService
{
    Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync();
    Task<ActivityDto?> GetActivityByIdAsync(Guid id);
    Task<ActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto);
    Task<ActivityDto> UpdateActivityAsync(Guid id, CreateActivityDto updateActivityDto);
    Task DeleteActivityAsync(Guid id);
}