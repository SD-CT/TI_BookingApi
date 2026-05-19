using TI_BookingApi.Domain.Entities;

namespace TI_BookingApi.Domain.Interfaces;

public interface IActivityRepository
{
    Task<IEnumerable<Activity>> GetAllAsync();
    Task<Activity?> GetByIdAsync(Guid id);
    Task<Activity> CreateAsync(Activity activity);
    Task<Activity> UpdateAsync(Activity activity);
    Task DeleteAsync(Guid id);
}