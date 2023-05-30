using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TB.DataAccess.Models.DTO;
using TB.DataAccess.Models;
using TB.DataAccess.Repository.IRepository;

namespace TicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       
        private readonly APIResponse _response;
      
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork )
        {
            _response = new();
           
            _unitOfWork = unitOfWork;
           

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            //var loginResponse = await _userRepo.Login(model);
            var loginResponse = await _unitOfWork.User.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
           
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            
            bool ifUserNameUnique = _unitOfWork.User.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("username already exists");
                return BadRequest(_response);

            }
            
            var user = await _unitOfWork.User.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while registering");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;

            return Ok(_response);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LocalUser>>> GetUsers()
        {
            try
            {
               
                IEnumerable<LocalUser> usersList = await _unitOfWork.User.GetAllAsync();

                _response.Result = usersList;
                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);


        }
       


    }
}
