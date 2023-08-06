using CXPApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CXPApp.Controllers
{
    public class AccountingSeatController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AccountingSeatController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(dbContext.AccountingSeat.OrderByDescending(x => x.Id).ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        [Route("AccountingSeat/EditView/{id:int}")]
        public IActionResult EditView(int id)
        {
            return View("Edit", dbContext.AccountingSeat.Find(id));
        }

        [HttpPost]
        public IActionResult Add(AccountingSeat accountingSeat)
        {

            //if (!ModelState.IsValid)
            //{
            //    TempData["AccountingSeat"] = "Formulario invalido, por favor revisar los campos!";
            //    return View();
            //}

            int ultimovalor = dbContext.AccountingSeat.OrderByDescending(x => x.Id)
                .Select(x => x.SeatId).FirstOrDefault();

            accountingSeat.SeatId = ultimovalor + 1;

            

            dbContext.Add(accountingSeat);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("AccountingSeat/Edit")]
        public IActionResult Edit(AccountingSeat accountingSeat)
        {
            
            dbContext.Update(accountingSeat);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("AccountingSeat/Delete")]
        public IActionResult Delete(int id)
        {
            dbContext.Remove(dbContext.AccountingSeat.Find(id));
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
