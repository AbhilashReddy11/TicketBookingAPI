using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TB.DataAccess.Data;
using TB.DataAccess.Models.DTO;
using TB.DataAccess.Models;
using TB.DataAccess.Repository.IRepository;
using Azure;
using TB.DataAccess.Repository;

namespace TicketBookingAPI.Controllers
{
    [Route("api/Booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {

    
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;
        protected APIResponse _response;



        public BookingController(IUnitOfWork unitOfWork, IMapper mapper)
        {
           
            _mapper = mapper;
            _response = new();
        
            _unitOfWork = unitOfWork;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()

        {
            try
            {
              
                IEnumerable<Booking> bookingList = await _unitOfWork.Booking.GetAllAsync(includeProperties: "events");

                _response.Result = bookingList;


                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);


        }
        [HttpGet("{id:int}", Name = "GetBooking")]

        public async Task<ActionResult<APIResponse>> GetBooking(int id)
        {
            try
            {

                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

            
                var booking = await _unitOfWork.Booking.GetAsync(u => u.EventId == id, includeProperties: "events");
                if (booking == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);


                }
                _response.Result = booking;

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
        public async Task<ActionResult<APIResponse>> CreateBooking([FromBody] BookingCreateDTO createDTO)
        {
            try
            {
               

                if (createDTO == null)
                {
                    return BadRequest();
                }
              
                if (await _unitOfWork.Event.GetAsync(u => u.EventId == createDTO.EventId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "EventId is invalid");
                    return BadRequest(ModelState);
                }

                Booking booking = _mapper.Map<Booking>(createDTO);
         
                await _unitOfWork.Booking.CreateAsync(booking);


                _response.Result = _mapper.Map<Booking>(booking);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetBooking", new { id = booking.EventId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

      
        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> UpdateEvent(int id, [FromBody] Booking updateDTO)
        {
            try
            {

                if (updateDTO == null || id != updateDTO.EventId)
                {
                    return BadRequest();
                }
              
                if (await _unitOfWork.Booking.GetAsync(u => u.EventId == updateDTO.EventId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "EventId is invalid");
                    return BadRequest(ModelState);
                }

                Booking model = updateDTO;


              
                await _unitOfWork.Booking.UpdateAsync(model);
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
		[HttpPost("newcreate")]
		public async Task<ActionResult<APIResponse>> Createnew([FromBody] BookingCreateDTO createDTO)
		{
			try
			{

                if (await _unitOfWork.Booking.GetAsync(u => u.CustomerName == createDTO.CustomerName) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Customername already taken");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
				{
					return BadRequest();
				}

				if (await _unitOfWork.Event.GetAsync(u => u.EventId == createDTO.EventId) == null)
				{
					ModelState.AddModelError("ErrorMessages", "EventId is invalid");
					return BadRequest(ModelState);
				}
				var eventtoupdate = await _unitOfWork.Event.GetAsync(u => u.EventId == createDTO.EventId);
                int available = eventtoupdate.AvailableSeats;
                int updatedone = available - createDTO.NumberOfTickets;
                if (updatedone < 0) {
                    ModelState.AddModelError("ErrorMessages", "Insufficient tickets");
                    return BadRequest(ModelState);
                    //some exception
                }
                eventtoupdate.AvailableSeats = updatedone;
               await _unitOfWork.Event.UpdateAsync(eventtoupdate);
                //save event
				Booking booking = _mapper.Map<Booking>(createDTO);

				await _unitOfWork.Booking.CreateAsync(booking);


				_response.Result = _mapper.Map<Booking>(booking);
				_response.StatusCode = HttpStatusCode.OK;
				return CreatedAtRoute("GetBooking", new { id = booking.EventId }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.ToString() };
			}
			return _response;
		}
        [HttpDelete("{id:int}")]

        public async Task<ActionResult<APIResponse>> DeletecountBooking(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }


                var booking = await _unitOfWork.Booking.GetAsync(u => u.BookingId == id);
                int bookedtickets = booking.NumberOfTickets;
                int eventid = booking.EventId;
                var events = await _unitOfWork.Event.GetAsync(u => u.EventId == eventid);
                int jstavailable = events.AvailableSeats;
                int updatedseats = jstavailable + bookedtickets;
                events.AvailableSeats = updatedseats;
                await _unitOfWork.Event.UpdateAsync(events);

                if (booking == null)
                {
                    return NotFound();
                }


                await _unitOfWork.Booking.RemoveAsync(booking);
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
    }
	
}
