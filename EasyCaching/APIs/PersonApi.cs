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

    using CachingFramework.Core;
    using CachingFramework.Core.Interceptions;

    using DAL.IRepository;
    using DAL.Models;

    using SampleConsoleCaching.Model;

    /// <summary>
    /// </summary>
    public class PersonApi
    {
        /// <summary>
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// </summary>
        /// <param name="userRepository">
        /// </param>
        public PersonApi(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// </summary>
        /// <param name="firstName">
        /// </param>
        /// <param name="lastName">
        /// </param>
        /// <returns>
        /// </returns>
        [CacheableResult(cacheType = CacheType.AppFabric)]
        public Person GetPerson(string firstName, string lastName)
        {
            return this._userRepository.GetPersonBy(firstName, lastName);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        [CacheableResult(cacheType = CacheType.AppFabric)]
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