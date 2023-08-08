using CXPApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CXPApp.Controllers
{
    public class WebServiceDtoController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public WebServiceDtoController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> IndexAsync()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri("http://129.80.203.120:5000/get-accounting-entries/auxiliar=6");
                var response = await client.GetAsync(url);
                var testResult = JsonConvert.DeserializeObject<WebServiceResponse>(await response.Content.ReadAsStringAsync());

                return View(testResult.AsientoContable.OrderByDescending(x=>x.Id).ToList());
            }

            
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AccountingSeat accountingSeat)
        {

            using (var client = new HttpClient())
            {
                var url = new Uri("http://129.80.203.120:5000/post-accounting-entries");
                var requestData = new WebServiceDto
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
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAsync(AccountingSeat accountingSeat)
        //{

        //    using (var client = new HttpClient())
        //    {
        //        var url = new Uri("http://129.80.203.120:5000/get-accounting-entries/auxiliar=6");
        //        var requestData = new WebServiceDto
        //        {
        //            descripcion = accountingSeat.Description,
        //            monto = accountingSeat.SeatAmount,
        //            auxiliar = 6,
        //            cuentaCR = 81,
        //            cuentaDB = 81
        //        };
        //        var response = await client.PostAsJsonAsync(url, requestData);
        //        if (!response.IsSuccessStatusCode)
        //            throw new Exception("Error al guardar asiento contable");

        //        var data = await response.Content.ReadFromJsonAsync<AccountingSeat>();
        //    }
        //    return RedirectToAction("Index");
        //}

    }
}
