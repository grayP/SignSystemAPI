using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignSystem.API.Entities
{
    public class SignSystemInfoContext : DbContext
    {
        public SignSystemInfoContext(DbContextOptions<SignSystemInfoContext> options)
           : base(options)
        {
           // Database.EnsureCreated();
           // Database.Migrate();
        }

        public DbSet<Sign> Sign { get; set; }
        public DbSet<Store> Store { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionstring");

        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
