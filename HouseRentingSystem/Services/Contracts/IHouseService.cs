using HouseRentingSystem.Models;
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

        HouseQueryServiceModel All(string category = null, string searchTerm = null, HouseSorting sorting = HouseSorting.Newest, int currentPage = 1, int housesPerPage = 1);

        int TotalHouseCount();

        IEnumerable<string> AllCategoriesNames();

        IEnumerable<HouseServiceModel> AllHousesByAgentId(int agentId);

        IEnumerable<HouseServiceModel> AllHousesByUserId(string userId);
    }
}
