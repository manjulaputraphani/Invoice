using System.Threading.Tasks;
using SriDurgaHariHaraBackend.Data.Models;

namespace SriDurgaHariHaraBackend.Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Company>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}