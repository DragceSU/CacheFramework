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
        public bool ChangeNameBy(string oldName, string oldLastName, string newFirstName, string newLastName)
        {
            using (var context = new DBContext())
            {
                try
                {
                    var person = context.People.FirstOrDefault(a => a.FirstName == oldName && a.LastName == oldLastName);
                    person.FirstName = newFirstName;
                    person.LastName = newLastName;
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}