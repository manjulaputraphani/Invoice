using System;
using System.Collections.Generic;

namespace SriDurgaHariHaraBackend.Data.Models;

public partial class InvoiceItem
{
    public Guid Id { get; set; }

    public Guid InvoiceId { get; set; }

    public Guid? ProductId { get; set; }

    public string Description { get; set; } = null!;

    public string? HsnSac { get; set; }

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal GstRate { get; set; }

    public decimal? Discount { get; set; }

    public decimal? TaxableValue { get; set; }

    public decimal? Cgst { get; set; }

    public decimal? Sgst { get; set; }

    public decimal? Igst { get; set; }

    public decimal? LineTotal { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual Product? Product { get; set; }
}
