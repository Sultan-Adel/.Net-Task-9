using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Interfaces;
using SchoolSystem.Models;

namespace SchoolSystem.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(SchoolDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Department>> GetAllWithStudentsAsync()
        {
            return await _context.Departments
                .Include(d => d.Students)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
