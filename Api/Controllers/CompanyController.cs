using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SriDurgaHariHaraBackend.Application.Dtos;
using SriDurgaHariHaraBackend.Application.Interfaces;

namespace SriDurgaHariHaraBackend.Api.Controllers
{
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _service;

        public CompanyController(ICompanyRepository service)
        {
            _service = service;
        }

        [HttpGet("getall")]
        [ProducesResponseType(typeof(IEnumerable<CompanyResponseDto>), 200)]
        public async Task<ActionResult<IEnumerable<CompanyResponseDto>>> GetAll(CancellationToken cancellationToken)
        {
            var companies = await _service.GetAllAsync(cancellationToken);
            return Ok(companies);
        }

        // GET: api/company/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CompanyResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var company = await _service.GetByIdAsync(id, cancellationToken);
            if (company == null) return NotFound();
            return Ok(company);
        }
    }
}