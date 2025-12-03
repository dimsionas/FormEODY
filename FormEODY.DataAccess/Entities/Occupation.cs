using System.ComponentModel.DataAnnotations;

namespace FormEODY.DataAccess.Entities;

public class Occupation
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Application> Applications { get; set; } = new List<Application>();
}
