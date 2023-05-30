using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using System.Text;
using TB.DataAccess.Data;
using TB.DataAccess.Models;
using TB.DataAccess.Models.DTO;
using TB.DataAccess.Repository;
using TB.DataAccess.Repository.IRepository;

namespace TB.DataAccess.Repository
{
    public class UserRepository : Repository<LocalUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

       
        private string secretKey;

       
        public UserRepository(ApplicationDbContext db, IConfiguration configuration) : base(db)
        {
            _db = db;
            
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

      

        public bool IsUniqueUser(string username)
        {
            var user = _db.LocalUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower()
            && u.Password == loginRequestDTO.Password);
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            //LoginResponseDTO loginResponseDTO = new LoginResponseDTO() { User=user};
            //return loginResponseDTO;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                //  User = user
                User = user

            };
            return loginResponseDTO;


        }

        public async Task<LocalUser> Register(RegistrationDTO registrationDTO)
        {
            LocalUser user = new()
            {
                UserName = registrationDTO.UserName,
                Password = registrationDTO.Password,
                Email = registrationDTO.Email,
                Role = registrationDTO.Role
            };
            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
      
    }
}

    

