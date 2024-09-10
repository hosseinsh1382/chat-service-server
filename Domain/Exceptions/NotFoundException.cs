namespace Domain.Exceptions;

public class NotFoundException:AppException
{
    public NotFoundException(string? message, Guid innerCode) : base(message, 404, innerCode)
    {
    }
}