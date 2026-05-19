using TI_BookingApi.Domain.Entities;
using TI_BookingApi.Domain.Interfaces;

namespace TI_BookingApi.Infrastructure.Repositories;

public class InMemoryActivityRepository : IActivityRepository
{
    private readonly List<Activity> _activities = new();

    public InMemoryActivityRepository()
    {
        // Données de test
        _activities.AddRange(new[]
        {
            new Activity
            {
                Id = Guid.NewGuid(),
                Name = "Randonnée en montagne",
                Description = "Une belle randonnée dans les Alpes",
                Price = 25.0m,
                MaxCapacity = 15,
                StartDate = DateTime.Now.AddDays(7),
                EndDate = DateTime.Now.AddDays(7).AddHours(6),
                CurrentBookings = 3
            },
            new Activity
            {
                Id = Guid.NewGuid(),
                Name = "Cours de cuisine",
                Description = "Apprenez à cuisiner comme un chef",
                Price = 75.0m,
                MaxCapacity = 8,
                StartDate = DateTime.Now.AddDays(14),
                EndDate = DateTime.Now.AddDays(14).AddHours(3),
                CurrentBookings = 1
            }
        });
    }

    public async Task<IEnumerable<Activity>> GetAllAsync()
    {
        await Task.Delay(10);
        return _activities.ToList();
    }

    public async Task<Activity?> GetByIdAsync(Guid id)
    {
        await Task.Delay(10);
        return _activities.FirstOrDefault(a => a.Id == id);
    }

    public async Task<Activity> CreateAsync(Activity activity)
    {
        await Task.Delay(10);
        _activities.Add(activity);
        return activity;
    }

    public async Task<Activity> UpdateAsync(Activity activity)
    {
        await Task.Delay(10);
        var existingIndex = _activities.FindIndex(a => a.Id == activity.Id);
        if (existingIndex >= 0)
        {
            _activities[existingIndex] = activity;
        }
        return activity;
    }

    public async Task DeleteAsync(Guid id)
    {
        await Task.Delay(10);
        var activity = _activities.FirstOrDefault(a => a.Id == id);
        if (activity != null)
        {
            _activities.Remove(activity);
        }
    }
}