using Melvicorp.Data;
using MoviePhile.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MoviePhile.Data
{
    public class MoviePhileDbContext : DbContext
    {
        public MoviePhileDbContext()
            //: base("name=main")
            : base(@"Data Source=.\sqlexpress2014;Initial Catalog=MoviePhile;Integrated Security=True")
        {
            Database.SetInitializer<MoviePhileDbContext>(null);
        }

        public DbSet<Movie> MovieSet { get; set; }
        public DbSet<Genre> GenreSet { get; set; }
        public DbSet<Actor> ActorSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Ignore<IIdentifiableEntity<int>>();

            modelBuilder.Entity<Movie>().HasKey<int>(e => e.MovieId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Genre>().HasKey<int>(e => e.GenreId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Actor>().HasKey<int>(e => e.ActorId).Ignore(e => e.EntityId);
            
            //modelBuilder.Entity<Movie>().HasRequired<Genre>(e => e.Genre).WithMany().HasForeignKey(e => e.GenreId);
            //modelBuilder.Entity<Movie>().HasMany<Actor>(e => e.Actors);
            //modelBuilder.Entity<Actor>().HasRequired<Movie>(e => e.Movie).WithMany().HasForeignKey(e => e.MovieId);
        }
    }
}
