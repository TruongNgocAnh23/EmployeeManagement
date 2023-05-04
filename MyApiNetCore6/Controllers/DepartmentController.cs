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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository repo)
        {
            _departmentRepo = repo;
        }
        [HttpGet("GetList")]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                return Ok(await _departmentRepo.GetAllDepartmentsAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetDepartmentBy{Department_ID}")]
        public async Task<IActionResult> GetDepartmentByID(int Department_ID)
        {
            try
            {
                var department = await _departmentRepo.GetDepartmentAsync(Department_ID);
                return department == null ? NotFound() : Ok(department);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("AddNew")]
        public async Task<IActionResult> AddDepartment(Department_Model model)
        {
            try
            {
                var newDepartment_ID = await _departmentRepo.AddDepartmentAsync(model);
                switch (newDepartment_ID)
                {
                    case 1:
                        return Ok("Department_ID already exists");
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
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateDepartment(Department_Model model)
        {
            try
            {
                var Update = await _departmentRepo.UpdateDepartmentAsync(model);
                switch (Update)
                {
                    case 1:
                        return Ok("Doesn't exist Department_ID = " + model.Department_ID);
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
        [HttpDelete("Delete{Department_ID}")]
        public async Task<IActionResult> DeleteDepartment(int Department_ID)
        {
            try
            {
               var Dele = await _departmentRepo.DeleteDepartmentAsync(Department_ID);
                switch (Dele)
                {
                    case 1:
                        return Ok("Doesn't exist Department_ID = " + Department_ID);
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
