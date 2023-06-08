using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TB.DataAccess.Models;
using TB.DataAccess.Repository.IRepository;
using TicketBookingWeb.Services.IServices;

namespace TicketBookingWeb.ViewComponents
{
  
    public class CountdownViewComponent : ViewComponent
    {
        private readonly IEventService _eventService;

        public CountdownViewComponent(IEventService eventService)
        {
            _eventService = eventService;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
        
          var  events = await _eventService.GetAllTrueAsync<APIResponse>();
           

                List<Event> model = JsonConvert.DeserializeObject<List<Event>>(Convert.ToString(events.Result));
                
       


            Event firstEvent = model.OrderBy(e => e.EventDate).FirstOrDefault();

            if (firstEvent != null)
            {
                var countdownTime = firstEvent.EventDate - DateTime.UtcNow;
                return View("Default", countdownTime);
            }

            return Content("No events found.");
        }
    }
}
