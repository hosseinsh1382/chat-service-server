using Application.Abstractions;
using Infrastructure.Business;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MapperConfiguration = AutoMapper.MapperConfiguration;

namespace ChatServer;

public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection serviceCollection)
    {
        //serviceCollection.AddAutoMapper(typeof(MapperConfiguration));
        serviceCollection.AddDbContext<AppDbContext>(builder => builder.UseInMemoryDatabase("test"));
        serviceCollection.AddSignalR();
        serviceCollection.AddScoped<IMessageService, MessageService>();
    }
}