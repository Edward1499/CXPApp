using CXPApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net.Http;

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
        public async Task<IActionResult> AddAsync(AccountingSeat accountingSeat)
        {
            
            using (var client = new HttpClient())
            {
                var url = new Uri("http://129.80.203.120:5000/post-accounting-entries");
                var requestData = new AccountingSeatWebService
                {
                    descripcion = accountingSeat.Description,
                    monto = accountingSeat.SeatAmount,
                    auxiliar = 6,
                    cuentaCR = 81,
                    cuentaDB = 81
                };
                var response = await client.PostAsJsonAsync(url, requestData);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Error al guardar asiento contable");

                var data = await response.Content.ReadFromJsonAsync<AccountingSeat>();
                
            }

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
