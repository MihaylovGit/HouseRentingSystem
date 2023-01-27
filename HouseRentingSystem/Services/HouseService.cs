using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Entities;
using HouseRentingSystem.Models;
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

        public HouseQueryServiceModel All(string category = null, string searchTerm = null, HouseSorting sorting = HouseSorting.Newest, int currentPage = 1, int housesPerPage = 1)
        {
            var housesQuery = this.data.Houses.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                housesQuery = housesQuery.Where(h => h.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                housesQuery = housesQuery.Where(h => h.Title.ToLower().Contains(searchTerm.ToLower()) ||
                    h.Address.ToLower().Contains(searchTerm.ToLower()) || h.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            //housesQuery = sorting switch
            //{
            //    HouseSorting.Price => housesQuery.OrderBy(h => h.PricePerMonth),
            //    HouseSorting.NotRentedFirst => housesQuery.OrderBy(h => h.RenterId != null)
            //                                              .ThenByDescending(h => h.Id),
            //    => housesQuery.OrderByDescending(h => h.Id)
            //};

            var houses = housesQuery.Skip((currentPage - 1) * housesPerPage)
                                    .Take(housesPerPage)
                                    .Select(h => new HouseServiceModel
                                    {
                                        Id = h.Id,
                                        Title = h.Title,
                                        Address = h.Address,
                                        ImageUrl = h.ImageUrl,
                                        IsRented = h.RenterId != null,
                                        PricePerMonth = h.PricePerMonth,
                                    }).ToList();

            var totalHousesCount = housesQuery.Count();

            return new HouseQueryServiceModel()
            {
                TotalHousesCount = totalHousesCount,
                Houses = houses,
            };
        }

        public IEnumerable<HouseCategoryServiceModel> AllCategories()
        {
            return this.data.Categories.Select(c => new HouseCategoryServiceModel
            {
                Id = c.Id,
                Name = c.Name,
            });
        }

        public IEnumerable<string> AllCategoriesNames()
        {
            return this.data.Categories.Select(c => c.Name)
                                       .Distinct()
                                       .ToList();
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

        public int TotalHouseCount()
        {
            throw new NotImplementedException();
        }
    }
}
