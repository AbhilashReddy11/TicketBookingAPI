using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using TB.DataAccess.Models;
using TB.DataAccess.Models.DTO;
using TB.DataAccess.Models.VM;
using TB.DataAccess.Repository.IRepository;
using TicketBooking_Utility;
using TicketBookingWeb.Services.IServices;

namespace TicketBookingWeb.Areas.Customers.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
		
		protected APIResponse _response;
        private readonly IEventService _eventService;
        private readonly IBookingService _bookingService;



        public HomeController(ILogger<HomeController> logger, IBookingService bookingService,IEventService eventService)
        {
            _logger = logger;
          
			
			_response = new();
            _bookingService = bookingService;

            _eventService =eventService;	

        }

		
		public async Task<ActionResult<APIResponse>> Index()
		{
           
            List<Event> list = new();
		var data= await _eventService.GetAllTrueAsync<APIResponse>();
			list = JsonConvert.DeserializeObject<List<Event>>(Convert.ToString(data.Result));
            return View(list);
        }
		public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]





        [Authorize]


        public async Task<IActionResult> CreateVBooking()
        {
            BookingCreateVM bookingCreateVM = new();
            var response = await _eventService.GetAllTrueAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                bookingCreateVM.EventList = JsonConvert.DeserializeObject<List<Event>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.EventName,
                        Value = i.EventId.ToString()
                    });
            }

            return View(bookingCreateVM);
        }
     
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateVBooking(BookingCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _bookingService.CreateAsync<APIResponse>(model.booking);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = " Ticked booked";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _eventService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.EventList = JsonConvert.DeserializeObject<List<Event>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.EventName,
                        Value = i.EventId.ToString()
                    });
            }
            

            return View(model);
        }
        public async Task<IActionResult> Details(int EventId)
        {
            var response = await _eventService.GetAsync<APIResponse>(EventId);
            if (response != null && response.IsSuccess)
            {

                Event model = JsonConvert.DeserializeObject<Event>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
		

	}
}