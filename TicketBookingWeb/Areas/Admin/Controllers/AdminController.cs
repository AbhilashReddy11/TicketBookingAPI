using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using TB.DataAccess.Models;
using TB.DataAccess.Repository.IRepository;
using TicketBookingWeb.Services.IServices;

namespace TicketBookingWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        
        private readonly IEventService _eventService;



        public AdminController( IEventService eventService)
        {

             _eventService= eventService;
            


        }
       
        [HttpGet("status=false")]
        public async Task<ActionResult<APIResponse>> Getfalse()
        {
         
            var response = await _eventService.GetAllFalseAsync<APIResponse>();
            List<Event> list = new();

            list = JsonConvert.DeserializeObject<List<Event>>(Convert.ToString(response.Result));


            return View(list);


        }

        public async Task<ActionResult<APIResponse>> Approve(int EventId)
        {

          

            if (ModelState.IsValid)
            {
                var response = await _eventService.UpdateStatusAsync<APIResponse>(EventId);
               

            }
            TempData["success"] = "Approved";
            return RedirectToAction(nameof(Getfalse));

        }
		

		public async Task<ActionResult<APIResponse>> Reject(int EventId)
		{


            var response = await _eventService.DeleteAsync<APIResponse>(EventId);
            TempData["success"] = "rejected";

            return RedirectToAction(nameof(Getfalse));



        }
	}
}
