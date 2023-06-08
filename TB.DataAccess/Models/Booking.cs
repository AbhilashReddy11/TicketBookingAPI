using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TB.DataAccess.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }

        public Event events  { get; set; }
        


        public string? Name { get; set; }
        [Range(0, 5, ErrorMessage = "Available Seats must be between 0 and 5")]
        public int NumberOfTickets { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
