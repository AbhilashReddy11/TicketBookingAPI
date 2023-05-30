

using TB.DataAccess.Models.DTO;

namespace TicketBookingWeb.Services.IServices
{
	public interface IAuthService
	{
		Task<T> LoginAsync<T>(LoginRequestDTO objToCreate);
		Task<T> RegisterAsync<T>(RegistrationDTO objToCreate);
	}
}
