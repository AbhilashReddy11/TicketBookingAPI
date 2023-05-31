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
    public class EventController : ControllerBase
    {


        private readonly IMapper _mapper;
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;



        public EventController(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _response = new();

        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()

        {
            try
            {

                IEnumerable<Event> eventList = await _unitOfWork.Event.GetAllAsync();

                _response.Result = eventList;


                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);


        }
        [HttpGet("{id:int}", Name = "GetEvent")]

        public async Task<ActionResult<APIResponse>> GetEvent(int id)
        {
            try
            {

                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }


                var events = await _unitOfWork.Event.GetAsync(u => u.EventId == id);
                if (events == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);


                }
                _response.Result = events;

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
        public async Task<ActionResult<APIResponse>> CreateEvent([FromBody] EventCreateDTO createDTO)
        {
            try
            {

                if (await _unitOfWork.Event.GetAsync(u => u.EventName.ToLower() == createDTO.EventName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Event already exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest();
                }

                Event events = _mapper.Map<Event>(createDTO);

                await _unitOfWork.Event.CreateAsync(events);


                _response.Result = _mapper.Map<Event>(events);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetEvent", new { id = events.EventId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult<APIResponse>> DeleteEvent(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }


                var events = await _unitOfWork.Event.GetAsync(u => u.EventId == id);
                if (events == null)
                {
                    return NotFound();
                }


                await _unitOfWork.Event.RemoveAsync(events);
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
        public async Task<ActionResult<APIResponse>> UpdateEvent(int id, [FromBody] Event updateDTO)
        {
            try
            {

                if (updateDTO == null || id != updateDTO.EventId)
                {
                    return BadRequest();
                }


                Event model = updateDTO;



                await _unitOfWork.Event.UpdateAsync(model);
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
        [HttpPost("{id:int}")]
        public async Task<ActionResult<APIResponse>> UpdateDataInDatabase(int id)
        {

            // Find the data from the database using the provided ID
            var data = await _unitOfWork.Event.GetAsync(u => u.EventId == id);

            if (data != null)
            {

                data.status = true;


                await _unitOfWork.Event.SaveAsync();
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);


            }
            else
            {

                _response.IsSuccess = false;

            }
            return _response;

        }
        [HttpGet("GetStatusTrue/status=true")]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            // Retrieve all items where status is true
            List<Event> items = await _unitOfWork.Event.GetAllAsync();
            List<Event> filteredItems = items.Where(x => x.status).ToList();


            return Ok(filteredItems);
        }
        [HttpGet("GetStatusTrue/status=false")]
        public async Task<ActionResult<APIResponse>> Getfalse()
        {
            // Retrieve all items where status is true
            List<Event> items = await _unitOfWork.Event.GetAllAsync();
            List<Event> filteredItems = items.Where(x => !x.status).ToList();


            return Ok(filteredItems);
        }
    }
}







