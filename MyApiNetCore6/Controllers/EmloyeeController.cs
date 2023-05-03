using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Repositories;
using MyApiNetCore6.Models;
namespace MyApiNetCore6.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmloyeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmloyeeController(IEmployeeRepository repo)
        {
            _employeeRepo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(int? Department_ID, int? Is_Register)
        {
            try
            {
                return Ok(await _employeeRepo.GetAllEmployeesAsync(Department_ID, Is_Register));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{Employee_ID}")]
        public async Task<IActionResult> GetRegularPamentByID(int Employee_ID)
        {
            try
            {
                var em = await _employeeRepo.GetEmployeesAsync(Employee_ID);
                return em == null ? Ok("Not exist Employee_ID = " + Employee_ID) : Ok(em);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register(Employee_Model model)
        {
            try
            {
                var newEmployee_ID = await _employeeRepo.RegisterAsync(model);
                if (newEmployee_ID == 0)
                {
                    return Ok("Employee existed");
                }
                else if (newEmployee_ID == 1)
                {
                    return Ok("Enter your password, make sure your password have at least six character include one character number and one special character");
                }
                else if (newEmployee_ID == 2)
                {
                    return Ok("Your password must have at least one character number.");
                }
                else if (newEmployee_ID == 3)
                {
                    return Ok("Your password must have at least one special character.");
                }
                else
                {
                    var regularPayment = await _employeeRepo.GetEmployeesAsync(newEmployee_ID);
                    return regularPayment == null ? NotFound() : Ok(regularPayment);
                }

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
