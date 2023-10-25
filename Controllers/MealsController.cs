using FoodDelivery.Models.DTO.MealDTO;
using FoodDelivery.Services.Meals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers;

[Route("[controller]")]
[ApiController]
public class MealsController : ControllerBase
{
    private readonly IMealService _meal;

    public MealsController(IMealService meal)
    {
        _meal = meal;
    }

    [HttpGet]
    public async Task<IActionResult> Meal()
    {
        return Ok(await _meal.GetMeals());
    }
    [HttpPost]
    public async Task<IActionResult> AddMeal(CreateMealRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var meal = await _meal.AddMeal(request);
        return Ok(meal);
    }
}
