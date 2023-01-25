using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class HousesController : Controller
    {
        public IActionResult All()
        {
            return View(new AllHousesQueryModel());
        }

        //[Authorize]
        //public IActionResult Mine()
        //{
        //    return this.View(new AllHousesQueryModel());
        //}

        //public IActionResult Details (int id)
        //{
        //    return View();
        //}

        //[Authorize]
        //public IActionResult Add(HouseFormModel house)
        //{
        //    => this.View();
        //}

        //[Authorize]
        //[HttpPost]
        //public IActionResult Add(HouseFormModel house)
        //{
        //    => this.View();
        //}

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
