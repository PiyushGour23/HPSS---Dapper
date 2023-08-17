namespace Laptop.Models
{
    public class UserModel
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string DateofBirth { get; set; } = null!;
        public bool? IsActive { get; set; } 
    }
}
