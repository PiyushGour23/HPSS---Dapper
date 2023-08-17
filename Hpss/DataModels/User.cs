namespace Laptop.Models
{
    public class User
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string DOB { get; set; } = null!;
        public bool? IsActive { get; set; } 
    }
}
