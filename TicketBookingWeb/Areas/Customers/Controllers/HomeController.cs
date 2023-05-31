using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TB.DataAccess.Models;
using TB.DataAccess.Repository.IRepository;


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

		//[HttpGet("status=true")]
		public async Task<ActionResult<APIResponse>> Index()
		{
			// Retrieve all items where status is true
			IEnumerable<Event> items = await _unitOfWork.Event.GetAllAsync();
			IEnumerable<Event> filteredItems = items.Where(x => x.status).ToList();


			return View(filteredItems);
		}
		public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}