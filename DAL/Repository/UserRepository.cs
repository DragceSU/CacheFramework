// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DAL.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using DAL.IRepository;
    using DAL.Models;

    /// <summary>
    /// </summary>
    [Serializable]
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public Person GetPersonBy(int id)
        {
            using (var context = new DBContext())
            {
                try
                {
                    return context.People.FirstOrDefault(p => p.BusinessEntityID == id);
                }
                catch (Exception ex)
                {
                    return null;
                }
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
        public Person GetPersonBy(string firstName, string lastName)
        {
            using (var context = new DBContext())
            {
                try
                {
                    return context.People.FirstOrDefault(p => p.FirstName == firstName && p.LastName == lastName);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public IList<Person> GetAllPersons()
        {
            using (var context = new DBContext())
            {
                try
                {
                    return context.People.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}