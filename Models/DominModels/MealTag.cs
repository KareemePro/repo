using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.DominModels;

public class MealTag
{
    public Guid MealID { get; set; }

    public Guid TagID { get; set; }

    [MinLength(3, ErrorMessage = "A name must be at least 3 letters")]
    [MaxLength(20, ErrorMessage = "A name must be not more than 20 letters")]
    public string Name { get; set; } = string.Empty;

    public virtual Meal Meal { get; set; } = default!;

    public virtual Tag Tag { get; set; } = default!;
}
