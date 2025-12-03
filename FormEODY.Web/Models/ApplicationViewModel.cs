using System.ComponentModel.DataAnnotations;
using FormEODY.DataAccess.Entities;

namespace FormEODY.Web.Models
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name must be less than 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Gender is required")]
    public Gender Gender { get; set; }

    [Required(ErrorMessage = "Occupation is required")]
    public int OccupationId { get; set; }
    public string? OccupationName { get; set; }

        public bool Single { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [MaxLength(500, ErrorMessage = "Message must be less than 500 characters")]
        public string Message { get; set; } = string.Empty;
    }
}
