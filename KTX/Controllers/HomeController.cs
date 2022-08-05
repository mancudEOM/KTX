using System.Diagnostics;
using KTX.Models;
using Microsoft.AspNetCore.Mvc;


namespace KTX.Controllers
{
    public class HomeController : Controller
    {
        private readonly KtxDbContext _logger;

        public HomeController(KtxDbContext logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var listofData = _logger.Posts.ToList();
            return View(listofData);
        }
        public IActionResult Index2()
        {
            return View();
        } 
       
        



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}