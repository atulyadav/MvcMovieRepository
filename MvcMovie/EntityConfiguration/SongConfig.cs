using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcMovie.DomainClasses;

namespace EntityConfiguration
{
   public class SongConfig : EntityTypeConfiguration<Song>
    {
       public SongConfig()
       {
           HasKey(p=>p.Id);
          // Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           HasRequired(p => p.Movie).WithOptional(p => p.Song).WillCascadeOnDelete(true);
           
           //.Map(m=>m.MapKey("MovieId")).WillCascadeOnDelete(true)
       }
    }
}
