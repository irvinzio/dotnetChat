using DotnetChat.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotnetChat.Data.Context
{
    public class DotnetChatContext : IdentityDbContext<ApplicationUser>
    {
        public DotnetChatContext(DbContextOptions<DotnetChatContext> options)
        : base(options)
        { }


        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
