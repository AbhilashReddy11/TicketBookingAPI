using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TB.DataAccess.Models;
using TB.DataAccess.Repository.IRepository;

namespace TicketBookingWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;



        public AdminController(IUnitOfWork unitOfWork, IMapper mapper)
        {


            _unitOfWork = unitOfWork;


        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet("status=false")]
        public async Task<ActionResult<APIResponse>> Getfalse()
        {
            // Retrieve all items where status is true
            IEnumerable<Event> items = await _unitOfWork.Event.GetAllAsync();
            IEnumerable<Event> filteredItems = items.Where(x => !x.status).ToList();


            return View(filteredItems);
        }
        
        public async Task<ActionResult<APIResponse>> Approve(int EventId)
        {

            // Find the data from the database using the provided ID
            var data = await _unitOfWork.Event.GetAsync();

            if (data != null)
            {

                data.status = true;

            }
            await _unitOfWork.Event.SaveAsync();

			return RedirectToAction(nameof(Getfalse));


		}
		

		public async Task<ActionResult<APIResponse>> Reject(int EventId)
		{
		
			

				var events = await _unitOfWork.Event.GetAsync();
				if (events == null)
				{
					return NotFound();
				}


				await _unitOfWork.Event.RemoveAsync(events);
			return RedirectToAction(nameof(Getfalse));



		}
	}
}
