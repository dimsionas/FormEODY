using System.ComponentModel.DataAnnotations;

namespace FormEODY.DataAccess.Entities;

public class Application
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name must be less than 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Gender is required")]
    public Gender Gender { get; set; }

    public string Occupation { get; set; } = string.Empty;

    public bool Single { get; set; }

    [Required(ErrorMessage = "Message is required")]
    [MaxLength(500, ErrorMessage = "Message must be less than 500 characters")]
    public string Message { get; set; } = string.Empty;
}

public enum Gender
{
    Male = 0,
    Female = 1
}
