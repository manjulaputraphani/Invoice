using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SriDurgaHariHaraBackend.Application.Dtos
{
    public class CompanyResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Gstin { get; set; } = null!;
    public string? StateCode { get; set; }
    public string? Pan { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? LogoUrl { get; set; }
    public string? BankName { get; set; }
    public string? AccountNumber { get; set; }
    public string? Ifsc { get; set; }
}
}