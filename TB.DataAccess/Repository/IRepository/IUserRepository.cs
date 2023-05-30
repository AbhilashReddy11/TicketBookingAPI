

using TB.DataAccess.Models;
using TB.DataAccess.Models.DTO;

namespace TB.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<LocalUser>
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegistrationDTO registrationDTO);
        
        

    }
}
