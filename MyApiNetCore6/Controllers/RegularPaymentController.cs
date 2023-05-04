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
    public class RegularPaymentController : ControllerBase
    {
        private readonly IRegularPaymentRepository _regularPaymentRepo;
        public RegularPaymentController(IRegularPaymentRepository repo)
        {
            _regularPaymentRepo = repo;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetAllRegularPayments()
        {
            try
            {
                return Ok(await _regularPaymentRepo.GetAllRegularPaymentsAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetRegularPaymentBy{RegularPayment_ID}")]
        public async Task<IActionResult> GetRegularPamentByID(int RegularPayment_ID)
        {
            try
            {
                var re = await _regularPaymentRepo.GetRegularPaymentAsync(RegularPayment_ID);
                return re == null ? Ok("Doesn't exist RegularPayment_ID = " + RegularPayment_ID) : Ok(re);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("AddNew")]
        public async Task<IActionResult> AddRegularPayment(RegularPayment_Model model)
        {
            try
            {
                var newRegularPayment_ID = await _regularPaymentRepo.AddRegularPaymentAsync(model);
                switch (newRegularPayment_ID)
                {
                    case 1:
                        return Ok("Enter RegularPaymentPerHour, make sure your value > 0");
                        break;
                    case 2:
                        return Ok("Doesn't exists Department_ID = " + model.Department_ID);
                        break;
                    case 3:
                        return Ok("RegularPayment_ID already exists, please set RegularPayment_ID = 0");
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
        public async Task<IActionResult> UpdateDepartment(RegularPayment_Model model)
        {
            try
            {
                var Update = await _regularPaymentRepo.UpdateRegularPaymentAsync(model);
                switch(Update)
                {
                    case 0:
                        return Ok("Doesn't exists RegularPayment_ID = " + model.RegularPayment_ID);
                        break;
                    case 1:
                        return Ok("Enter RegularPaymentPerHour, make sure your value > 0");
                        break;
                    default:
                        return Ok("Success!");
                        break;
                } 
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("Delete{RegularPayment_ID}")]
        public async Task<IActionResult> DeleteDepartment(int RegularPayment_ID)
        {
            try
            {
                var Delete = await _regularPaymentRepo.DeleteRegularPaymentAsync(RegularPayment_ID);
                if (Delete == 0)
                {
                    return Ok("Doesn't exists RegularPayment_ID = " + RegularPayment_ID);
                }    
                else
                {
                    return Ok("Success!");
                }    
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
