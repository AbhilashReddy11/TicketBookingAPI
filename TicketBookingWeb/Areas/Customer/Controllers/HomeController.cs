using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections;
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
        [Authorize]
        public async Task<IActionResult> CreateBooking(int eventId)
        {
           
            TempData["EventId"] = eventId;
            return View();

        }
        public async Task<IActionResult> Book(BookingCreateDTO createDTO)
        {
           
            createDTO.EventId = Convert.ToInt32(TempData["EventId"]);

            createDTO.Name = User.Identity.Name;

            var eventlst = await _eventService.GetAsync<APIResponse>(createDTO.EventId);
           

                Event evntlst = JsonConvert.DeserializeObject<Event>(Convert.ToString(eventlst.Result));


            if (createDTO.NumberOfTickets > evntlst.AvailableSeats)
            {
                TempData["Error"] = "Not enough Tickets";
                return RedirectToAction(nameof(Index));
            }
            else
            {

                await _bookingService.CreateAsync<APIResponse>(createDTO);


                TempData["success"] = "Ticket booked successfully";
                return RedirectToAction(nameof(Index));
            }
            
            
        }


        public async Task<IActionResult> MyTicket()
        {
            List<Booking> list = new();


            var response = await _bookingService.GetAllAsync<APIResponse>();

            if (response != null)
            {
                list = JsonConvert.DeserializeObject<List<Booking>>(Convert.ToString(response.Result));

            }

            List<Booking> myTickets = new List<Booking>();
           
            string UserName = User.Identity.Name;
            var list1 = list.Where(u => u.Name == UserName);
           

            return View(list1);

        }
        public async Task<ActionResult<APIResponse>> DeleteBooking(int BookingId)

        {
			

			var bookinglist = await _bookingService.GetAsync<APIResponse>(BookingId);


            Booking bookinglst = JsonConvert.DeserializeObject<Booking>(Convert.ToString(bookinglist.Result));
            TimeSpan timeDifference = (TimeSpan)(bookinglst.events.EventDate - DateTime.UtcNow);

           
            if (timeDifference.TotalDays > 2)
            {

                var response = await _bookingService.DeleteAsync<APIResponse>(BookingId);

                TempData["success"] = " deleted  successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = " You can't Cancel your ticket before a day ";
                return RedirectToAction(nameof(Index));

            }

        }


    }
}