using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestChat.Models
{
    public class ApplicationContext:IdentityDbContext<User>
    {
        public DbSet<Friends> Friends { get; set; }
        public DbSet<ChatDialogs> ChatDialogs { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
