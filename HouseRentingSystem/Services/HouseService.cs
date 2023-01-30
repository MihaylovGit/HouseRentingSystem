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

        public IEnumerable<HouseServiceModel> AllHousesByAgentId(int agentId)
        {
            var houses = this.data.Houses.Where(h => h.AgentId == agentId).ToList();

            return ProjectToModel(houses);
        }

        private List<HouseServiceModel> ProjectToModel(List<House> houses)
        {
            var resultHouses = houses.Select(x => new HouseServiceModel
            {
                Id = x.Id,
                Title = x.Title,
                Address = x.Address,
                ImageUrl = x.ImageUrl,
                PricePerMonth = x.PricePerMonth,
                IsRented = x.RenterId != null,
            }).ToList();

            return resultHouses;
        }

        public IEnumerable<HouseServiceModel> AllHousesByUserId(string userId)
        {
            var houses = this.data
                             .Houses
                             .Where(h => h.RenterId == userId)
                             .ToList();

            return ProjectToModel(houses);
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
            return this.data.Houses.Count();
        }

        public bool Exists(int id)
        {
            return this.data.Houses.Any(h => h.Id == id);
        }

        public HouseDetailsServiceModel HouseDetailsById(int id)
        {
            return this.data.Houses.Where(h => h.Id == id)
                                   .Select(h => new HouseDetailsServiceModel()
                                   {
                                       Id = h.Id,
                                       Title = h.Title,
                                       Address = h.Address,
                                       Description = h.Description,
                                       ImageUrl = h.ImageUrl,
                                       PricePerMonth = h.PricePerMonth,
                                       IsRented = h.RenterId != null,
                                       Category = h.Category.Name,
                                       Agent = new AgentServiceModel()
                                       {
                                           PhoneNumber = h.Agent.PhoneNumber,
                                           Email = h.Agent.User.Email,
                                       }
                                   }).FirstOrDefault();
        }

        public void Edit(int houseId, string title, string address, string description, string imageUrl, decimal price, int categoryId)
        {
            var house = this.data.Houses.Find(houseId);

            house.Title = title;
            house.Address = address;
            house.Description = description;
            house.ImageUrl = imageUrl;
            house.PricePerMonth = price;
            house.CategoryId = categoryId;

            this.data.SaveChanges();    
        }

        public bool HasAgentWithId(int houseId, string currentUserId)
        {
            var house = this.data.Houses.Find(houseId);
            var agent = this.data.Agents.FirstOrDefault(a => a.Id == house.AgentId);
            if (agent == null)
            {
                return false;
            }

            if (agent.UserId != currentUserId)
            {
                return false;
            }

            return true;
        }

        public int GetHouseCategoryId(int houseId)
        {
            return this.data.Houses.Find(houseId).CategoryId;
        }

        public void Delete(int houseId)
        {
            var house = this.data.Houses.FirstOrDefault(h => h.Id == houseId);

            this.data.Houses.Remove(house);

            this.data.SaveChanges();
        }
    }
}
