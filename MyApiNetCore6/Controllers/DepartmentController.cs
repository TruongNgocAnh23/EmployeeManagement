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
        [HttpGet]
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

        [HttpGet("{Department_ID}")]
       
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
        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department_Model model)
        {
            try
            {
                var newDepartment_ID = await _departmentRepo.AddDepartmentAsync(model);
                var department = await _departmentRepo.GetDepartmentAsync(newDepartment_ID);
                return department == null ? NotFound() : Ok(department);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(Department_Model model)
        {
            try
            {
                await _departmentRepo.UpdateDepartmentAsync(model);
                return Ok("Success!");
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Department_ID}")]
        public async Task<IActionResult> DeleteDepartment(int Department_ID)
        {
            try
            {
                await _departmentRepo.DeleteDepartmentAsync(Department_ID);
                return Ok("Success!");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
