namespace WebApp7BySupriya.Data.Entities
{
    // Simulated scaffolded entity from an existing database
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Faculty { get; set; } = null!;
        public double Gpa { get; set; }
    }
}
