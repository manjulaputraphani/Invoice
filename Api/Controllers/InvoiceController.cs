using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SriDurgaHariHaraBackend.Api.Attributes;
using SriDurgaHariHaraBackend.Application.Interfaces;
using SriDurgaHariHaraBackend.Data.Models;

namespace SriDurgaHariHaraBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {

        private readonly IInvoiceRepository _service;

        public InvoiceController(IInvoiceRepository service)
        {
            _service = service;
        }
        [HttpPost]
        [ValidateRequest(typeof(Invoice))]
        public IActionResult Create([FromBody] Invoice dto)
        {
            // if middleware passed, dto is valid
            return Ok(new { success = true });
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var invoice = await _service.GetByIdAsync(id);
            if (invoice == null) return NotFound();

            // Option A: return entity directly (ok for internal API)
            // Option B (recommended): map to a response DTO that hides navigation cycles
            return Ok(invoice);
        }

        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     var invoices = await _service.GetAllAsync();
        //     return Ok(invoices);
        // }
    }
}