using System.ComponentModel.DataAnnotations;

namespace ContactForm.Web.Components.Models;

internal sealed record ContactFormModel
{
    [Required]
    [RegularExpression(@"[^\s]+", ErrorMessage = "Remove white space from First name input")]
    [StringLength(100, ErrorMessage = "First name is too long")]
    public string FirstName { get; set; } = null!;

    [Required]
    [RegularExpression(@"[^\s]+", ErrorMessage = "Remove white space from Last name input")]
    [StringLength(100, ErrorMessage = "Last name is too long")]
    public string LastName { get; set; } = null!;

    [Required]
    [RegularExpression(@"[^\s]+", ErrorMessage = "Remove white space from Email input")]
    public string Email { get; set; } = null!;
}