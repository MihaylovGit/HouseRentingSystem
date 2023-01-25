using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Services.Models
{
    public class HouseIndexServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
    }
}
