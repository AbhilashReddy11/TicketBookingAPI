﻿namespace TB.DataAccess.Models.DTO
{
    public class BookingCreateDTO
    {
        public int EventId { get; set; }
       
        public string? Name { get; set; }

        public int NumberOfTickets { get; set; }
        //public DateTime BookingDate { get; set; }
    }
}
