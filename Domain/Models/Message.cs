namespace Domain.Models;

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string Sender { get; set; }
    /*public Guid AccountId { get; set; }
    public Account? Account { get; set; }*/
    public long ChatId { get; set; }
    public DateTime DateTime { get; set; }=DateTime.UtcNow;
}