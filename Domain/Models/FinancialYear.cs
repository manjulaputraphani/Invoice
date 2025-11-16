using System;
using System.Collections.Generic;

namespace SriDurgaHariHaraBackend.Data.Models;

public partial class FinancialYear
{
    public string FyCode { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
