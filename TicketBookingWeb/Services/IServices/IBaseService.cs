using TB.DataAccess.Models;
using TicketBookingWeb.Models;

namespace TicketBookingWeb.Services.IServices
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
