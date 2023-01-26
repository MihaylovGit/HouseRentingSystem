using HouseRentingSystem.Services.Models;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstants.House;

namespace HouseRentingSystem.Models
{
    public class HouseFormModel
    {
        [Required]
        [StringLength(HouseTitleMaxLength, MinimumLength = HouseTitleMinLength)]
        public string Title { get; init; } = null!;

        [Required]
        [StringLength(HouseAddressMaxLength, MinimumLength = HouseAddressMinLength)]
        public string Address { get; init; } = null!;

        [Required]
        [StringLength(HouseDescriptionMaxLength, MinimumLength = HouseDescriptionMinLength)]
        public string Description { get; init; } = null!;

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; } = null!;

        [Range(typeof(decimal), HousePricePerMonthMinValue, HousePricePerMonthMaxValue, 
            ErrorMessage = "Price Per Month must be a positive number and less than {2} leva.")]
        [Display(Name = "Price Per Month")]
        public decimal PricePerMonth { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<HouseCategoryServiceModel> Categories { get; set; }
            = new List<HouseCategoryServiceModel>();
    }
}
