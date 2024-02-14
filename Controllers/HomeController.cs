using InvWebApp.Data;
using InvWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InvWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            CategorieCount();
            MaterielCount();
            ServCount();
            lastFiveCategorie();
            UserCount();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Admin");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Show categorie Count
        private void CategorieCount()
        {
            int catCount = _context.Categories.Count();
            ViewBag.getCatCount = catCount;
        }

        //Show Materiel Count
        private void MaterielCount()
        {
            int matCount = _context.Materiels.Count();
            ViewBag.getMatCount = matCount;
        }

        //Show Service Count
        private void ServCount()
        {
            int servCount = _context.Services.Count();
            ViewBag.getServCount = servCount;
        }

        //Show Users Count
        private void UserCount()
        {
            int userCount = _context.Users.Count();
            ViewBag.getUserCount = userCount;
        }

        // show last 5 categories
        private IActionResult lastFiveCategorie()
        {
            var viewModel = new DashboardData
            {
                CategorieList = _context.Categories.OrderByDescending(x => x.Id).Take(5).ToList(),
                ServiceList = _context.Services.OrderByDescending(x => x.Id).Take(5).ToList(),
                MaterielList = _context.Materiels.Include(m => m.Categorie).Include(m => m.Service).OrderByDescending(x => x.Id).Take(5).ToList(),
                LogList = _context.Logs.OrderByDescending(x => x.Id).Take(5).ToList(),

            };

            return View(viewModel);
        }

    }
}
