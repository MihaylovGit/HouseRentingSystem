using Microsoft.AspNetCore.Identity;

namespace HouseRentingSystem.Data.Entities
{
    public class Agent
    {
        public int Id { get; init; }

        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}
