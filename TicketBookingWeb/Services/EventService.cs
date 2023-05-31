

using TB.DataAccess.Models;
using TB.DataAccess.Models.DTO;
using TicketBooking_Utility;
using TicketBookingWeb.Models;
using TicketBookingWeb.Services.IServices;

namespace TicketBookingWeb.Services
{
    public class EventService : BaseService, IEventService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string EventUrl;

        public EventService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            EventUrl = configuration.GetValue<string>("ServiceUrls:TicketBookingAPI");

        }

        public Task<T> CreateAsync<T>(EventCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Class1.ApiType.POST,
                Data = dto,
                Url = EventUrl + "/api/Event"
         

            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Class1.ApiType.DELETE,
                Url = EventUrl + "/api/Event/" + id
                
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Class1.ApiType.GET,
                Url = EventUrl + "/api/Event"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Class1.ApiType.GET,
                Url = EventUrl + "/api/Event/" + id

            });
        }

        public Task<T> UpdateAsync<T>(Event dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Class1.ApiType.PUT,
                Data = dto,
                Url = EventUrl + "/api/Event/" + dto.EventId

            }) ;
        }
    }
}
