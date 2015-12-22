// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DAL.IRepository
{
    using System.Collections.Generic;

    using DAL.Models;

    /// <summary>
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        Person GetPersonBy(int id);

        /// <summary>
        /// </summary>
        /// <param name="firstName">
        /// </param>
        /// <param name="lastName">
        /// </param>
        /// <returns>
        /// </returns>
        Person GetPersonBy(string firstName, string lastName);

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        IList<Person> GetAllPersons();

        /// <summary>
        /// </summary>
        /// <param name="oldName">
        /// </param>
        /// <param name="oldLastName">
        /// </param>
        /// <param name="newFirstName">
        /// </param>
        /// <param name="newLastName">
        /// </param>
        /// <returns>
        /// </returns>
        bool ChangeNameBy(string oldName, string oldLastName, string newFirstName, string newLastName);
    }
}