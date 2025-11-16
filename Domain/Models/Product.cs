using System;
using System.Collections.Generic;

namespace SriDurgaHariHaraBackend.Data.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public string Sku { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? HsnCode { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? GstRate { get; set; }

    public string? Unit { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}
