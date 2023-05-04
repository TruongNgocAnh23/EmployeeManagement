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

        [HttpGet("GetList")]
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
        [HttpGet("GetEmloyeeBy{Employee_ID}")]
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
        [HttpPost("Register")]
        public async Task<IActionResult> Register(Employee_Model model)
        {
            try
            {
                var newEm = await _employeeRepo.RegisterAsync(model);
                switch (newEm)
                {
                    case 1:
                        return Ok("Enter your password, make sure your password have at least six character include one character number and one special character");
                        break;
                    case 2:
                        return Ok("Your password must have at least one character number.");
                        break;
                    case 3:
                        return Ok("Your password must have at least one special character.");
                        break;
                    case 4:
                        return Ok("Employee already exists, please set Employee_ID = 0");
                        break;
                    case 5:
                        return Ok("Please check Gender <Male = 1; Female = 2; Other = 3>");
                        break;
                    case 6:
                        return Ok("Department_ID doestn't exists");
                        break;
                    default:
                        return Ok("Success");
                        break;
                }    
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePassword_Model model)
        {
            try
            {
                var updateEm = await _employeeRepo.UpdateEmployeeeAsync(model);
                switch (updateEm)
                {
                    case 1:
                        return Ok("Enter your password, make sure your password have at least six character include one character number and one special character");
                        break;
                    case 2:
                        return Ok("Your password must have at least one character number.");
                        break;
                    case 3:
                        return Ok("Your password must have at least one special character.");
                        break;
                    case 4:
                        return Ok("Employee_ID doesn't exists");
                        break;
                    default:
                        return Ok("Success");
                        break;
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("CheckIn")]
        public async Task<IActionResult> CheckIn(CheckIn_Model model)
        {
            try
            {
                var CheckIn = await _employeeRepo.CheckInAsync(model);
                switch (CheckIn)
                {
                    case 1:
                        return Ok("Doesn't exists Employee_ID = " + model.Employee_ID);
                        break;
                    case 2:
                        return Ok("This Employee_ID has already checked in");
                        break;
                    case 3:
                        return Ok("Enter CheckIn");
                        break;
                    default:
                        return Ok("Success");
                        break;
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("CheckOut")]
        public async Task<IActionResult> CheckOut(CheckOut_Model model)
        {
            try
            {
                var CheckIn = await _employeeRepo.CheckOutAsync(model);
                switch (CheckIn)
                {
                    case 1:
                        return Ok("Doesn't exists TimeSheet_ID = " + model.TimeSheet_ID);
                        break;
                    case 2:
                        return Ok("Enter CheckIn");
                        break;
                    default:
                        return Ok("Success");
                        break;
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("SetManager")]
        public async Task<IActionResult> SetManager(SetManager_Model model)
        {
            try
            {
                var CheckIn = await _employeeRepo.SetManagerAsync(model);
                switch (CheckIn)
                {
                    case 1:
                        return Ok("Doesn't exists Employee_ID = " + model.Employee_ID);
                        break;
                    default:
                        return Ok("Success");
                        break;
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
