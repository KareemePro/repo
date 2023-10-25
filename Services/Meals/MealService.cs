using FoodDelivery.Data;
using FoodDelivery.Models.DominModels;
using FoodDelivery.Models.DTO.MealDTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace FoodDelivery.Services.Meals;

public class MealService : IMealService
{
    private readonly DBContext _context;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public MealService(DBContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;

        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IEnumerable<Meal>> GetMeals()
    {
        return await _context.Meals.Take(5).Where(x => x.MealTag.Tag.Name == "Hello").ToArrayAsync();
    }

    public async Task<Meal> AddMeal(CreateMealRequest request)
    {
        if(await _context.Chiefs.AnyAsync(x => x.Id == request.ChiefID))
        {
            Meal newMeal = new()
            {
                ID = Guid.NewGuid(),
                ChiefID = request.ChiefID,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CreatedDate = DateTime.Now,
                IsAvailable = request.IsAvailable
            };
            newMeal.MealImages = SaveImages(request.Images, newMeal.ID);
            await _context.AddAsync(newMeal);
            await _context.SaveChangesAsync();
            return newMeal;

        }
        return new Meal();
    }

    public ICollection<MealImage> SaveImages(ICollection<IFormFile> request,Guid MealID)
    {
        var MealImages = new List<MealImage>();
        foreach (var MealImage in request)
        {
            var ImageId = Guid.NewGuid();
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var uploads = Path.Combine(wwwRootPath, "Images", "News");
            var extension = Path.GetExtension(MealImage.FileName);
            var filename = $"{ImageId}{extension}";

            var filePath = Path.Combine(uploads, filename);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                MealImage.CopyTo(fileStream);
            }
            MealImages.Add(new MealImage { ID = ImageId, Image = @"\Images\News\" + filename, MealID = MealID });
        }
        return MealImages;

    }
}
