using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TB.DataAccess.Models;
using TB.DataAccess.Repository.IRepository;
using TicketBookingWeb.Models;

namespace TicketBookingWeb.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public  IActionResult Index()
        {
			List<Event> EventList = _unitOfWork.Event.GetAllAsync().Result;
			return View(EventList);
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