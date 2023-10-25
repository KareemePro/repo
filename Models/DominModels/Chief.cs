using FoodDelivery.Models.DominModels.Address;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.DominModels;

public class Chief : User
{
    public Guid BuildingID { get; set; }
    [Required(ErrorMessage = "Please enter your middle name")]
    [MinLength(2, ErrorMessage = "A name must be at least 2 letters")]
    [MaxLength(15, ErrorMessage = "A name must be not more than 15 letters")]
    [Display(Name = "Middle Name")]
    public string MiddleName { get; set; } = string.Empty;

    public string GovernmentID { get; set; } = string.Empty;
    [Display(Name = "Is Available")]
    public bool IsAvailable { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Building Building { get; set; } = default!;

    public ICollection<Meal> Meals { get; set; } = default!;
}
