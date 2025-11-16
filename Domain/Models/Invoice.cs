using System;
using System.Collections.Generic;

namespace SriDurgaHariHaraBackend.Data.Models;

public partial class Invoice
{
    public Guid Id { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public string FyCode { get; set; } = null!;

    public Guid CustomerId { get; set; }

    public DateOnly InvoiceDate { get; set; }

    public DateOnly DueDate { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? TotalGst { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? AmountPaid { get; set; }

    public decimal? BalanceDue { get; set; }

    public string? Status { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual FinancialYear FyCodeNavigation { get; set; } = null!;

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
