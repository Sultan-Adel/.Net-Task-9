using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolSystem.Interfaces;
using SchoolSystem.Models;
using SchoolSystem.ViewModels;

namespace SchoolSystem.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IDepartmentRepository _departmentRepo;

        public CourseController(ICourseRepository courseRepo, IDepartmentRepository departmentRepo)
        {
            _courseRepo = courseRepo;
            _departmentRepo = departmentRepo;
        }

        private async Task<List<SelectListItem>> GetDepartmentsSelectListAsync(int? selectedId = null)
        {
            var departments = await _departmentRepo.GetAllAsync();
            return departments.OrderBy(d => d.Name).Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name,
                Selected = selectedId.HasValue && d.Id == selectedId.Value
            }).ToList();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepo.GetAllWithDepartmentAsync();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View(new CourseFormViewModel { Departments = await GetDepartmentsSelectListAsync() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CourseFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departments = await GetDepartmentsSelectListAsync(vm.DepartmentId);
                return View(vm);
            }

            var course = new Course
            {
                Name = vm.Name.Trim(),
                Degree = vm.Degree,
                MinDegree = vm.MinDegree,
                DepartmentId = vm.DepartmentId
            };

            await _courseRepo.AddAsync(course);
            await _courseRepo.SaveChangesAsync();
            TempData["Success"] = "Course added successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
