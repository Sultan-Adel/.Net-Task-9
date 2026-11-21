using SchoolSystem.Models;
using SchoolSystem.ViewModels;

namespace SchoolSystem.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<StudentFormViewModel> PrepareAddFormAsync();
        Task<StudentFormViewModel?> PrepareEditFormAsync(int id);
        Task<StudentFormViewModel> RepopulateFormAsync(StudentFormViewModel vm);
        Task<(bool Success, string? ErrorMessage)> CreateStudentAsync(StudentFormViewModel vm);
        Task<(bool Success, string? ErrorMessage)> UpdateStudentAsync(StudentFormViewModel vm);
        Task DeleteStudentAsync(int id);
    }
}
