// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookApi.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SampleConsoleCaching.APIs
{
    using System.Collections.Generic;
    using System.Threading;

    using CachingFramework.Core;
    using CachingFramework.Core.Interceptions;

    using DAL.IRepository;
    using DAL.Models;

    using SampleConsoleCaching.Model;

    /// <summary>
    /// </summary>
    public class BookApi
    {
        /// <summary>
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// </summary>
        /// <param name="userRepository">
        /// </param>
        public BookApi(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// </summary>
        /// <param name="authorName">
        /// </param>
        /// <returns>
        /// </returns>
        [CacheableResult(cacheType = CacheType.Memory)]
        public static IEnumerable<Book> GetBooks(string authorName)
        {
            Thread.Sleep(1000);
            using (var repository = new BookRepository())
            {
                return new List<Book>
                           {
                               new Book
                                   {
                                       Id = 1, 
                                       Title = "Life Of Brian", 
                                       Authors = {
                                                    new Author { Id = 1, Name = "Ross L. Finney" } 
                                                 }
                                   }
                           };
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="firstName">
        /// </param>
        /// <param name="lastName">
        /// </param>
        /// <returns>
        /// </returns>
        [CacheableResult(cacheType = CacheType.Disk)]
        public Person GetPerson(string firstName, string lastName)
        {
            return this._userRepository.GetPersonBy(firstName, lastName);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        [CacheableResult(cacheType = CacheType.Disk)]
        public IList<Person> GetAllPersons()
        {
            return this._userRepository.GetAllPersons();
        }

        /// <summary>
        /// </summary>
        /// <param name="oldName">
        /// </param>
        /// <param name="newName">
        /// </param>
        [AffectedCacheableMethods("SampleConsoleCaching.APIs.BookApi.GetBooks")]
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