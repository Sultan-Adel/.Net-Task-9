using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Interfaces;
using SchoolSystem.ViewModels;

namespace SchoolSystem.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var vm = await _studentService.PrepareAddFormAsync();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StudentFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(await _studentService.RepopulateFormAsync(vm));
            }

            var (success, errorMessage) = await _studentService.CreateStudentAsync(vm);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, errorMessage ?? "Unable to create student.");
                return View(await _studentService.RepopulateFormAsync(vm));
            }

            TempData["Success"] = "Student created successfully!";
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _studentService.PrepareEditFormAsync(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentFormViewModel vm)
        {
            if (id != vm.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(await _studentService.RepopulateFormAsync(vm));
            }

            var (success, errorMessage) = await _studentService.UpdateStudentAsync(vm);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, errorMessage ?? "Unable to update student.");
                return View(await _studentService.RepopulateFormAsync(vm));
            }

            TempData["Success"] = "Student updated successfully!";
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            TempData["Success"] = "Student deleted successfully!";
            return RedirectToAction(nameof(GetAll));
        }
    }
}
