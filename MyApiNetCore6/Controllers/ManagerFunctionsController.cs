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
    public class ManagerFunctionsController : ControllerBase
    {
        private readonly IManagerFunctionsRepository _repo;

        public ManagerFunctionsController(IManagerFunctionsRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetListTimesheet")]
        public async Task<IActionResult> GetListTimesheet(int? Employee_ID, int? Year, int? Month)
        {
            try
            {
                return Ok(await _repo.GetListTimeSheetAsync(Employee_ID, Year, Month));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("AddTimesheet")]
        public async Task<IActionResult> AddTimeSheet(AddTimesheet_Model model)
        {
            try
            {
                var CheckIn = await _repo.AddTimesheetAsync(model);
                switch (CheckIn)
                {
                    case 1:
                        return Ok("Doesn't exists Employee_ID = " + model.Employee_ID);
                        break;
                    case 2:
                        return Ok("This Employee_ID has already has timesheet");
                        break;
                    case 3:
                        return Ok("Enter CheckIn");
                        break;
                    case 5:
                        return Ok("Only Manager can use this function!");
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
        [HttpPut("UpdateTimesheet")]
        public async Task<IActionResult> UpdateTimesheet(UpdateTimesheet_Model model)
        {
            try
            {
                var CheckIn = await _repo.UpdateTimesheetAsync(model);
                switch (CheckIn)
                {
                    case 1:
                        return Ok("Doesn't exists Employee_ID = " + model.Employee_ID);
                        break;
                    case 2:
                        return Ok("Doesn't exists Timesheet_ID = " + model.Timesheet_ID);
                        break;
                    case 3:
                        return Ok("Enter CheckIn");
                        break;
                    case 5:
                        return Ok("Only Manager can use this function!");
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
        [HttpDelete("DeleteTimesheet")]
        public async Task<IActionResult> DeleteTimesheet(DeleteTimesheet_Model model)
        {
            try
            {
                var CheckIn = await _repo.DeleteTimesheetAsync(model);
                switch (CheckIn)
                {
                    case 1:
                        return Ok("Doesn't exists Timesheet_ID = " + model.Timesheet_ID);
                        break;
                    case 2:
                        return Ok("Only Manager can use this function!");
                        break;
                    case 3:
                        return Ok("Can not delete this timesheet");
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
