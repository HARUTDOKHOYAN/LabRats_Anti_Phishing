using Microsoft.AspNetCore.Identity;

namespace LearningASPweb.Data;

public class APIUserModel : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}