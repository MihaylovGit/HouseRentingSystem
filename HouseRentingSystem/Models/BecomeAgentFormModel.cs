using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstants.Agent;

namespace HouseRentingSystem.Models
{
    public class BecomeAgentFormModel
    {
        [Required]
        [StringLength(AgentPhoneNumberMaxLength, MinimumLength = AgentPhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
