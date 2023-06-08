using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.DataAccess.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }
        [StringLength(20, ErrorMessage = "Location must not exceed 20 characters")]
        public string EventName { get; set; }
        
        public string EventDescription { get; set; }
        
        public DateTime EventDate { get; set; }
        [StringLength(50, ErrorMessage = "Location must not exceed 50 characters")]
        public string Location { get; set; }
        [Range(0, 100, ErrorMessage = "Available Seats must be between 0 and 100")]
        public int AvailableSeats { get; set; }
        public double TicketPrice { get; set; }
        public bool status { get; set; }
    }
}
