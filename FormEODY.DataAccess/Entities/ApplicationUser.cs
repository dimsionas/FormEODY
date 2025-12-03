using Microsoft.AspNetCore.Identity;

namespace FormEODY.DataAccess.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int UserType { get; set; }
    public int? HospitalId { get; set; }
    public int? DepartmentId { get; set; }
}
