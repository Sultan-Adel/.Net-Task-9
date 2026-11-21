using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Interfaces;
using SchoolSystem.Models;

namespace SchoolSystem.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Student>> GetAllWithDepartmentAsync()
        {
            return await _context.Students
                .Include(s => s.Department)
                .AsNoTracking()
                .OrderBy(s => s.Id)
                .ToListAsync();
        }

        public async Task<Student?> GetByIdWithDepartmentAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
