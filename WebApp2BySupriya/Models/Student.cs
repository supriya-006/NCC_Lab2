using System.ComponentModel.DataAnnotations;

namespace WebApp2BySupriya.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Student ID is required.")]
        [Range(1, 99999, ErrorMessage = "Student ID must be a positive integer.")]
        [Display(Name = "Student ID")]
        public int StdID { get; set; }

        [Required(ErrorMessage = "Student Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Faculty is required.")]
        [Display(Name = "Faculty")]
        public string Faculty { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;
    }
}
