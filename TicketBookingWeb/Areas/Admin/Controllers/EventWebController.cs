using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using TB.DataAccess.Models;
using TB.DataAccess.Models.DTO;
using TicketBooking_Utility;
using TicketBookingWeb.Services.IServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TicketBookingWeb.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class EventWebController : Controller
    {
        private readonly IEventService _eventService;
   

        public EventWebController(IEventService eventService)
        {

            
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
                    TempData["success"] = " Event created successfully";

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

                    TempData["success"] = " Event updated successfully";
                    return RedirectToAction(nameof(IndexEvent));
                }
            }
           
            return View(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(int EventId)
        {

            var response = await _eventService.DeleteAsync<APIResponse>(EventId);

            TempData["success"] = " deleted updated successfully";
            return RedirectToAction(nameof(IndexEvent));

        }
        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Event> list = new();

            var response = await _eventService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<Event>>(Convert.ToString(response.Result));
            }
          
            return Json(new { data = list });
        }


        

        #endregion

    }
}







