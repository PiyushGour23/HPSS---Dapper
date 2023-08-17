using System.ComponentModel.DataAnnotations;

namespace Hpss.Model
{
    public class Employees
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Email { get; set; } = null!;
        [Required]
        public int CompanyId { get; set; }
    }
}
