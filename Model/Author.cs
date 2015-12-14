using System.Collections.Generic;

namespace EasyCaching.Model
{
  public class Author
  {
    public Author()
    {
      Books = new HashSet<Book>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Book> Books { get; set; }
  }
}
