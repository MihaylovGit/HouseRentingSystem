using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstants.Category;

namespace HouseRentingSystem.Data.Entities
{
    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        public IEnumerable<House> Houses { get; init; } = new List<House>();
    }
}
