using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolSystem.Interfaces;
using SchoolSystem.Models;
using SchoolSystem.ViewModels;

namespace SchoolSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IDepartmentRepository _departmentRepo;

        public StudentService(IStudentRepository studentRepo, IDepartmentRepository departmentRepo)
        {
            _studentRepo = studentRepo;
            _departmentRepo = departmentRepo;
        }

        private async Task<List<SelectListItem>> BuildDepartmentListAsync(int? selectedId = null)
        {
            var departments = await _departmentRepo.GetAllAsync();
            return departments
                .OrderBy(d => d.Name)
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name,
                    Selected = selectedId.HasValue && d.Id == selectedId.Value
                })
                .ToList();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync() => await _studentRepo.GetAllWithDepartmentAsync();

        public async Task<Student?> GetStudentByIdAsync(int id) => await _studentRepo.GetByIdWithDepartmentAsync(id);

        public async Task<StudentFormViewModel> PrepareAddFormAsync()
        {
            return new StudentFormViewModel
            {
                Departments = await BuildDepartmentListAsync()
            };
        }

        public async Task<StudentFormViewModel?> PrepareEditFormAsync(int id)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            if (student == null) return null;

            return new StudentFormViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                DepartmentId = student.DepartmentId,
                Departments = await BuildDepartmentListAsync(student.DepartmentId)
            };
        }

        public async Task<StudentFormViewModel> RepopulateFormAsync(StudentFormViewModel vm)
        {
            vm.Departments = await BuildDepartmentListAsync(vm.DepartmentId);
            return vm;
        }

        public async Task<(bool Success, string? ErrorMessage)> CreateStudentAsync(StudentFormViewModel vm)
        {
            var department = await _departmentRepo.GetByIdAsync(vm.DepartmentId);
            if (department == null)
            {
                return (false, "Selected department does not exist.");
            }

            var student = new Student
            {
                Name = vm.Name.Trim(),
                Age = vm.Age,
                DepartmentId = vm.DepartmentId
            };

            await _studentRepo.AddAsync(student);
            await _studentRepo.SaveChangesAsync();
            return (true, null);
        }

        public async Task<(bool Success, string? ErrorMessage)> UpdateStudentAsync(StudentFormViewModel vm)
        {
            var existing = await _studentRepo.GetByIdAsync(vm.Id);
            if (existing == null)
            {
                return (false, "Student not found.");
            }

            var department = await _departmentRepo.GetByIdAsync(vm.DepartmentId);
            if (department == null)
            {
                return (false, "Selected department does not exist.");
            }

            existing.Name = vm.Name.Trim();
            existing.Age = vm.Age;
            existing.DepartmentId = vm.DepartmentId;

            await _studentRepo.UpdateAsync(existing);
            await _studentRepo.SaveChangesAsync();
            return (true, null);
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _studentRepo.DeleteAsync(id);
            await _studentRepo.SaveChangesAsync();
        }
    }
}
