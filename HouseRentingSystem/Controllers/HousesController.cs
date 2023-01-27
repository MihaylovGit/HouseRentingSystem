using HouseRentingSystem.Models;
using HouseRentingSystem.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class HousesController : Controller
    {
        private readonly IHouseService houseService;
        private readonly IAgentService agentService;
        private readonly UserManager<IdentityUser> userManager;

        public HousesController(IHouseService houseService, IAgentService agentService, UserManager<IdentityUser> userManager)
        {
            this.houseService = houseService;
            this.agentService = agentService;
            this.userManager = userManager;
        }

        public IActionResult All([FromQuery] AllHousesQueryModel query)
        {
             
            var queryResult = this.houseService.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = queryResult.TotalHousesCount;
            query.Houses = queryResult.Houses;

            var houseCategories = this.houseService.AllCategoriesNames();
            query.Categories = houseCategories;

            return this.View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            return this.View(new AllHousesQueryModel());
        }

        public IActionResult Details(int id)
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.agentService.ExistsById(this.userManager.GetUserId(this.User)))
            {
                return RedirectToAction(nameof(AgentController.Become), "Agents");
            }

            var viewModel = new HouseFormModel
            {
                Categories = this.houseService.AllCategories()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(HouseFormModel formInput)
        {
            if (!this.agentService.ExistsById(this.userManager.GetUserId(this.User)))
            {
                return RedirectToAction(nameof(AgentController.Become), "Agents");
            }

            if (!this.houseService.CategoryExists(formInput.CategoryId))
            {
                this.ModelState.AddModelError(nameof(formInput.CategoryId),
                    "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                formInput.Categories = this.houseService.AllCategories();

                return this.View(formInput);
            }

            var agentId = this.agentService.GetAgentId(userManager.GetUserId(this.User));
            var newHouseId = this.houseService.Create(formInput.Title, formInput.Address, formInput.Description,
                formInput.ImageUrl, formInput.PricePerMonth, formInput.CategoryId, agentId);

            return RedirectToAction(nameof(Details), new { id = newHouseId } );
        }

        //public IActionResult Edit(int id, HouseFormModel house)
        //{

        //}

        //[Authorize]
        //public IActionResult Delete(int id)
        //{

        //}

        //[Authorize]
        //[HttpPost]
        //public IActionResult Delete(HouseFormModel house)
        //{
        //    return RedirectToAction(nameof(All));
        //}

        //[Authorize]
        //[HttpPost]
        //public IActionResult Rent(int id)
        //{
        //    return this.RedirectToAction(nameof(Mine));
        //}

        //[Authorize]
        //[HttpPost]
        //public IActionResult Leave(int id)
        //{
        //    return this.RedirectToAction(nameof(Mine));
        //}
    }
}
