﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.DataAccess.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Name { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
