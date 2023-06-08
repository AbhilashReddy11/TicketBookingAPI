using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using TB.DataAccess.Data;
using TB.DataAccess.Models;
using TB.DataAccess.Models.DTO;
using TB.DataAccess.Repository;
using TB.DataAccess.Repository.IRepository;

namespace TicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {


        private readonly IMapper _mapper;
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;



        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _response = new();

        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()

        {
            try
            {

                IEnumerable<Employee> empList = await _unitOfWork.Employee.GetAllAsync();

                _response.Result = empList;


                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);


        }
        [HttpGet("{id:int}")]

        public async Task<ActionResult<APIResponse>> GetEmployee(int id)
        {
            try
            {

                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }


                var emp = await _unitOfWork.Employee.GetAsync(u => u.EmployeeId == id);
                if (emp == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);


                }
                _response.Result = emp;

                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateEmployee([FromBody] EmployeeDTO createDTO)
        {
            try
            {

                if (await _unitOfWork.Employee.GetAsync(u => u.EmployeeName.ToLower() == createDTO.EmployeeName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Employee already exists!");
                    return BadRequest(ModelState);
                }


                if (createDTO == null)
                {
                    return BadRequest();
                }

                Employee emps = _mapper.Map<Employee>(createDTO);

                await _unitOfWork.Employee.CreateAsync(emps);


                _response.Result = emps;
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetEmployee", new { id = emps.EmployeeId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]


        public async Task<ActionResult<APIResponse>> DeleteEmployee(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }


                var emps = await _unitOfWork.Employee.GetAsync(u => u.EmployeeId == id);
                if (emps == null)
                {
                    return NotFound();
                }


                await _unitOfWork.Employee.RemoveAsync(emps);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> UpdateEmployee(int id, [FromBody] Employee updateDTO)
        {
            try
            {

                if (updateDTO == null || id != updateDTO.EmployeeId)
                {
                    return BadRequest();
                }


                Employee model = updateDTO;



                await _unitOfWork.Employee.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

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
       





