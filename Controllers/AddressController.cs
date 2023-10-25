using FoodDelivery.Models.DominModels.Address;
using FoodDelivery.Services.Addresses;
using FoodDelivery.Services.Meals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers;

[Route("[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IAddressSerivce _address;

    public AddressController(IAddressSerivce address)
    {
        _address = address;
    }

    [HttpGet("Governorate")]
    public async Task<IEnumerable<Governorate>> Governorate()
    {
        return await _address.GetGovernorates();
    }

    [HttpGet("District")]
    public async Task<IEnumerable<District>> District(Guid ID)
    {
        return await _address.GetDistricts(ID);
    }

    [HttpGet("Street")]
    public async Task<IEnumerable<Street>> Street(Guid ID)
    {
        return await _address.GetStreets(ID);
    }

    [HttpGet("Building")]
    public async Task<IEnumerable<Building>> Building(Guid ID)
    {
        return await _address.GetBuildings(ID);
    }
}