using HouseRentingSystem.Services.Models;

namespace HouseRentingSystem.Services.Contracts
{
    public interface IHouseService
    {
        IEnumerable<HouseIndexServiceModel> LastThreeHouses();
    }
}
