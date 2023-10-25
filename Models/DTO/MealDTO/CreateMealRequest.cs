namespace FoodDelivery.Models.DTO.MealDTO;

public class CreateMealRequest
{
    public string ChiefID { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Price { get; set; }

    public bool IsAvailable { get; set; }

    public List<IFormFile> Images { get; set; }
}
