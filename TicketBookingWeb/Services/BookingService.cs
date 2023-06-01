using TB.DataAccess.Models.DTO;
using TB.DataAccess.Models;
using TicketBooking_Utility;
using TicketBookingWeb.Models;
using TicketBookingWeb.Services.IServices;

namespace TicketBookingWeb.Services
{
    public class BookingService : BaseService, IBookingService
    
    {
        private readonly IHttpClientFactory _clientFactory;
        private string EventUrl;

        public BookingService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            EventUrl = configuration.GetValue<string>("ServiceUrls:TicketBookingAPI");

        }

        public Task<T> CreateWithoutAsync<T>(BookingCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Class1.ApiType.POST,
                Data = dto,
                Url = EventUrl + "/api/Booking"


            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Class1.ApiType.DELETE,
                Url = EventUrl + "/api/Booking/" + id

            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Class1.ApiType.GET,
                Url = EventUrl + "/api/Booking"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Class1.ApiType.GET,
                Url = EventUrl + "/api/Booking/" + id

            });
        }

        public Task<T> UpdateAsync<T>(Booking dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Class1.ApiType.PUT,
                Data = dto,
                Url = EventUrl + "/api/Booking/" + dto.BookingId

            });
        }
       
        public Task<T> CreateAsync<T>(BookingCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Class1.ApiType.POST,
                Data = dto,
                Url = EventUrl + "/api/Booking/newcreate"


            });
        }
    }
}
