using System;
using System.Collections.Generic;
using DotnetChat.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetChat.Data.Context
{
    public class DotnetChatContext : DbContext
    {
        public DotnetChatContext(DbContextOptions<DotnetChatContext> options)
        : base(options)
        { }


        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
