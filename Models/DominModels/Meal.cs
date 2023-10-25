using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.DominModels;

public class Meal
{
    public Guid ID { get; set; }

    public string ChiefID { get; set; } = string.Empty;
    [MinLength(3, ErrorMessage = "A name must be at least 3 letters")]
    [MaxLength(20, ErrorMessage = "A name must be not more than 20 letters")]
    public string Name { get; set; } = string.Empty;
    [MinLength(10, ErrorMessage = "A description must be at least 10 letters")]
    [MaxLength(200, ErrorMessage = "A description must be not more than 200 letters")]
    public string Description { get; set; } = string.Empty;

    public int Price { get; set; }

    [Display(Name = "Is Available")]
    public bool IsAvailable { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    public virtual Chief Chief { get; set; } = default!;

    public virtual MealTag MealTag { get; set; } = default!;

    public ICollection<MealImage> MealImages { get; set; } = new List<MealImage>();

    //public ICollection<MealTag> MealTags { get; set; }

    //public ICollection<Tag> Tags { get; set; }
}
