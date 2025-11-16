using System;
using System.Collections.Generic;

namespace SriDurgaHariHaraBackend.Data.Models;

public partial class Customer
{
    public Guid Id { get; set; }

    public string CustomerCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Gstin { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? StateCode { get; set; }

    public string? Address { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
