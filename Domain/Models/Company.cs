using System;
using System.Collections.Generic;

namespace SriDurgaHariHaraBackend.Data.Models;

public partial class Company
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
