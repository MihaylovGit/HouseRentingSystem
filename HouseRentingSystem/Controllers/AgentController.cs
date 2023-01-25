using HouseRentingSystem.Models;
using HouseRentingSystem.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class AgentController : Controller
    {
        private readonly IAgentService agentService;
        private readonly UserManager<IdentityUser> userManager;

        public AgentController(IAgentService agentService, UserManager<IdentityUser> userManager)
        {
            this.agentService = agentService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Become()
        {
            var userId = this.userManager.GetUserId(this.User);
            if (this.agentService.ExistsById(userId))
            {
                return BadRequest();
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeAgentFormModel agent)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.userManager.GetUserId(this.User);

            if (this.agentService.ExistsById(userId))
            {
                return BadRequest();
            }

            if (this.agentService.UserWithPhoneNumberExists(agent.PhoneNumber))
            {
                ModelState.AddModelError(nameof(agent.PhoneNumber),
                    "Phone number already exists. Please, enter another one.");
            }

            if (this.agentService.UserHasRents(userId))
            {
                ModelState.AddModelError("Error", "You should have no rents to become an agent!");
            }

            this.agentService.Create(userId, agent.PhoneNumber);

            return RedirectToAction(nameof(HousesController.All), "Houses");
        }
    }
}
