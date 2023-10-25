using FoodDelivery.Models.DominModels.Address;

namespace FoodDelivery.Services.Addresses;

public interface IAddressSerivce
{
    Task<IEnumerable<Governorate>> GetGovernorates();

    Task<IEnumerable<District>> GetDistricts(Guid GovernorateID);

    Task<IEnumerable<Street>> GetStreets(Guid DistrictID);

    Task<IEnumerable<Building>> GetBuildings(Guid StreetID);
}
