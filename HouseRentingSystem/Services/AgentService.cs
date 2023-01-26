using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Entities;
using HouseRentingSystem.Services.Contracts;

namespace HouseRentingSystem.Services
{
    public class AgentService : IAgentService
    {
        private readonly ApplicationDbContext data;

        public AgentService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void Create(string userId, string phoneNumber)
        {
            var agent = new Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber,
            };

            this.data.Agents.Add(agent);

            this.data.SaveChanges();
        }

        public bool ExistsById(string userId)
        {
           return this.data.Agents.Any(a => a.UserId == userId);
        }

        public int GetAgentId(string userId)
        {
            int agentId = this.data.Agents.FirstOrDefault(a => a.UserId == userId).Id;
           
            return agentId;
        }

        public bool UserHasRents(string userId)
        {
            return this.data.Houses.Any(x => x.RenterId == userId);
        }

        public bool UserWithPhoneNumberExists(string phoneNumber)
        {
            return this.data.Agents.Any(a => a.PhoneNumber == phoneNumber);
        }
    }
}
