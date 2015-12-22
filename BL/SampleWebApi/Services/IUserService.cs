// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserService.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SampleWebApi.Services
{
    using System.Collections.Generic;

    using DAL.Models;

    /// <summary>
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// </summary>
        /// <param name="firstName">
        /// </param>
        /// <param name="lastName">
        /// </param>
        /// <returns>
        /// </returns>
        Person GetPerson(string firstName, string lastName);

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        List<Person> GetAllPersons();

        bool ChangeNameBy(string oldName, string oldLastName, string newName, string newLastName);
    }
}