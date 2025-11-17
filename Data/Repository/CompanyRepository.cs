using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SriDurgaHariHaraBackend.Application.Interfaces;
using SriDurgaHariHaraBackend.Data.Models;
using SriDurgaHariHaraBackend.Data.Persistence;

namespace SriDurgaHariHaraBackend.Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _db;

        public CompanyRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Company> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Companies
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Company>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Companies
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);
        }
    }
}