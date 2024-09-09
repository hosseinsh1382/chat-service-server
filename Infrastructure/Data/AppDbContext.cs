using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ChatAccount> Chats { get; set; }
    public DbSet<AccountConnection> AccountConnections { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatAccount>().HasKey(x => new { x.AccountId, x.ChatId });
        modelBuilder.Entity<AccountConnection>().HasKey(x => new { x.AccountId, x.ConnectionId });
    }
}