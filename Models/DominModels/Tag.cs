using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.DominModels;

public class Tag
{
    public Guid ID { get; set; }

    [MinLength(3, ErrorMessage = "A name must be at least 3 letters")]
    [MaxLength(20, ErrorMessage = "A name must be not more than 20 letters")]
    public string Name { get; set; } = string.Empty;

    public virtual MealTag MealTag { get; set; } = default!;

    //public ICollection<MealTag> MealTags { get; set; }
}
