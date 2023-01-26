using HouseRentingSystem.Services.Models;

namespace HouseRentingSystem.Services.Contracts
{
    public interface IHouseService
    {
        IEnumerable<HouseIndexServiceModel> LastThreeHouses();

        IEnumerable<HouseCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);

        int Create(string title, string address, string description, string imageUrl, decimal price, int categoryId,
            int agentId);
    }
}
