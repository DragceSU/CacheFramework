using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCaching.Model
{
  public class Book
  {
    public Book()
    {
      Authors = new HashSet<Author>();
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public virtual ICollection<Author> Authors { get; set; }
  }
}
