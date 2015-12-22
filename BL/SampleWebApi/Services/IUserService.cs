namespace SampleWebApi.Services
{
    using System.Collections.Generic;

    using DAL.Models;

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
    }
}