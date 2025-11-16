using SriDurgaHariHaraBackend.Data.Models;

namespace SriDurgaHariHaraBackend.Application.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetByIdAsync(Guid id);
        Task AddAsync(Invoice invoice);
        Task<List<Invoice>> GetAllAsync();
    }
}