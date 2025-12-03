using System.ComponentModel.DataAnnotations;

namespace FormEODY.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string? UserId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "User Type is required")]
        public int UserType { get; set; }

        public int? HospitalId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
