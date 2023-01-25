using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstants.House;

namespace HouseRentingSystem.Data.Entities
{
    public class House
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(HouseTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(HouseAddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(HouseDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal), HousePricePerMonthMinValue, HousePricePerMonthMaxValue)]
        public decimal PricePerMonth { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int AgentId { get; set; }

        public Agent Agent { get; set; }

        public string? RenterId { get; set; }
    }
}
