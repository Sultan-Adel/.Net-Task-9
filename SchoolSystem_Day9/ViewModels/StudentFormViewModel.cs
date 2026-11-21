using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolSystem.ViewModels
{
    public class StudentFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Student Name is required.")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Age is required.")]
        [Range(16, 65)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid department.")]
        public int DepartmentId { get; set; }

        public List<SelectListItem> Departments { get; set; } = new List<SelectListItem>();
    }
}
