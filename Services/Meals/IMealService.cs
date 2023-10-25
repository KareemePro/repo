using FoodDelivery.Models.DominModels;
using FoodDelivery.Models.DTO.MealDTO;

namespace FoodDelivery.Services.Meals;

public interface IMealService
{
    Task<IEnumerable<Meal>> GetMeals();

    Task<Meal> AddMeal(CreateMealRequest request);
}
