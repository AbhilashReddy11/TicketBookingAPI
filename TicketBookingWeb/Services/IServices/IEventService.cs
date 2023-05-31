

using TB.DataAccess.Models;
using TB.DataAccess.Models.DTO;


namespace TicketBookingWeb.Services.IServices
{
    public interface IEventService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(EventCreateDTO dto);
        Task<T> UpdateAsync<T>(Event dto);
        Task<T> DeleteAsync<T>(int id);

    }
}
