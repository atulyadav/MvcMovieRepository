using System.Data.Entity.ModelConfiguration;
using MvcMovie.DomainClasses;

namespace EntityConfiguration
{
  public class MovieConfig : EntityTypeConfiguration<Movie>
    {
        public MovieConfig()
        {
            Property(p => p.Title).HasMaxLength(50);
            //Property(p => p.ReleaseDate);
            Property(p => p.Genre).HasMaxLength(15);
            Property(p => p.Price).HasColumnType("decimal");
            Property(p => p.ReleaseDate).HasColumnType("datetime2");

            //.HasColumnName("Release Date")
        }
        
    }
}
