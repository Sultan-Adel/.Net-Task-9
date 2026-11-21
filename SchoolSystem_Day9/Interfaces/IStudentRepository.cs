using SchoolSystem.Models;

namespace SchoolSystem.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<IEnumerable<Student>> GetAllWithDepartmentAsync();
        Task<Student?> GetByIdWithDepartmentAsync(int id);
    }
}
