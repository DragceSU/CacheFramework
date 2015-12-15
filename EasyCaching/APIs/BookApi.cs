using System.Collections.Generic;
using System.Threading;
using EasyCaching.Model;
using CachingFramework.Core.Interceptions;

namespace EasyCaching.APIs
{
    using CachingFramework.Core;

    public class BookApi
    {
        [CacheableResult(cacheType=CacheType.AppFabric)]
        public static IEnumerable<Book> GetBooks(string authorName)
        {
            Thread.Sleep(1000);
            using (var repository = new BookRepository())
            {
                //return repository.Authors
                //                 .Where(a => a.Name == authorName)
                //                 .SelectMany(a => a.Books)
                //                 .ToList();
                return new List<Book>
                {
                    new Book {Id = 1, Title = "Life Of Brian", Authors = {new Author {Id = 1, Name = "Ross L. Finney"}}}
                };
            }
        }

        [AffectedCacheableMethods("EasyCaching.APIs.BookApi.GetBooks")]
        public static void ChangeAuthorName(string oldName, string newName)
        {
            using (var repository = new BookRepository())
            {
                // Commenting this to avoid making actual changes, or breaking the code
                // if the author doesn't exist in the database.

                /*repository.Authors.Single(a => a.Name == oldName).Name = newName;
                repository.SaveChanges();*/
            }
        }
    }
}