namespace Domain.Models;

public class AccountConnection
{
    public Guid AccountId { get; set; }
    public Account? Account { get; set; }
    public string ConnectionId { get; set; }
}