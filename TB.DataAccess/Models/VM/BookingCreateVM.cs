using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.DataAccess.Models.DTO;

namespace TB.DataAccess.Models.VM
{
    public class BookingCreateVM
    {
        public BookingCreateVM()
        {
            booking = new BookingCreateDTO();
        }
        public BookingCreateDTO booking { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> EventList { get; set; }
    }
}

