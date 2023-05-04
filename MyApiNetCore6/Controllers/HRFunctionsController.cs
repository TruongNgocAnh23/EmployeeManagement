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
    public class HRFunctionsController : ControllerBase
    {
        private readonly IHRFunctionsRepository _repo;

        public HRFunctionsController(IHRFunctionsRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("HRGetListTimesheet")]
        public async Task<IActionResult> HRGetListTimesheet(int? HR_ID, int? Department_ID, int? Year, int? Month)
        {
            try
            {
                return Ok(await _repo.HRGetListTimeSheetAsync(HR_ID, Department_ID, Year, Month));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetHighestWorkingHourOfEmployee")]
        public async Task<IActionResult> GetHighestWorkingHourOfEmployee(int? HR_ID, int? Department_ID, int? Year, int? Month)
        {
            try
            {
                var data = await _repo.GetHighestWorkingHourOfEmployee(HR_ID, Department_ID, Year, Month);
                return data == null ? Ok() : Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
