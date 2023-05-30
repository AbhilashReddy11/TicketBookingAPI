
using TB.DataAccess.Models.DTO;
using TicketBooking_Utility;
using TicketBookingWeb.Models;

using TicketBookingWeb.Services.IServices;

namespace TicketBookingWeb.Services
{
	public class AuthService : BaseService ,IAuthService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string Url;

		public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			Url = configuration.GetValue<string>("ServiceUrls:TicketBookingAPI");

		}

		public Task<T> LoginAsync<T>(LoginRequestDTO obj)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = Class1.ApiType.POST,
				Data = obj,
				Url = Url + "/api/User/login"
			});
		}

		public Task<T> RegisterAsync<T>(RegistrationDTO obj)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = Class1.ApiType.POST,
				Data = obj,
				Url = Url + "/api/User/register"
            });
		}
	}

	
		
			
		
	}
