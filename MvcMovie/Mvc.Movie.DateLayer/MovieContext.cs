using System;
using System.Data.Entity;
using EntityConfiguration;
using MvcMovie.DomainClasses;

namespace MvcMovie.DateLayer
{
  public  class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Configurations.Add(new MovieConfig());
            modelBuilder.Configurations.Add(new SongConfig());
            base.OnModelCreating(modelBuilder);
        }
    }

  public class MovieInitializer : DropCreateDatabaseAlways<MovieContext>
  {
      protected override void Seed(MovieContext movieContext)
      {
          Movie movie = new Movie { Genre="Rock", ReleaseDate=DateTime.Now, Title="Rockt", Price=222 };
          movieContext.Movies.Add(movie);
          base.Seed(movieContext);
      }
  }
}
