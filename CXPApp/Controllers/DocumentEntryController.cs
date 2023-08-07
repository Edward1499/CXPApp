using CXPApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CXPApp.Controllers
{
    public class DocumentEntryController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public DocumentEntryController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            
            return View(dbContext.DocumentEntry.OrderByDescending(x => x.Id).ToList());
        }

        public IActionResult Add()
        {
            FillDropdownLists();
            return View();
        }

        [HttpGet]
        [Route("DocumentEntry/EditView/{id:int}")]
        public IActionResult EditView(int id)
        {
            FillDropdownLists();
            return View("Edit", dbContext.DocumentEntry.Find(id));
        }

        [HttpPost]
        public IActionResult Add(DocumentEntry documentEntry)
        {
            //if (!ModelState.IsValid)
            //{
            //    TempData["DocumentEntry"] = "Formulario invalido, por favor revisar los campos!";
            //    return View();
            //}
            
            dbContext.Add(documentEntry);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("DocumentEntry/Edit")]
        public IActionResult Edit(DocumentEntry documentEntry)
        {
            
            dbContext.Update(documentEntry);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("DocumentEntry/Delete")]
        public IActionResult Delete(int id)
        {
            dbContext.Remove(dbContext.DocumentEntry.Find(id));
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        private void FillDropdownLists()
        {
            ViewBag.Provider = new SelectList(dbContext.Providers.OrderByDescending(x => x.Id).ToList(),"Id", "Name");
        }

        }
}
