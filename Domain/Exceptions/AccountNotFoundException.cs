namespace Domain.Exceptions;

public class AccountNotFoundException : NotFoundException
{
    public AccountNotFoundException() : base("Account not found", new Guid("6ABB7376-090A-43B6-9F36-DFC50FE1A43A"))
    {
    }
}