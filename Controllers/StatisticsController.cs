using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TI_BookingApi.Application.Services;

namespace TI_BookingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[SwaggerTag("Statistiques et rapports sur les activités et réservations")]
public class StatisticsController : ControllerBase
{
    private readonly StatisticsService _statisticsService;

    public StatisticsController(StatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet("total-activities")]
    [SwaggerOperation(
        Summary = "Nombre total d'activités",
        Description = "Retourne le nombre total d'activités dans le système"
    )]
    [SwaggerResponse(200, "Succès", typeof(int))]
    public ActionResult<int> GetTotalActivities()
    {
        var total = _statisticsService.GetTotalActivities();
        return Ok(total);
    }

    [HttpGet("revenue")]
    public ActionResult<decimal> GetTotalRevenue()
    {
        var revenue = _statisticsService.GetTotalRevenue();
        return Ok(revenue);
    }

    [HttpGet("report")]
    public ActionResult<string> GetReport()
    {
        var report = _statisticsService.GenerateReport();
        return Ok(report);
    }
}