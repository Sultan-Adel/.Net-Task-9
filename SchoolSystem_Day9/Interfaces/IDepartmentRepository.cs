using SchoolSystem.Models;

namespace SchoolSystem.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<IEnumerable<Department>> GetAllWithStudentsAsync();
    }
}
