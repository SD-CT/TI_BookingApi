using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TI_BookingApi.Application.DTOs;
using TI_BookingApi.Application.Interfaces;

namespace TI_BookingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[SwaggerTag("Gestion des activités disponibles à la réservation")]
public class ActivitiesController : ControllerBase
{
    private readonly IActivityService _activityService;

    public ActivitiesController(IActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Récupère toutes les activités",
        Description = "Retourne la liste complète des activités disponibles"
    )]
    [SwaggerResponse(200, "Succès", typeof(IEnumerable<ActivityDto>))]
    public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivities()
    {
        var activities = await _activityService.GetAllActivitiesAsync();
        return Ok(activities);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Récupère une activité par son ID",
        Description = "Retourne les détails d'une activité spécifique"
    )]
    [SwaggerResponse(200, "Activité trouvée", typeof(ActivityDto))]
    [SwaggerResponse(404, "Activité non trouvée")]
    public async Task<ActionResult<ActivityDto>> GetActivity(Guid id)
    {
        var activity = await _activityService.GetActivityByIdAsync(id);
        return Ok(activity);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Crée une nouvelle activité",
        Description = "Ajoute une nouvelle activité au système"
    )]
    [SwaggerResponse(201, "Activité créée avec succès", typeof(ActivityDto))]
    [SwaggerResponse(400, "Données invalides")]
    public async Task<ActionResult<ActivityDto>> CreateActivity(CreateActivityDto createActivityDto)
    {
        var activity = await _activityService.CreateActivityAsync(createActivityDto);
        return CreatedAtAction(nameof(GetActivity), new { id = activity.Id }, activity);
    }

    [HttpPost("{id}/update")]
    public async Task<ActionResult<ActivityDto>> UpdateActivity(Guid id, CreateActivityDto updateActivityDto)
    {
        var activity = await _activityService.UpdateActivityAsync(id, updateActivityDto);
        return Ok(activity);
    }

    [HttpPost("{id}/delete")]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
        await _activityService.DeleteActivityAsync(id);
        return NoContent();
    }
}