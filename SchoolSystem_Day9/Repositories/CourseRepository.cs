using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Interfaces;
using SchoolSystem.Models;

namespace SchoolSystem.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(SchoolDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Course>> GetAllWithDepartmentAsync()
        {
            return await _context.Courses
                .Include(c => c.Department)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
