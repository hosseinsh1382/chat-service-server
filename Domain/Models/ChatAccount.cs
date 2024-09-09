namespace Domain.Models;

public class ChatAccount
{
    public Guid AccountId { get; set; }
    public Account? Account { get; set; }
    public long ChatId { get; set; }
}