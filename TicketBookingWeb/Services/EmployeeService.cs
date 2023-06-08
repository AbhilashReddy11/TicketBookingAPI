

using TB.DataAccess.Models;
using TB.DataAccess.Models.DTO;
using TicketBooking_Utility;
using TicketBookingWeb.Models;
using TicketBookingWeb.Services.IServices;

namespace TicketBookingWeb.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string EventUrl;

        public EmployeeService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            EventUrl = configuration.GetValue<string>("ServiceUrls:TicketBookingAPI");

        }

        public Task<T> CreateAsync<T>(EmployeeDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Class1.ApiType.POST,
                Data = dto,
                Url = EventUrl + "/api/Employee"
         

            });
        }

      

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Class1.ApiType.GET,
                Url = EventUrl + "/api/Employee"
            });
        }

       
     
       
    }
}
