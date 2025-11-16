using System;
using System.Collections.Generic;

namespace SriDurgaHariHaraBackend.Data.Models;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid InvoiceId { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string? Method { get; set; }

    public string? ReferenceNo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
