using CXPApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CXPApp.Controllers
{
    public class ProviderController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public ProviderController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(dbContext.Providers.OrderByDescending(x => x.Id).ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        [Route("Provider/EditView/{id:int}")]
        public IActionResult EditView(int id)
        {
            return View("Edit", dbContext.Providers.Find(id));
        }

        [HttpPost]
        public IActionResult Add(Provider provider)
        {
            dbContext.Add(provider);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Provider/Edit")]
        public IActionResult Edit(Provider provider)
        {
            dbContext.Update(provider);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Provider/Delete")]
        public IActionResult Delete(int id)
        {
            dbContext.Remove(dbContext.Providers.Find(id));
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
