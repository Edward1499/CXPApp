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
        public async Task<IActionResult> AddAsync(DocumentEntry documentEntry)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri("http://129.80.203.120:5000/post-accounting-entries");
                var requestData = new WebServiceDto
                {
                    descripcion = documentEntry.InvoiceNumber,
                    monto = documentEntry.amount,
                    auxiliar = 6,
                    cuentaCR = 81,
                    cuentaDB = 81
                };
                var response = await client.PostAsJsonAsync(url, requestData);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Error al guardar asiento contable");

                var data = await response.Content.ReadFromJsonAsync<DocumentEntry>();

            }

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
            ViewBag.Provider = new SelectList(dbContext.Providers.OrderByDescending(x => x.Id).ToList(), "Id", "Name");
            ViewBag.PaymentConcept = new SelectList(dbContext.PaymentConcepts.OrderByDescending(x => x.Id).ToList(), "Description", "Description");
        }

    }
}