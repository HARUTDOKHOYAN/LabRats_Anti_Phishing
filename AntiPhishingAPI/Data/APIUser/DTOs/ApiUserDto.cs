using System.ComponentModel.DataAnnotations;

namespace LearningASPweb.Data.DTOs;

public class ApiUserDto : APIUserLoginDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }


}