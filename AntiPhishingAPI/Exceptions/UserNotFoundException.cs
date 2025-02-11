namespace LearningASPweb.Exceptions;

public class UserNotFoundException : ApplicationException
{
    public UserNotFoundException(string name , object key) : base($"NAME` {name} KEY` {key}")
    {
        
    }
}