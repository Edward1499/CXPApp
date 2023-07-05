using CXPApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CXPApp.Controllers
{
    public class PaymentConceptController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public PaymentConceptController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(dbContext.PaymentConcepts.OrderByDescending(x => x.Id).ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        [Route("PaymentConcept/EditView/{id:int}")]
        public IActionResult EditView(int id)
        {
            return View("Edit", dbContext.PaymentConcepts.Find(id));
        }

        [HttpPost]
        public IActionResult Add(PaymentConcept paymentConcept)
        {
            if (!ModelState.IsValid)
            {
                TempData["PaymentConcept"] = "Formulario invalido, por favor revisar los campos!";
                return View();
            }

            dbContext.Add(paymentConcept);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("PaymentConcept/Edit")]
        public IActionResult Edit(PaymentConcept paymentConcept)
        {
            dbContext.Update(paymentConcept);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("PaymentConcept/Delete")]
        public IActionResult Delete(int id)
        {
            dbContext.Remove(dbContext.PaymentConcepts.Find(id));
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
