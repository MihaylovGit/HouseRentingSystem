using HouseRentingSystem.Data;
using HouseRentingSystem.Services.Contracts;
using HouseRentingSystem.Services.Models;

namespace HouseRentingSystem.Services
{
    public class HouseService : IHouseService
    {
        private readonly ApplicationDbContext data;

        public HouseService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<HouseIndexServiceModel> LastThreeHouses()
        {
          var lastThreeHousesAdded = this.data.Houses.OrderByDescending(h => h.Id)
                            .Select(h => new HouseIndexServiceModel
                            {
                                Id = h.Id,
                                Title = h.Title,
                                ImageUrl = h.ImageUrl,
                            })
                            .Take(3);

            return lastThreeHousesAdded;
        }
    }
}
