
using AutoMapper;
using TB.DataAccess.Models;
using TB.DataAccess.Models.DTO;

namespace TicketBookingAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()

        {
           
            CreateMap<Event, EventCreateDTO>().ReverseMap();
           

           
            CreateMap<Booking, BookingCreateDTO>().ReverseMap();
            

            
           
           

          
          

        }
    }
}

