namespace Application.DTOs;

public class MessageResponse
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string Sender { get; set; }
    public DateTime DateTime { get; set; }
}