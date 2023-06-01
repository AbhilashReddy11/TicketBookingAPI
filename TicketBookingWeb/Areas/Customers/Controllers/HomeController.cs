using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
    [Area("Customers")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
		private readonly IMapper _mapper;
		protected APIResponse _response;
        private readonly IEventService _eventService;
        private readonly IBookingService _bookingService;



        public HomeController(ILogger<HomeController> logger,IMapper mapper, IBookingService bookingService,IEventService eventService)
        {
            _logger = logger;
          
			_mapper = mapper;
			_response = new();
            _bookingService = bookingService;

            _eventService =eventService;	

        }

		//[HttpGet("status=true")]
		public async Task<ActionResult<APIResponse>> Index()
		{
            //// Retrieve all items where status is true
            //IEnumerable<Event> items = await _unitOfWork.Event.GetAllAsync();
            //IEnumerable<Event> filteredItems = items.Where(x => x.status).ToList();


            //return View(filteredItems);
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

        public async Task<IActionResult> BookTicket(int id)

		{
            var events=await _eventService.GetAsync<APIResponse>(id);
            Event model = JsonConvert.DeserializeObject<Event>(Convert.ToString(events.Result));


            return View(model);
		}
		[HttpPost]
		public async Task<ActionResult> BookTicket( [FromBody] BookingCreateDTO createDTO)
		{



            //if (createDTO == null)
            //{
            //	return BadRequest();
            //}

            //if (await _unitOfWork.Event.GetAsync(u => u.EventId == createDTO.EventId) == null)
            //{
            //	ModelState.AddModelError("ErrorMessages", "EventId is invalid");
            //	return BadRequest(ModelState);
            //}
            //var eventtoupdate = await _unitOfWork.Event.GetAsync(u => u.EventId == createDTO.EventId);
            //int available = eventtoupdate.AvailableSeats;
            //int updatedone = available - createDTO.NumberOfTickets;
            //if (updatedone < 0)
            //{
            //	//some exception
            //}
            //eventtoupdate.AvailableSeats = updatedone;
            //await _unitOfWork.Event.UpdateAsync(eventtoupdate);
            ////save event
            //Booking booking = _mapper.Map<Booking>(createDTO);

            //await _unitOfWork.Booking.CreateAsync(booking);
            //return RedirectToAction(nameof(Index));
         
            if (ModelState.IsValid)
            {
                var response = await _bookingService.CreateAsync<APIResponse>(createDTO);

                
                    //TempData["success"] = "Event created";

            
            }


                return RedirectToAction(nameof(Index));
            }



        public async Task<IActionResult> CreateVBooking()
        {
            BookingCreateVM bookingCreateVM = new();
            var response = await _eventService.GetAllAsync<APIResponse>();
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVBooking(BookingCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _bookingService.CreateAsync<APIResponse>(model.booking);
                if (response != null && response.IsSuccess)
                {
                   
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