using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMovie.DomainClasses
{
   public class Song
    {
       public int Id { get; set; }
       public string FolderPath { get; set; }
       public virtual Movie Movie { get; set; }
       //public int MovieId { get; set; }

    }
}
