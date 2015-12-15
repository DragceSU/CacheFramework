using System.Data.Entity;

namespace EasyCaching.Model
{
  class BookRepository : DbContext
  {
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
  }
}
