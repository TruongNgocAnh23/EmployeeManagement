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

        [HttpGet]
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
        [HttpGet("{RegularPayment_ID}")]
        public async Task<IActionResult> GetRegularPamentByID(int RegularPayment_ID)
        {
            try
            {
                var re = await _regularPaymentRepo.GetRegularPaymentAsync(RegularPayment_ID);
                return re == null ? Ok("Not exist RegularPayment_ID = " + RegularPayment_ID) : Ok(re);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddRegularPayment(RegularPayment_Model model)
        {
            try
            {
                var newRegularPayment_ID = await _regularPaymentRepo.AddRegularPaymentAsync(model);
                if (newRegularPayment_ID == 0)
                {
                    return Ok("Not exists Department_ID = " + model.Department_ID);
                }    
                else
                {
                    var regularPayment = await _regularPaymentRepo.GetRegularPaymentAsync(newRegularPayment_ID);
                    return regularPayment == null ? NotFound() : Ok(regularPayment);
                }    
               
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(RegularPayment_Model model)
        {
            try
            {
                var Update = await _regularPaymentRepo.UpdateRegularPaymentAsync(model);
                if (Update == 0)
                {
                    return Ok("Not exists RegularPayment_ID = " + model.RegularPayment_ID);
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
        [HttpDelete("{RegularPayment_ID}")]
        public async Task<IActionResult> DeleteDepartment(int RegularPayment_ID)
        {
            try
            {
                var Delete = await _regularPaymentRepo.DeleteRegularPaymentAsync(RegularPayment_ID);
                if (Delete == 0)
                {
                    return Ok("Not exists RegularPayment_ID = " + RegularPayment_ID);
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
