using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using TB.DataAccess.Models;
using TB.DataAccess.Models.DTO;
using TicketBooking_Utility;
using TicketBookingWeb.Services.IServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TicketBookingWeb.Areas.Admin.Controllers
{
    [Area("Admin")]

   
    public class EmployeewebController : Controller
    {
        private readonly IEmployeeService _empService;
   

        public EmployeewebController(IEmployeeService empService)
        {

            
            _empService = empService;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> list = new();

            var response = await _empService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<Employee>>(Convert.ToString(response.Result));
            }
            return View(list);
        }


        public async Task<IActionResult> CreateEmployee()
        {

            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> CreateEmployee(EmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _empService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = " Employee created successfully";

                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "error";
            return View(model);
        }
       

    }
}







