using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using TB.DataAccess.Models;
using TB.DataAccess.Models.DTO;
using TicketBooking_Utility;
using TicketBookingWeb.Services.IServices;

namespace TicketBookingWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [Authorize(Roles = SD.Role_Admin)]
    public class EventWebController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventWebController(IEventService eventService, IMapper mapper)
        {

            _mapper = mapper;
            _eventService = eventService;
        }
        public async Task<IActionResult> IndexEvent()
        {
            List<Event> list = new();

            var response = await _eventService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<Event>>(Convert.ToString(response.Result));
            }
            return View(list);
        }


        public async Task<IActionResult> CreateEvent()
        {

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent(EventCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _eventService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = " Event succesfully successfully";

                    return RedirectToAction(nameof(IndexEvent));
                }
            }
            TempData["error"] = "error";
            return View(model);
        }
        public async Task<IActionResult> UpdateEvent(int EventId)
        {
            var response = await _eventService.GetAsync<APIResponse>(EventId);
            if (response != null && response.IsSuccess)
            {

                Event model = JsonConvert.DeserializeObject<Event>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEvent(Event model)
        {
            if (ModelState.IsValid)
            {
                var response = await _eventService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {

                    //TempData["success"] = "Villa Updated";
                    return RedirectToAction(nameof(IndexEvent));
                }
            }
            //TempData["error"] = "error";
            return View(model);
        }


        public async Task<IActionResult> DeleteEvent(int EventId)
        {

            var response = await _eventService.DeleteAsync<APIResponse>(EventId);

         
            return RedirectToAction(nameof(IndexEvent));

        }

    }
}







