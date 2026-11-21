using SchoolSystem.Models;

namespace SchoolSystem.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllWithDepartmentAsync();
    }
}
