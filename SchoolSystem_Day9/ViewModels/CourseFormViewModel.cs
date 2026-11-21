using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolSystem.ViewModels
{
    public class CourseFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course Name is required.")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 500)]
        public int Degree { get; set; }

        [Required]
        [Range(0, 500)]
        public int MinDegree { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int DepartmentId { get; set; }

        public List<SelectListItem> Departments { get; set; } = new List<SelectListItem>();
    }
}
