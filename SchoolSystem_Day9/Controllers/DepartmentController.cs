using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Interfaces;
using SchoolSystem.Models;

namespace SchoolSystem.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ShowAll()
        {
            var departments = await _departmentRepo.GetAllWithStudentsAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Add() => View(new Department());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Department department)
        {
            if (!ModelState.IsValid) return View(department);

            await _departmentRepo.AddAsync(department);
            await _departmentRepo.SaveChangesAsync();
            TempData["Success"] = "Department added successfully!";
            return RedirectToAction(nameof(ShowAll));
        }
    }
}
