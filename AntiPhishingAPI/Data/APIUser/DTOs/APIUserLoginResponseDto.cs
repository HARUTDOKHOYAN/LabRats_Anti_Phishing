namespace LearningASPweb.Data.DTOs;

public class APIUserLoginResponseDto
{
  public  string Token { get; set; }
  public string UserID { get; set; }
  
  public string RefreshToken { get; set; }
}