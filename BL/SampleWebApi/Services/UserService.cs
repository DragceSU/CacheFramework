// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserService.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SampleWebApi.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using CachingFramework.Core;
    using CachingFramework.Core.Interceptions;

    using DAL.IRepository;
    using DAL.Models;

    /// <summary>
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// </summary>
        /// <param name="userRepository">
        /// </param>
        public UserService(IUserRepository userRepository)
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
        [CacheableResult(cacheType = CacheType.NCacheExpress)]
        public Person GetPerson(string firstName, string lastName)
        {
            return this._userRepository.GetPersonBy(firstName, lastName);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        [CacheableResult(cacheType = CacheType.NCacheExpress)]
        public List<Person> GetAllPersons()
        {
            return this._userRepository.GetAllPersons().Take(30).ToList();
        }

        /// <summary>
        /// </summary>
        /// <param name="oldName">
        /// </param>
        /// <param name="oldLastName">
        /// </param>
        /// <param name="newName">
        /// </param>
        /// <param name="newLastName">
        /// </param>
        /// <returns>
        /// </returns>
        [AffectedCacheableMethods("SampleWebApi.Services.UserService.GetPerson")]
        public bool ChangeNameBy(string oldName, string oldLastName, string newName, string newLastName)
        {
            if (this._userRepository.ChangeNameBy(oldName, oldLastName, newName, newLastName))
                return true;
            else
                return false;
        }
    }
}