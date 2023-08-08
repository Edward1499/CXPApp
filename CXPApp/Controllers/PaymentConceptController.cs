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

        [HttpGet]
        [Route("PaymentConcept/Add")]
        public IActionResult Add()
        {
            return View();
        }

        //Yo del futuro, para los proximos cruds, COPIAR ESTA FUNCION (LINEA 24 A LA 35 y adaptarla a lo que se necesite
        [HttpGet]
        [Route("PaymentConcept/{parametro}")]
        public IActionResult Search(string parametro)
        {

            if(parametro == "Search")
            {
                return RedirectToAction("Index");
            }

            else
            {
                return View("Index", dbContext.PaymentConcepts.Where(x => (x.Id.ToString() == parametro
                    || x.Description.ToLower().Contains(parametro.ToLower())
                    || (x.IsActive && (parametro.ToLower() == "activo"))
                    || (!x.IsActive && (parametro.ToLower() == "inactivo")))).OrderByDescending(x => x.Id).ToList());
            }
            
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
