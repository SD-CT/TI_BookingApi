using System.ComponentModel.DataAnnotations;

namespace TI_BookingApi.Application.Validators;

public class CreateActivityDtoValidation
{
    [Required(ErrorMessage = "Le nom de l'activité est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "La description est requise")]
    [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 1000.00, ErrorMessage = "Le prix doit être entre 0.01 et 1000.00")]
    public decimal Price { get; set; }

    [Range(1, 100, ErrorMessage = "La capacité doit être entre 1 et 100")]
    public int MaxCapacity { get; set; }

    [DataType(DataType.DateTime, ErrorMessage = "Format de date invalide")]
    public DateTime StartDate { get; set; }

    [DataType(DataType.DateTime, ErrorMessage = "Format de date invalide")]
    public DateTime EndDate { get; set; }
}

public class CreateBookingDtoValidation
{
    [Required(ErrorMessage = "L'ID de l'activité est requis")]
    public Guid ActivityId { get; set; }

    [Required(ErrorMessage = "L'email est requis")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le nom du client est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string CustomerName { get; set; } = string.Empty;

    [Range(1, 20, ErrorMessage = "Le nombre de personnes doit être entre 1 et 20")]
    public int NumberOfPeople { get; set; }
}