using System.ComponentModel.DataAnnotations;

namespace WebApp6BySupriya.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Faculty { get; set; }

        [Range(0, 4)]
        public double Gpa { get; set; }
    }
}
