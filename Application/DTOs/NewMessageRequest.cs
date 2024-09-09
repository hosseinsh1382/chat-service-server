namespace Application.DTOs;

public class NewMessageRequest
{
    public string Sender { get; set; }
    public string Text { get; set; }
    public long ChatId { get; set; }
}