using TB.DataAccess.Models.DTO;
using TB.DataAccess.Models;

namespace TicketBookingWeb.Services.IServices
{
    public interface IBookingService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateWithoutAsync<T>(BookingCreateDTO dto);
        Task<T> UpdateAsync<T>(Booking dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> CreateAsync<T>(BookingCreateDTO dto);


    }
}
