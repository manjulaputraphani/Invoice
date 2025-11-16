using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SriDurgaHariHaraBackend.Application.Interfaces;
using SriDurgaHariHaraBackend.Data.Models;
using SriDurgaHariHaraBackend.Data.Persistence;

namespace SriDurgaHariHaraBackend.Data.Repository
{
    public class InvoiceReadRepo : IInvoiceRepository
    {
        private readonly AppDbContext _db;

        public InvoiceReadRepo(AppDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task AddAsync(Invoice invoice)
        {
            if (invoice is null) throw new ArgumentNullException(nameof(invoice));

            // Ensure primary keys exist
            if (invoice.Id == Guid.Empty)
                invoice.Id = Guid.NewGuid();

            // Ensure created timestamp
            if (invoice.CreatedAt == null || invoice.CreatedAt == default)
                invoice.CreatedAt = DateTime.UtcNow;

            // Ensure InvoiceItems have ids and fix any relationships if needed
            if (invoice.InvoiceItems != null)
            {
                foreach (var item in invoice.InvoiceItems)
                {
                    if (item.Id == Guid.Empty)
                        item.Id = Guid.NewGuid();

                    // Ensure the FK is set (optional; EF will set this automatically when navigation is used)
                    item.InvoiceId = invoice.Id;
                }
            }

            // Add and save
            await _db.Invoices.AddAsync(invoice);
            await _db.SaveChangesAsync();
        }

        public async Task<Invoice> GetByIdAsync(Guid id)
        {
            return await _db.Invoices
                        .Include(i => i.InvoiceItems)
                        .Include(i => i.Payments)
                        .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _db.Invoices
                            .Include(i => i.InvoiceItems)
                            .Include(i => i.Payments)
                            .ToListAsync();
        }
    }
}