using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Entities;
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

        public IEnumerable<HouseCategoryServiceModel> AllCategories()
        {
            return this.data.Categories.Select(c => new HouseCategoryServiceModel
            {
                Id = c.Id,
                Name = c.Name,
            });
        }

        public bool CategoryExists(int categoryId)
        {
            return this.data.Categories.Any(c => c.Id == categoryId);
        }

        public int Create(string title, string address, string description, string imageUrl, decimal price, int categoryId, int agentId)
        {
            var house = new House
            {
                Title = title,
                Address = address,
                Description = description,
                ImageUrl = imageUrl,
                PricePerMonth = price,
                CategoryId = categoryId,
                AgentId = agentId,
            };

            this.data.Add(house);
            this.data.SaveChanges();

            return house.Id;    
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
