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

        [HttpGet]
        [Route("Provider/Add")]
        public IActionResult Add()
        {
            return View();
        }

        //Yo del futuro, para los proximos cruds, COPIAR ESTA FUNCION (LINEA 24 A LA 35 y adaptarla a lo que se necesite
        [HttpGet]
        [Route("Provider/{parametro}")]
        public IActionResult Search(string parametro)
        {
            if (parametro == "Search")
            {
                return RedirectToAction("Index");
            }

            else
            {
                return View("Index", dbContext.Providers.Where(x => (x.Id.ToString() == parametro
                    || x.PersonalId == parametro
                    || x.Name.ToLower().Contains(parametro.ToLower())
                    || x.PersonType.ToLower().Contains(parametro.ToLower())
                    || x.Balance.ToString() == parametro
                    || (x.IsActive && (parametro.ToLower() == "activo"))
                    || (!x.IsActive && (parametro.ToLower() == "inactivo")))).OrderByDescending(x => x.Id).ToList());
            }
            
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
            if (validaCedula(provider.PersonalId) == true && provider.Balance > 0) {
                
                dbContext.Add(provider);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            else
            {
                TempData["err"] = "Revisar campos";
                return RedirectToAction("Add");
            }

        }

        [HttpPost]
        [Route("Provider/Edit")]
        public IActionResult Edit(Provider provider)
        {
            int id = provider.Id;
            
            if (validaCedula(provider.PersonalId) == true && provider.Balance > 0)
            {

                dbContext.Update(provider);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            else
            {
                TempData["err"] = "Revisar campos";
                return View("Edit", dbContext.Providers.Find(id));
            }
        }

        [HttpGet]
        [Route("Provider/Delete")]
        public IActionResult Delete(int id)
        {
            dbContext.Remove(dbContext.Providers.Find(id));
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public static bool validaCedula(string pCedula)

        {
            try
            {
                int vnTotal = 0;
                string vcCedula = pCedula.Replace("-", "");
                int pLongCed = vcCedula.Trim().Length;
                int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };
                if (pLongCed < 11 || pLongCed > 11)
                    return false;
                for (int vDig = 1; vDig <= pLongCed; vDig++)
                {
                    int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                    if (vCalculo < 10)
                        vnTotal += vCalculo;
                    else
                        vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
                }
                if (vnTotal % 10 == 0)
                    return true;
                else
                    return false;
            }

            catch
            {
                return false;
            }
            
        }
    }
}
