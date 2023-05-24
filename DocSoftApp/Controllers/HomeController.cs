using DocSoftApp.Models;
using DocSoftSupport.Test;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace DocSoftApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


     
        public async Task<IActionResult> GetSorts(TestModels models)
        {
            try
            {
                TestAccess acp = new TestAccess();
                TestModels model = new()
                {
                    Name = models.Name,
                    Age = models.Age,
                    DOB = models.DOB

                };

                await acp.AddTest(model);
                Console.WriteLine("Sucss");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}