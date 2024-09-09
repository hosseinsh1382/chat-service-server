namespace Domain.Exceptions;

public class InvalidPasswordException : AppException
{
    public InvalidPasswordException() : base("Invalid password", 401, new Guid("84164269-D635-4C5F-9131-6FB4DC91A597"))
    {
    }
}