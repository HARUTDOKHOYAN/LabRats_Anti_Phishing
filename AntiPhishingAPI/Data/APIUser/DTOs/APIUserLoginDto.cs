using System.ComponentModel.DataAnnotations;

namespace LearningASPweb.Data.DTOs;

public class APIUserLoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(15, MinimumLength = 3 , ErrorMessage = "Username must be between 3 and 15 characters long.")]
    public string Password { get; set; }
}