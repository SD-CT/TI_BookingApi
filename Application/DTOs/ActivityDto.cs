using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace TI_BookingApi.Application.DTOs;

public class CreateActivityDto
{
    [SwaggerSchema("Nom de l'activité")]
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [SwaggerSchema("Description détaillée de l'activité")]
    public string Description { get; set; } = string.Empty;
    
    [SwaggerSchema("Prix par personne en euros")]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
    
    [SwaggerSchema("Capacité maximale de participants")]
    [Range(1, int.MaxValue)]
    public int MaxCapacity { get; set; }
    
    [SwaggerSchema("Date et heure de début")]
    public DateTime StartDate { get; set; }
    
    [SwaggerSchema("Date et heure de fin")]
    public DateTime EndDate { get; set; }
}

public class ActivityDto
{
    [SwaggerSchema("Identifiant unique de l'activité")]
    public Guid Id { get; set; }
    
    [SwaggerSchema("Nom de l'activité")]
    public string Name { get; set; } = string.Empty;
    
    [SwaggerSchema("Description de l'activité")]
    public string Description { get; set; } = string.Empty;
    
    [SwaggerSchema("Prix par personne")]
    public decimal Price { get; set; }
    
    [SwaggerSchema("Capacité maximale")]
    public int MaxCapacity { get; set; }
    
    [SwaggerSchema("Date de début")]
    public DateTime StartDate { get; set; }
    
    [SwaggerSchema("Date de fin")]
    public DateTime EndDate { get; set; }
    
    [SwaggerSchema("Nombre de réservations actuelles")]
    public int CurrentBookings { get; set; }
    
    [SwaggerSchema("Activité active ou non")]
    public bool IsActive { get; set; }
}