using DotnetChat.Data.Context;
using DotnetChat.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
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
                        Email = "test2@test.com",
                        UserName = "testuser2",
                        Messages =  new List<Message>()
                        {
                            new Message() { Text = "hola", CreatedAt = DateTime.Now }
                        }
                       
                    },
                    new User
                    {
                        Email = "test1@test.com",
                        UserName = "testuser1",
                        Messages =  new List<Message>()
                        {
                            new Message() { Text = "hola, cuanto tiempo", CreatedAt = DateTime.Now  }
                        }
                    }
                }
            );     
            context.SaveChanges();
        }

    }
}
