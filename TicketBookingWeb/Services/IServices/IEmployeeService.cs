

using TB.DataAccess.Models;
using TB.DataAccess.Models.DTO;


namespace TicketBookingWeb.Services.IServices
{
    public interface IEmployeeService
    {
        Task<T> GetAllAsync<T>();
        
        Task<T> CreateAsync<T>(EmployeeDTO dto);
        

    }
}
