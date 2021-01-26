using DotnetChat.Data.Context;
using DotnetChat.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace DotnetChat.Infrasctructure.Extensions.Initialization
{
    public static class DbInit
    {
        public static void PopulateDatabase(this IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<DotnetChatContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Users.AddRange(
                new List<User>()
                {
                    new User
                    {
                        Email = "irvinzio.ram@gmail.com",
                        UserName = "irvinzio",
                        Messages =  new List<Message>()
                        {
                            new Message() { Text = "hola" }
                        }
                    },
                    new User
                    {
                        Email = "bot.test@bot.com",
                        UserName = "tets bot",
                        Messages =  new List<Message>()
                        {
                            new Message() { Text = "hola, cuanto tiempo" }
                        }
                    }
                }
            );     
            context.SaveChanges();
        }

    }
}
