using Microsoft.AspNetCore.Mvc;
using TB.DataAccess.Models;
using TB.DataAccess.Repository.IRepository;

namespace TicketBookingWeb.ViewComponents
{
	public class CountdownViewComponent : ViewComponent
	{
		private readonly IEventRepository _eventRepository;

		public CountdownViewComponent(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			List<Event> events;
			events = await _eventRepository.GetAllAsync();
				
			
		  Event firstEvent = events.OrderBy(e => e.EventDate).FirstOrDefault();

			if (firstEvent != null)
			{
				var countdownTime = firstEvent.EventDate - DateTime.UtcNow;
				return View("Default", countdownTime);
			}

			return Content("No events found.");
		}
	}
}
