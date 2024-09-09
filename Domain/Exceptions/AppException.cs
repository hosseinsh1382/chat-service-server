namespace Domain.Exceptions;

public class AppException : Exception
{
    public int StatusCode { get; set; }
    public Guid InnerCode { get; set; }

    public AppException()
    {
    }

    public AppException(string? message, int statusCode, Guid innerCode) : base(message)
    {
        StatusCode = statusCode;
        InnerCode = innerCode;
    }
}