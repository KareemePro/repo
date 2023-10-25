using FoodDelivery.Data;
using FoodDelivery.Models.DominModels.Address;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Services.Addresses;

public class AddressSerivce : IAddressSerivce
{

    private readonly DBContext _context;

    public AddressSerivce(DBContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
    }

    public async Task<IEnumerable<Governorate>> GetGovernorates()
    {
        return await _context.Governorates.ToArrayAsync();
    }

    public async Task<IEnumerable<District>> GetDistricts(Guid GovernorateID)
    {
        if(await _context.Districts.AnyAsync(x => x.GovernorateID == GovernorateID))
        {
            return await _context.Districts.Where(x => x.GovernorateID == GovernorateID).ToArrayAsync();
        }
        return new List<District>();
    }

    public async Task<IEnumerable<Street>> GetStreets(Guid DistrictID)
    {
        if (await _context.Streets.AnyAsync(x => x.DistrictID == DistrictID))
        {
            return await _context.Streets.Where(x => x.DistrictID == DistrictID).ToArrayAsync();
        }
        return new List<Street>();
    }

    public async Task<IEnumerable<Building>> GetBuildings(Guid StreetID)
    {
        if (await _context.Buildings.AnyAsync(x => x.StreetID == StreetID))
        {
            return await _context.Buildings.Where(x => x.StreetID == StreetID).ToArrayAsync();
        }
        return new List<Building>();
    }

}
