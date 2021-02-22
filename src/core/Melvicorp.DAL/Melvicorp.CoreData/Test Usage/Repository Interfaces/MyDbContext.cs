using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Melvicorp.CoreData.Test
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=myDb");
        }

        public DbSet<FieldGroup> FieldGroupSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<IIdentifiableEntity<int>>();

            modelBuilder.Entity<FieldGroup>().HasKey(e => e.FieldGroupId);
            modelBuilder.Entity<FieldGroup>().Ignore(e => e.EntityId);
        }
    }
}
